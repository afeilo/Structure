using Assets.FrameWork.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.FrameWork.Resources.simulate
{
    public class Request
    {

        public string name;
        public string path;
        public AssetBundle ab;
        public Object obj;
        public string[] dependencies;
        public LoadAssetCallbacks callback;
        public Request(string name, string path)
        {
            this.name = name;
            this.path = path;
        }
    }
}

