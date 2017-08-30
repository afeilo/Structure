using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ICache<K,V>
{
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
        public abstract void Add(K key,V value);
        /**
         * 获取数据
         */
        public abstract V Get(K key);
        /**
         * 删除数据
         */ 
        public abstract void Remove(K key);
}
