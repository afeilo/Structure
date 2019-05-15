using AppSettings;
using Assets.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public class Test : MonoBehaviour {
	void Start () {
        DalObj dalObj = new DalObj("users");
        StringBuilder sb = new StringBuilder();
        foreach (FieldInfo proInfo in dalObj.GetType().GetFields())
        {
            object[] attrs = proInfo.GetCustomAttributes(typeof(DBFieldAttribute), true);
            Debug.Log(proInfo.Name + "  " + proInfo.FieldType);
            
            //if (attrs.Length == 1)
            //{
                //DBFieldAttribute attr = (DBFieldAttribute)attrs[0];
                //proInfo.SetValue(dalObj, "13", null);
                proInfo.SetValue(dalObj, "123");
                Debug.Log("setvalue");
                //sb.Append(attr.FieldName + ":" + (attr.DefaultValue == null ? "null" : attr.DefaultValue.ToString()) + "\r\n");
            //}
        }
        Debug.Log(sb.ToString());
        Debug.Log(dalObj.Name);
        //Debug.Log(dalObj.PassWord);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
