using Assets.FrameWork.Resources.cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork
{
    class ResourceObject : BaseObject
    {
        IResourceHelper m_ResourceHelper; 
        public ResourceObject(IResourceHelper resourceHelper,string name,object target) : base(name,target) {
            m_ResourceHelper = resourceHelper;
        }
        public override void Release() {
            m_ResourceHelper.Release(Target);
        }
    }
}
