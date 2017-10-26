using Assets.FrameWork;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Runtime
{
    public class AssetLoader : IAssetLoader
    {
        ///// <summary>
        ///// 正在加载的资源列表 key为AssetName
        ///// </summary>
        //private static Dictionary<string, Request> assetLoadingList = new Dictionary<string, Request>();
        ///// <summary>
        ///// 正在加载的AB列表 key为BundleName
        ///// </summary>
        //private static Dictionary<string, Request> bundleLoadingList = new Dictionary<string, Request>();
        /// <summary>
        /// AB缓存
        /// </summary>
        private static ISpawnPool<AssetBundle> bundlePool = new ResourceSpawnPool<AssetBundle>();
        /// <summary>
        /// AB缓存
        /// </summary>
        private static ISpawnPool<Object> assetPool = new ResourceSpawnPool<Object>();
        /// <summary>
        /// 回调列表 key为assetName
        /// </summary>
        private static Dictionary<string, List<LoadAssetCallbacks>> callBacks = new Dictionary<string, List<LoadAssetCallbacks>>();

        /// <summary>
        /// AssetBundle release
        /// </summary>
        private BundleReleaseHelper bundleReleaseHelper = new BundleReleaseHelper();
        /// <summary>
        /// object release
        /// </summary>
        private ObjectReleaseHelper objectReleaseHelper = new ObjectReleaseHelper();
        /// <summary>
        /// 加载ab
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onComplete"></param>AssetLoader
        public override void LoadAsset(string bundleName, string assetName, string[] dependencies, LoadAssetCallbacks callback)
        {
            Request request = GetRequest(bundleName, assetName,dependencies, null);
            List<LoadAssetCallbacks> loadAssetCallBack;
            callBacks.TryGetValue(assetName, out loadAssetCallBack);
            if (loadAssetCallBack != null)
            {
                loadAssetCallBack.Add(callback);
            }
            else {
                loadAssetCallBack = new List<LoadAssetCallbacks>();
                loadAssetCallBack.Add(callback);
                callBacks.Add(assetName, loadAssetCallBack);
                startTask(LoadAssets(request), assetName);
            }
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        /// <param name="coroutine"></param>
        /// <param name="name"></param>
        private void startTask(IEnumerator coroutine, string name)
        {
            TaskManager.TaskState state = TaskManager.CreateTask(coroutine, name);
            state.Finished += finishLoad;
            state.Start();
        }


        /// <summary>
        /// 加载完成
        /// </summary>
        /// <param name="manual"></param>
        /// <param name="name"></param>
        private void finishLoad(bool manual, string name)
        {
            MLog.D("finishLoad = " + manual);
            if (!manual)
            {
                BaseObject<Object> assetObject = assetPool.Spawn(name);
                List<LoadAssetCallbacks> loadAssetCallBack;
                callBacks.TryGetValue(name, out loadAssetCallBack);
                if (loadAssetCallBack != null)
                {
                    
                    for (int i = 0, len = loadAssetCallBack.Count; i < len; i++)
                    {
                        if (loadAssetCallBack[i] != null)
                        {
                            MLog.D("LoadAssetSuccessCallback = " + name);
                            loadAssetCallBack[i].LoadAssetSuccessCallback(name, assetObject.Target);
                        }

                    }
                    callBacks.Remove(name);
                }
            }
        }


        private IEnumerator loadAB(string bundleName)
        {
            BaseObject<AssetBundle> bundleObject = bundlePool.Spawn(bundleName);
            if (bundleObject == null)
            {
                var bundleLoadRequest = AssetBundle.LoadFromFileAsync(UUtils.GetStreamingAssets(bundleName));
                yield return bundleLoadRequest;

                var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
                if (myLoadedAssetBundle == null)
                {
                    MLog.E("Failed to load AssetBundle!");
                    yield break;
                }
                bundleObject = new ResourceObject<AssetBundle>(bundleName, myLoadedAssetBundle, bundleReleaseHelper);
                bundlePool.Add(bundleName, bundleObject);
            }
        
        }

        public IEnumerator LoadAssets(Request request)
        {
            if (request.dependencies != null) {
                for (int i = 0, len = request.dependencies.Length; i < len; i++) {
                    yield return loadAB(request.dependencies[i]);
                }
            }
            yield return loadAB(request.bundleName);
            //加载AssetBundle阶段
            BaseObject<AssetBundle> bundleObject = bundlePool.Spawn(request.bundleName);
            //加载Asset阶段
            BaseObject<Object> assetObject = assetPool.Spawn(request.assetName);
            if (assetObject == null) {
                AssetBundle assetBundle = bundleObject.Target;
                var assetLoadRequest = assetBundle.LoadAssetAsync(request.assetName);
                yield return assetLoadRequest;
                if (assetLoadRequest == null)
                {
                    MLog.E("Failed to load Asset!");
                    yield break;
                }
                assetObject = new ResourceObject<Object>(request.bundleName, assetLoadRequest.asset,objectReleaseHelper);
                assetPool.Add(request.assetName, assetObject);
            }

        }


        /// <summary>
        /// 构造request
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="assetName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Request GetRequest(string bundleName,string assetName,string[] dependencies, string path)
        {
            if (path == null)
                return new Request(bundleName, assetName, UUtils.GetStreamingAssets(bundleName), dependencies);
            else
                return new Request(bundleName, assetName, path, dependencies);
        }
    }

}
