using Assets.FrameWork;
using UnityEngine;

namespace Assets.Runtime
{
    class ResourceComponent : MonoBehaviour
    {
        private ResourceManager m_ResourceManager;
        private void Awake()
        {
            m_ResourceManager = FrameWorkHelper.getModule<ResourceManager>();
            m_ResourceManager.SetResourceLoader(new ResourceLoader(new AssetCheckDependency(),new AssetLoader()));
        }
    }
}
