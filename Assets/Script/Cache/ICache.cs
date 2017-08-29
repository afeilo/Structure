using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Cache
{
    public abstract class ICache<K,V>
    {
        protected IDictionary<K, V> caches;
        /**
         * 初始化
         */
        public abstract void Init();
        /**
         * 缓存清除
         */
        public abstract void Clear();
        /**
         * 主动调用回收
         */ 
        public abstract void Recycle();
        /**
         * 添加数据
         */
        public virtual void Add(K key, V value) {
            if (!checkNull()) {
                return;
            }
            caches.Add(key,value);
        }
        /**
         * 获取数据
         */ 
        public virtual V Get(K key)
        {
            if (!checkNull())
                return default(V);
            V value;
            caches.TryGetValue(key, out value);
            return value;
        }
        /**
         * 删除数据
         */ 
        public abstract void Remove(K key);

        protected bool checkNull() {
            return caches != null;
        }
    }
}
