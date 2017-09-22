using Assets.FrameWork.Resources;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.FrameWork.Resources.simulate
{
    public class ABLoader : IAssetLoader
    {
        private static string path;
        private static Dictionary<string, Request> loadingList = new Dictionary<string, Request>();
        private AssetBundleManifest abManifest;

        private static ABLoader loader;
        private static string abloader_name = "ABLoader";

        public ABLoader()
        {
            init(Application.streamingAssetsPath);
        }

        public void init(string abpath)
        {
            path = abpath;
            loadManifest();
        }

        private void loadManifest()
        {
            MLog.D("loadManifest");
            var bundleLoadRequest = AssetBundle.LoadFromFile(Path.Combine(path, "StreamingAssets"));
            abManifest = bundleLoadRequest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }


        public void Load(string name, System.Action<Object> onComplete)
        {
            //是否正在加载

        }

        /// <summary>
        /// 加载ab
        /// </summary>
        /// <param name="name"></param>
        /// <param name="onComplete"></param>
        public override void LoadAsset(string name, LoadAssetCallbacks callback)
        {
            if (loadingList.ContainsKey(name))
            {
                Request request;
                loadingList.TryGetValue(name, out request);
                if (request != null)
                {
                    //request.callback += callback;
                }
                return;
            }
            //缓存获取
            CacheData<Object> cacheData = MemoryCache.GetInstance().Get(name);
            //MLog.D(cacheData.ToString());
            if (cacheData != null)
            {
                if (callback != null)
                {
                    callback.LoadAssetSuccessCallback(name, cacheData.cache);
                }
                return;
            }
            //添加依赖
            string[] dependecies = abManifest.GetAllDependencies(name);
            for (int i = 0; i < dependecies.Length; i++)
            {
                Load(dependecies[i], null);
            }
            addRequest(name, null, dependecies, callback);
        }

        /// <summary>
        /// 构建Request
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="dependencice"></param>
        private void addRequest(string name, string path, string[] dependencice)
        {
            addRequest(name, path, dependencice, null);
        }

        /// <summary>
        /// 构建Request
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="dependencice"></param>
        /// <param name="onComplete"></param>
        private void addRequest(string name, string path, string[] dependencice, LoadAssetCallbacks callback)
        {
            Request request = GetRequest(name, null);
            request.dependencies = dependencice;
            request.callback = callback;
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
            MLog.D("finishload = " + name);
            if (!manual)
            {
                Request request;
                loadingList.TryGetValue(name, out request);
                if (request != null)
                {
                    string[] dependencies = request.dependencies;
                    for (int i = 0; i < dependencies.Length; i++)
                    {
                        if (MemoryCache.GetInstance().Get(dependencies[i]) == null)
                        {
                            startTask(wait4Dependencies(dependencies), name);
                            return;
                        }
                    }
                    //Instantiate(request.obj);
                    CacheData<Object> cacheData = new CacheData<Object>();
                    cacheData.cache = request.obj;
                    cacheData.ab = request.ab;
                    cacheData.dependencies = request.dependencies;
                    //加入缓存
                    MemoryCache.GetInstance().Add(name, cacheData);
                    //从正在加载中移除
                    loadingList.Remove(name);
                    //MLog.D(request.onComplete);

                    request.callback.LoadAssetSuccessCallback(request.name, request.obj);
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
        IEnumerator wait4Dependencies(string[] dependencies)
        {
            for (int i = 0; i < dependencies.Length; i++)
            {
                while (MemoryCache.GetInstance().Get(dependencies[i]) == null)
                {
                    yield return 0;
                }
            }
        }

        public Request GetRequest(string name, string path)
        {
            if (path == null)
                return new Request(name, Path.Combine(Application.streamingAssetsPath, name));
            else
                return new Request(name, path);
        }
    }

}
