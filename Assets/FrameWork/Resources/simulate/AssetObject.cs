using Assets.FrameWork.Resources.cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.FrameWork.Resources.simulate
{
    class AssetObject:BaseObject
    {
        AssetBundle ab;
        public AssetObject(AssetBundle ab, string name, object target)
            : base(name, target)
        {
            this.ab = ab;
        }
        public override void Release() {
            ab.Unload(true);
        }
    }
}
