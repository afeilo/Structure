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
		cache = MemoryCache.GetInstance ().Get (aBName);
		cache.references = 1;
    }
    public void Destory(){
		if (cache != null)
			cache.references= -1;
    }

}
