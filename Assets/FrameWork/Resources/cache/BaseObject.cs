using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork.Resources.cache
{
    public abstract class BaseObject
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 引用数量
        /// </summary>
        public int ReferencesCount;
        /// <summary>
        /// 缓存对象
        /// </summary>
        public object Target;
        /// <summary>
        /// 释放
        /// </summary>
        public abstract void Release();

        public BaseObject(string name,object target) : this(name,target,0) { 
            
        }
        public BaseObject(string name, object target,int referencesCount)
        {
            Name = name;
            Target = target;
            ReferencesCount = referencesCount;
        }
    }
}
