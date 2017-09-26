using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork.Resources.cache
{
    public interface ISpawnPool<T> where T:BaseObject
    {
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T Spawn(string name);
        /// <summary>
        /// 加入缓存池
        /// </summary>
        /// <param name="name"></param>
        /// <param name="t"></param>
        void Add(string name, T t);
        /// <summary>
        /// 回收
        /// </summary>
        void Release();
    }
}
