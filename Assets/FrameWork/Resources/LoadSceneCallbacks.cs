using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork.Resources
{
    public sealed class LoadSceneCallbacks
    {
        public delegate void loadSceneSuccess(string abname,Object obj);
        public delegate void loadSceneFail(string abname, string errorMessage);
    }
}
