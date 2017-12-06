using Assets.FrameWork;
using System;
using UnityEngine;
namespace Assets.Runtime
{
    class ResourceComponent : ComponentModule
    {
        private ResourceManager m_ResourceManager;
        protected override void Awake()
        {
            base.Awake();
            m_ResourceManager = FrameWorkHelper.getModule<ResourceManager>();
            m_ResourceManager.SetResourceLoader(new ResourceLoader(new AssetCheckDependency(),new AssetLoader()));
        }
        public void LoadAsset(string bundleName,string assetName, LoadAssetCallbacks loadAssetCallBack) {
            m_ResourceManager.LoadAsset(bundleName, assetName, loadAssetCallBack, null);
        }
        public void LoadAsset(string bundleName, string assetName, 
            Assets.FrameWork.LoadAssetCallbacks.loadAssetSuccess loadAssetSuccessCallback,
            Assets.FrameWork.LoadAssetCallbacks.loadAssetFail loadAssetFailCallback)
        {
            m_ResourceManager.LoadAsset(bundleName, assetName, new LoadAssetCallbacks(loadAssetSuccessCallback, loadAssetFailCallback), null);
        }
    }
}
