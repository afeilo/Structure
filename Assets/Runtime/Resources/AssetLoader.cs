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
        /// <summary>
        /// 正在加载的AB列表 key为BundleName
        /// </summary>
        private static List<string> bundleLoadingList = new List<string>();
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
                MLog.D("loadAssetCallBack = " + loadAssetCallBack);
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
            TaskManager.TaskState state = TaskManager.StartTask(coroutine, name);
            state.FinishedHandler = finishLoad;
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
                MLog.D("LoadAssetSuccessCallback = " + name);
                MLog.D("LoadAssetSuccessCallback = " + loadAssetCallBack);
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



        public IEnumerator LoadAssets(Request request)
        {
            Debug.Log("frameCount0  " + Time.frameCount);
            //加载AssetBundle阶段
            request.beginLoadTime = System.DateTime.Now;
            int len = 1;
            if (request.dependencies != null) {
                len += request.dependencies.Length;
            }
            for (int i = 0; i < len; i++)
            {
                string bundleName;
                if (i == len - 1)
                {
                    bundleName = request.bundleName;
                }
                else {
                    bundleName = request.dependencies[i];
                }
                
                var bundleObject = bundlePool.Spawn(bundleName);
                if (bundleObject == null)
                {
                    Debug.Log("LoadAssets  " + bundleName);
                    //bundleLoadingList.Add(bundleName);
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
                    Debug.Log("bundlePool add  " + bundleName);
                    //bundleLoadingList.Remove(bundleName);
                }
                
            }

            Debug.Log("frameCount1 " + Time.frameCount);
            var now = System.DateTime.Now;
            var dt1 = now - request.beginQueueTime;
            var dt2 = now - request.beginLoadTime;
            Debug.LogFormat("wao load bundle Request(bundle={0}, alltime={1},loadtime={2}", request.bundleName, dt1.TotalSeconds,dt2.TotalSeconds);
            request.beginLoadTime = System.DateTime.Now;
            //加载Asset阶段
            BaseObject<Object> assetObject = assetPool.Spawn(request.assetName);
            if (assetObject == null) {
                var bundleObject = bundlePool.Spawn(request.bundleName);
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
            now = System.DateTime.Now;
            dt1 = now - request.beginQueueTime;
            dt2 = now - request.beginLoadTime;
            Debug.LogFormat("wao load Asset(bundle={0}, alltime={1},loadtime={2}", request.assetName, dt1.TotalSeconds, dt2.TotalSeconds);
            Debug.Log("frameCount2 " + Time.frameCount);
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
