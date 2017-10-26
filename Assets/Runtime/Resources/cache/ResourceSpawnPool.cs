using Assets.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Runtime
{
    class ResourceSpawnPool<T> : ISpawnPool<T>
    {
        /**
         * Todo 这种写法有点破坏原有的结构
         */
        Dictionary<string, BaseObject<T>> cache = new Dictionary<string, BaseObject<T>>();
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public BaseObject<T> Spawn(string name)
        {
            BaseObject<T> t;
            cache.TryGetValue(name, out t);
            return t;
        }
        /// <summary>
        /// 加入缓存池
        /// </summary>
        /// <param name="name"></param>
        /// <param name="t"></param>
        public void Add(string name, BaseObject<T> t)
        {
            cache.Add(name, t);
        }
        /// <summary>
        /// 回收
        /// </summary>
        public void Release() { 
        
        }
    }
}
