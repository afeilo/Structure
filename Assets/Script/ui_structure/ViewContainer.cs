using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与界面直接绑定的类，用来索引界面信息
/// </summary>
public class ViewContainer : MonoBehaviour {

    /// <summary>
    /// 用来描述页面层级
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 最基本ui
        /// </summary>
        baseUI = 1,
        /// <summary>
        /// 页面ui
        /// </summary>
        pageUI = 2,
        /// <summary>
        /// 弹出框
        /// </summary>
        popupUI = 3,
        /// <summary>
        /// 加载框
        /// </summary>
        loadingUI = 4,
    }

    public State state = State.baseUI;

    public bool isPopup = false;

    public List<ReferData> refers;
	// Use this for initialization
    void Awake() {
       
    }
	void Start () {
        transform.SetParent(UIStateManager.instance.GetParent((int)state));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
