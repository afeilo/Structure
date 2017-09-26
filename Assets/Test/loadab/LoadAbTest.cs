using Assets.FrameWork.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAbTest : MonoBehaviour {
    LoadAssetCallbacks loadAssetCallback;
	// Use this for initialization
	void Start () {
        loadAssetCallback = new LoadAssetCallbacks(loadSuccess, loadFail);
	}
    public void load() {
        ResourceManager.instance.LoadAsset("image_1", loadAssetCallback);
    }

    /// <summary>
    /// 加载成功
    /// </summary>
    /// <param name="obj"></param>
    private void loadSuccess(string abname, System.Object obj)
    {
        Debug.Log("loadSuccess");
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
