using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CacheData<T>
{
	private int realReferences = 0;
	public int references{
		get{ 
			return realReferences;
		}
		set{
			if (value >= 0)
				realReferences ++;
			else
				realReferences --;
//			MLog.D (""+value);
		}
	}
    public T cache;
    public string[] dependencies;
}
