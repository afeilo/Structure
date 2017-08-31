using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/**
* 引用计数器
*/
class CountReference : MonoBehaviour{
	public string aBName;
	CacheData<UnityEngine.Object> cache;
    public void Awake(){
		MLog.D ("Awake");
		MemoryCache.GetInstance ().ReferenceAdd (aBName);
    }

	public void OnDestroy(){
		MLog.D ("OnDestroy");
		MemoryCache.GetInstance ().ReferenceRemove (aBName);
    }

}
