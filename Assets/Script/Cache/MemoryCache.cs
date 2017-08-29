using Assets.Script.Cache;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 加载到内存后做一次缓存
 */
public class MemoryCache: ICache<string,Request>{
	//private Dictionary<string,object> caches;
    private static MemoryCache cache;
    public MemoryCache() {
        caches = new Dictionary<string, Request>();
    }
    public override void Init() { 
    }
    public override void Clear() { 
    }

    public override void Recycle()
    {

    }
    public override void Remove(string key)
    {
    }

    

    public static MemoryCache GetInstance(){
        if (null == cache)
            cache = new MemoryCache();
        return cache;
    }
}
