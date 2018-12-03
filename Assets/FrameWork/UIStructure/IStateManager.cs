using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.FrameWork
{
    public interface IStateManager
    {
        /// <summary>
        /// 切换界面
        /// </summary>
        /// <param name="stateId"></param>
        void SetCurrentView(string abName,string assetName);

        /// <summary>
        /// 返回到上一个界面
        /// </summary>
        void GoBack();

        /// <summary>
        /// 获取当前state
        /// </summary>
        /// <returns></returns>
        IStateView getCurrentView();
    }
}