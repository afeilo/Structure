using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.FrameWork
{
    public interface IStateView
    {

        /// <summary>
        /// 界面创建完成 用来初始化数据
        /// </summary>
        void PageCreate();

        /// <summary>
        /// 页面显示
        /// </summary>
        void PageStart();

        /// <summary>
        /// 界面暂停
        /// </summary>
        void PagePause();

        /// <summary>
        /// 界面激活
        /// </summary>
        void PageResume();

        /// <summary>
        /// 页面暂停
        /// </summary>
        void PageStop();

        /// <summary>
        /// 页面销毁
        /// </summary>
        void PageDestory();

        /// <summary>
        /// 是否为弹出框
        /// </summary>
        /// <returns></returns>
        bool IsPopupWindow();
    }
}
