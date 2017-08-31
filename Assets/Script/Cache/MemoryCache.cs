//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 加载到内存后做一次缓存
 */
public class MemoryCache : ICache<string, CacheData<Object>>
{
    private static Dictionary<string, CacheData<Object>> caches;
    private static MemoryCache cache;
    public MemoryCache() {
        caches = new Dictionary<string, CacheData<Object>>();
    }
	public void ReferenceAdd(string key){
		var cache = Get (key);
		if (cache != null) {
			cache.references++;
		} else {
			MLog.E ("can't find Cache");
		}
	}
	public void ReferenceRemove(string key){
		var cache = Get (key);
		if (cache != null) {
			cache.references--;
			if (cache.references == 0) {
				MLog.D("remove" + key);
				Remove (key);
				cache.Dispose ();
			}
		} else {
			MLog.E ("can't find Cache");
		}
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
        if (!checkNull())
        {
            return;
        }
        var cache = Get(key);
        if(cache == null){
            MLog.D("can't remove value,because not find value");
            return;
        }
        string[] dependencies = cache.dependencies;
        for (int i = 0; i < dependencies.Length; i++)
        {
			ReferenceRemove (dependencies [i]);
//            var dependence = Get(dependencies[i]);
//            if (dependence != null)
//            {
//				dependence.references= 1;
//            }
//            else
//            {
//
//                MLog.E("dependencies can't loaded normally");
//            }
        }
		caches.Remove (key);
        //cache.references--;
    }

    public override void Add(string key, CacheData<Object> value)
    {
        if (!checkNull())
        {
            return;
        }
        caches.Add(key, value);
        string[] dependencies = value.dependencies;
        for (int i = 0; i < dependencies.Length; i++) { 
			ReferenceAdd (dependencies [i]);
        }
        //value.references++;
    }
    /**
     * 获取数据
     */
    public override CacheData<Object> Get(string key)
    {
        if (!checkNull())
            return default(CacheData<Object>);
        CacheData<Object> value;
        caches.TryGetValue(key, out value);
        return value;
    }
    /**
     * 判断是否为null
     */ 
    protected bool checkNull()
    {
        return caches != null;
    }

    /**
    *获取单例
    */ 
    public static MemoryCache GetInstance(){
        if (null == cache)
            cache = new MemoryCache();
        return cache;
    }
}
