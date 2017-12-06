using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Runtime;

public class begin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//初始化
        //加载默认页面
        Debug.Log(ModuleEntry.GetModule<ResourceComponent>());
        ModuleEntry.GetModule<ResourceComponent>().LoadAsset("ui_canvas", "ui_canvas",loadSuccess,null);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void loadSuccess(string abname, System.Object obj)
    {
        Instantiate((Object)obj);
        ModuleEntry.GetModule<UIComponent>().Open("mainui");
    }

}
