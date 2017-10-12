using Assets.FrameWork;
using Assets.FrameWork.Resources;
using Assets.FrameWork.Resources.cache;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Runtime
{
    public class AssetLoader : IAssetLoader
    {

        private static Dictionary<string, Request> loadingList = new Dictionary<string, Request>();
        private ISpawnPool<AssetObject> resourcePool = new AssetSpawnPool<AssetObject>();
        private Dictionary<string, List<LoadAssetCallbacks>> callBacks = new Dictionary<string, List<LoadAssetCallbacks>>();

        /// <summary>
        /// 加载ab
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onComplete"></param>AssetLoader
        public override void LoadAsset(string name, LoadAssetCallbacks callback)
        {
            if (callBacks.ContainsKey(name))
            {
                List<LoadAssetCallbacks> loadAssetCallBack;
                callBacks.TryGetValue(name, out loadAssetCallBack);
                if (loadAssetCallBack != null)
                {
                    loadAssetCallBack.Add(callback);
                    //request.callback += callback;
                    
                }
                return;
            }
            //缓存获取
            AssetObject cacheData = resourcePool.Spawn(name);

            if (cacheData != null)
            {
                if (callback != null)
                {
                    callback.LoadAssetSuccessCallback(name, cacheData.Target);
                }
                return;
            }

            addRequest(name, null,callback);
        }

        /// <summary>
        /// 构建Request
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="dependencice"></param>
        private void addRequest(string name, string path)
        {
            addRequest(name, path, null);
        }

        /// <summary>
        /// 构建Request
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="dependencice"></param>
        /// <param name="onComplete"></param>
        private void addRequest(string name, string path, LoadAssetCallbacks callback)
        {
            Request request = GetRequest(name, null);

            List<LoadAssetCallbacks> loadAssetCallBack = new List<LoadAssetCallbacks>();
            loadAssetCallBack.Add(callback);
            callBacks.Add(name, loadAssetCallBack);
            loadingList.Add(name, request);
            startTask(LoadAssets(request), name);
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
                
                Request request;
                loadingList.TryGetValue(name, out request);
                if (request != null)
                {
                    AssetObject assetObject = new AssetObject(request.ab, name, request.obj);
                    //从正在加载中移除
                    loadingList.Remove(name);
                    //MLog.D(request.onComplete);
                    List<LoadAssetCallbacks> loadAssetCallBack;
                    callBacks.TryGetValue(name, out loadAssetCallBack);
                    if (loadAssetCallBack != null) {
                        for (int i = 0, len = loadAssetCallBack.Count; i < len; i++)
                        {
                            if (loadAssetCallBack[i]!=null) {
                                MLog.D("LoadAssetSuccessCallback = " + request.name);
                                loadAssetCallBack[i].LoadAssetSuccessCallback(request.name, request.obj);
                            }
                            
                        }
                        callBacks.Remove(name); 
                    }
                   
                }

            }
        }


        public IEnumerator LoadAssets(Request request)
        {
            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(request.path);
            yield return bundleLoadRequest;

            var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }

            var assetLoadRequest = myLoadedAssetBundle.LoadAllAssetsAsync();
            yield return assetLoadRequest;
            request.obj = assetLoadRequest.asset;
            request.ab = myLoadedAssetBundle;
            //        myLoadedAssetBundle.Unload(false);

            //WWW www = new WWW (request.path);
            //yield return www;
            //request.ab = www.assetBundle;
            //Object obj = www.assetBundle.mainAsset;
            //Object[] objs = www.assetBundle.LoadAllAssets();
            //yield return objs;
            //request.obj = objs[0];
            //GameObject clone = Instantiate (request.obj) as GameObject;
            //clone.name = request.name;
            //if (parent) {
            //	clone.transform.parent = parent;
            //}
        }

        //暂时依赖用这种策略，但是比较消耗资源，有更好的方式再替换
        //IEnumerator wait4Dependencies(string[] dependencies)
        //{
        //    for (int i = 0; i < dependencies.Length; i++)
        //    {
        //        while (MemoryCache.GetInstance().Get(dependencies[i]) == null)
        //        {
        //            yield return 0;
        //        }
        //    }
        //}

        public Request GetRequest(string name, string path)
        {
            if (path == null)
                return new Request(name, UUtils.GetStreamingAssets(name));
            else
                return new Request(name, path);
        }
    }

}
