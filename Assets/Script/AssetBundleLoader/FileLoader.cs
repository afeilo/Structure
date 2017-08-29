using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileLoader : MonoBehaviour{
	private static string path;
    private Dictionary<string, Request> loadingList;
    private AssetBundleManifest abManifest;
    public void Awake(){
        loadingList = new Dictionary<string, Request>();
        path = Application.streamingAssetsPath;
        loadManifest();
    }

    private void loadManifest()
    {
        var bundleLoadRequest = AssetBundle.LoadFromFile( Path.Combine(path,"StreamingAssets"));
        abManifest = bundleLoadRequest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    }

    public void GC() {
        System.GC.Collect();
    }
    public void Load(string name) {
        Load(name, o => {
            print("instantiate");
            Instantiate(o);
        });
    }
    public void Load(string name, System.Action<Object> onComplete)
    {
        //是否正在加载
        if (loadingList.ContainsKey(name)) {
            return;
        }
        //缓存获取
        Request request = MemoryCache.GetInstance().Get(name);
        if (request != null)
        {
            if (onComplete != null) {
                onComplete(request.obj);
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
    public void Load(Request request) { 
        
    }

    //构建Request
    private void addRequest(string name,string path,string[] dependencice){
        addRequest(name, path, dependencice, null);
    }
    private void addRequest(string name, string path, string[] dependencice, System.Action<Object> onComplete)
    {
        Request request = GetRequest(name, null);
        request.dependencies = dependencice;
        request.onComplete = onComplete;
        loadingList.Add(name, request);
        startTask(new FileAsyncLoader().LoadAssets(request), name);
    }
    private void startTask(IEnumerator coroutine,string name) {
        TaskManager.TaskState state = TaskManager.CreateTask(coroutine, name);
        state.Finished += finishLoad;
        state.Start();
    }
    //加载完成
    private void finishLoad(bool manual,string name)
    {
        print("finishload = " + name);
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
                MemoryCache.GetInstance().Add(name, request);
                loadingList.Remove(name);
                print(request.onComplete);
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
