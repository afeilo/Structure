using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework
{
    public abstract class ABBaseObject<T> : BaseObject<T>
    {
        /// <summary>
        /// 正在操作
        /// </summary>
        public bool isLock = false;
        /// <summary>
        /// 引用数量
        /// </summary>
        public int ReferencesCount;
        public abstract void Release();

        //public delegate void LoadSuccess(T t);
        //public delegate void LoadFail(string errorMessage);

        public Action<T> LoadAssetSuccessCallback;
        public Action LoadAssetFailCallback;

        public ABBaseObject(string name, T target)
            : base(name, target)
        {

        }

        public void AddReference()
        {
            ReferencesCount++;
        }

        public void RemoveReference()
        {
            ReferencesCount--;
        }
    }

}
