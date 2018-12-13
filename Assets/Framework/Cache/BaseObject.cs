using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework
{
    public abstract class BaseObject<T>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 缓存对象
        /// </summary>
        public T Target;

        public BaseObject(string name, T target)
        {
            Name = name;
            Target = target;
        }

    }
    
}
