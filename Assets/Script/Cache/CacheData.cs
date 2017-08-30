using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CacheData<T>
{
    public int references;
    public T cache;
    public string[] dependencies;
}
