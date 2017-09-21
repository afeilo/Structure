using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateObject{
    /// <summary>
    /// ui 名称
    /// </summary>
    public string name;
    /// <summary>
    /// abname
    /// </summary>
    public string abName;
    /// <summary>
    /// 是否为popup对象
    /// </summary>
    public bool isPopup;
    public StateObject(string name, string abName,bool isPopup = false)
    {
        this.name = name;
        this.abName = abName;
        this.isPopup = isPopup;
    }
}
