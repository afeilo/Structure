using System;

namespace Assets.FrameWork
{
    public interface IStateHelper
    {
        /// <summary>
        /// 实例化object 并返回实例
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        object InstantObject(object obj);

        /// <summary>
        /// 创建View
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IStateView BindView(object obj,string name);
    }
}
