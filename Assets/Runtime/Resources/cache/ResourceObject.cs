using Assets.FrameWork;
using System;

namespace Assets.Runtime
{
    class ResourceObject<T> : BaseObject<T>
    {
        IResourceHelper<T> m_ResourceHelper;
        public ResourceObject(string name, T target, IResourceHelper<T> resourceHelper)
            : base(name, target)
        {
            m_ResourceHelper = resourceHelper;
        }
        public override void Release() {
            m_ResourceHelper.Release(Target);
        }
    }
}
