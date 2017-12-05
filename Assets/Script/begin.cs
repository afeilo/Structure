using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Runtime;

public class begin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//初始化
        //加载默认页面
        ModuleEntry.GetModule<UIComponent>().Open("mainui");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
