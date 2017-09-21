using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ABLoader:MonoBehaviour{
	private static string path;
    private static Dictionary<string, Request> loadingList = new Dictionary<string, Request>();
    private AssetBundleManifest abManifest;

    private static ABLoader loader;
    private static string abloader_name = "ABLoader";

    public static ABLoader instance
    {
        get
        {
            if (loader == null)
            {
                GameObject go = new GameObject(abloader_name);
                loader = go.AddComponent<ABLoader>();
            }

            return loader;
        }
    }

    public void Awake(){
        path = Application.streamingAssetsPath;
        loadManifest();
    }

    private void loadManifest()
    {
        MLog.D("loadManifest");
        var bundleLoadRequest = AssetBundle.LoadFromFile(Path.Combine(path,"StreamingAssets"));
        abManifest = bundleLoadRequest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    }

    /// <summary>
    /// 加载ab
    /// </summary>
    /// <param name="name"></param>
    /// <param name="onComplete"></param>
    public void Load(string name, System.Action<Object> onComplete)
    {
        //是否正在加载
        if (loadingList.ContainsKey(name)) {
            return;
        }
        //缓存获取
        CacheData<Object> cacheData = MemoryCache.GetInstance().Get(name);
        //MLog.D(cacheData.ToString());
        if (cacheData != null)
        {
            if (onComplete != null) {
                onComplete(cacheData.cache);
            }
            return;
        }
		//添加依赖
        string[] dependecies = abManifest.GetAllDependencies(name);
        for (int i = 0; i < dependecies.Length; i++) {
            Load(dependecies[i],null);
        }
        addRequest(name, null, dependecies,onComplete);
	}

   
    /// <summary>
    /// 构建Request
    /// </summary>
    /// <param name="name"></param>
    /// <param name="path"></param>
    /// <param name="dependencice"></param>
    private void addRequest(string name,string path,string[] dependencice){
        addRequest(name, path, dependencice, null);
    }

    /// <summary>
    /// 构建Request
    /// </summary>
    /// <param name="name"></param>
    /// <param name="path"></param>
    /// <param name="dependencice"></param>
    /// <param name="onComplete"></param>
    private void addRequest(string name, string path, string[] dependencice, System.Action<Object> onComplete)
    {
        Request request = GetRequest(name, null);
        request.dependencies = dependencice;
        request.onComplete = onComplete;
        loadingList.Add(name, request);
        startTask(new ABAsyncLoader().LoadAssets(request), name);
    }

    /// <summary>
    /// 开启任务
    /// </summary>
    /// <param name="coroutine"></param>
    /// <param name="name"></param>
    private void startTask(IEnumerator coroutine,string name) {
        TaskManager.TaskState state = TaskManager.CreateTask(coroutine, name);
        state.Finished += finishLoad;
        state.Start();
    }

    /// <summary>
    /// 加载完成
    /// </summary>
    /// <param name="manual"></param>
    /// <param name="name"></param>
    private void finishLoad(bool manual,string name)
    {
		MLog.D("finishload = " + name);
        if (!manual) {
            Request request;
            loadingList.TryGetValue(name, out request);
            if (request != null)
            {
                string[] dependencies = request.dependencies;
                for (int i = 0; i < dependencies.Length; i++) {
                    if (MemoryCache.GetInstance().Get(dependencies[i]) == null) {
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
                if (request.onComplete!=null)
                    request.onComplete(request.obj);
            }
            
        }
    }

    //暂时依赖用这种策略，但是比较消耗资源，有更好的方式再替换
    IEnumerator wait4Dependencies(string[] dependencies){
        for (int i = 0; i < dependencies.Length; i++)
        {
            while (MemoryCache.GetInstance().Get(dependencies[i]) == null)
            {
                yield return 0;
            }
        } 
    }

	public Request GetRequest(string name,string path){
        if(path == null)
            return new Request(name, Path.Combine(Application.streamingAssetsPath, name));
        else
            return new Request(name, path);
	}
}
