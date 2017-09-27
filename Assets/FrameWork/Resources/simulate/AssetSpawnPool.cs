﻿using Assets.FrameWork.Resources.cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork.Resources.simulate
{
    class AssetSpawnPool<T> : ISpawnPool<T> where T : BaseObject
    {
        Dictionary<string, T> cache = new Dictionary<string, T>();
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Spawn(string name) {
            T t;
            cache.TryGetValue(name, out t);
            return t;
        }
        /// <summary>
        /// 加入缓存池
        /// </summary>
        /// <param name="name"></param>
        /// <param name="t"></param>
        public void Add(string name, T t) {
            cache.Add(name, t);
        }
        /// <summary>
        /// 回收
        /// </summary>
        public void Release() { 
        
        }
    }
}
