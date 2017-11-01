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
        assetLoader.LoadAsset("pic1", "p1",null, loadAssetCallback, REQUEST_TYPE.HTTP);
        assetLoader.LoadAsset("pic2", "p2", null, loadAssetCallback, REQUEST_TYPE.HTTP);
        assetLoader.LoadAsset("pic3", "p3", null, loadAssetCallback, REQUEST_TYPE.HTTP);
        //assetLoader.LoadAsset("pic4", "p4", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic5", "p5", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic6", "p6", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic7", "p7", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic8", "p8", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic9", "p9", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic10", "p10", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic11", "p11", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic12", "p12", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic13", "p13", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic14", "p14", null, loadAssetCallback);
        //assetLoader.LoadAsset("pic15", "p15", null, loadAssetCallback);
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
