using Assets.FrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Runtime
{
    public class UIStateView : MonoBehaviour, IStateView
    {

        // Use this for initialization
        private void Start()
        {
            MLog.D("Start");
        }

        public virtual void PageCreate()
        {
            MLog.D("PageCreate");
        }

        public virtual void PagePause()
        {
            MLog.D("PagePause");
        }

        public virtual void PageResume()
        {
            MLog.D("PageResume");
            gameObject.SetActive(true);
        }

        public virtual void PageStart()
        {
            MLog.D("PageResume");
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
            return false;
        }




    }
}
