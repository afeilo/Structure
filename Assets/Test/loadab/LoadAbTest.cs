using Assets.FrameWork;
using Assets.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAbTest : MonoBehaviour {
    LoadAssetCallbacks loadAssetCallback;
    IAssetLoader assetLoader;
	// Use this for initialization
    System.DateTime begin;
	void Start () {
        loadAssetCallback = new LoadAssetCallbacks(loadSuccess, loadFail);
        assetLoader = new AssetLoader();
	}
    public void load() {
        begin = System.DateTime.Now;
        assetLoader.LoadAsset("pic", "p1",null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p2", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p3", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p4", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p5", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p6", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p7", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p8", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p9", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p10", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p11", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p12", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p13", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p14", null, loadAssetCallback);
        assetLoader.LoadAsset("pic", "p15", null, loadAssetCallback);
    }

    /// <summary>
    /// 加载成功
    /// </summary>
    /// <param name="obj"></param>
    private void loadSuccess(string abname, System.Object obj)
    {
        var time = System.DateTime.Now - begin;
        Debug.Log("loadSuccess "+abname +"  "+time.TotalSeconds);
        GameObject gameObject = Instantiate(obj as Object) as GameObject;
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="errorMessage"></param>
    private void loadFail(string name, string errorMessage)
    {
        Debug.Log("loadFail");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
