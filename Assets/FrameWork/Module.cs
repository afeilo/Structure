using Assets.FrameWork.Resources;
using Assets.FrameWork.Resources.simulate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork
{
    public class Module
    {
        public static IAssetLoader getAssetLoader() {
            return new ABLoader();
        }
    }
}
