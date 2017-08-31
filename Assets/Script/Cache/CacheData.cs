using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CacheData<T> : IDisposable
{
//	private int realReferences = 0;
	public int references = 0;
//	{
//		get{ 
//			return realReferences;
//		}
//		set{
//			if (value >= 0)
//				realReferences ++;
//			else
//				realReferences --;
////			MLog.D (""+value);
//		}
//	}
    public T cache;
	public AssetBundle ab;
    public string[] dependencies;
	public void Dispose(){
		ab.Unload (true);
//		this = null;
	}
}
