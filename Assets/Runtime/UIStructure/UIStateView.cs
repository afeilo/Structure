using Assets.FrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Runtime
{
    public class UIStateView : MonoBehaviour, IStateView
    {
        public bool isPopupWindow = false;
        // Use this for initialization

        public virtual void PageCreate()
        {
            MLog.D("PageCreate");
        }
        public virtual void PageStart()
        {
            gameObject.SetActive(true);
            MLog.D("PageResume");
        }
        public virtual void PageResume()
        {
            MLog.D("PageResume");
        }

        public virtual void PagePause()
        {
            MLog.D("PagePause");
        }

        public virtual void PageStop()
        {
            MLog.D("PageStop");
            gameObject.SetActive(false);
        }

        public virtual void PageDestory()
        {
            MLog.D("PageDestory");
        }

        public bool IsPopupWindow()
        {
            return isPopupWindow;
        }




    }
}
