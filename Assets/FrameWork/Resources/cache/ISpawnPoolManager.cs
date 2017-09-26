using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork.Resources.cache
{
    public interface ISpawnPoolManager
    {
        /// <summary>
        /// 创建缓存池
        /// </summary>
        ISpawnPool<T> CreateSpawnPool<T>() where T :BaseObject;

        /// <summary>
        /// 释放缓存池
        /// </summary>
        void ReleaseSpawnPool();
    }
}
