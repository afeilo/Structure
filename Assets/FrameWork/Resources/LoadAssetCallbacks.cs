using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork.Resources
{
    public class LoadAssetCallbacks
    {
        public delegate void loadAssetSuccess(string abname, Object obj);
        public delegate void loadAssetFail(string abname, string errorMessage);

        public readonly loadAssetSuccess m_LoadAssetSuccessCallback;
        public readonly loadAssetFail m_LoadAssetFailCallback;

        public loadAssetSuccess LoadAssetSuccessCallback
        {
            get
            {
                return m_LoadAssetSuccessCallback;
            }
        }

        public loadAssetFail LoadFailCallback
        {
            get
            {
                return m_LoadAssetFailCallback;
            }
        }

        public LoadAssetCallbacks(loadAssetSuccess loadAssetSuccessCallback,loadAssetFail loadAssetFailCallback) {
            m_LoadAssetSuccessCallback = loadAssetSuccessCallback;
            m_LoadAssetFailCallback = loadAssetFailCallback;
        }
    }
}
