using Assets.FrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Runtime
{
    public class Request
    {
        public string bundleName;
        public string assetName;
        public string path;
        public string[] dependencies;
        public REQUEST_TYPE requestType;

        public System.DateTime beginQueueTime;
        public System.DateTime beginLoadTime;

        public Request(string bundleName, string assetName, string[] dependencies, REQUEST_TYPE requestType)
        {
            this.bundleName = bundleName;
            this.assetName = assetName;
            this.dependencies = dependencies;
            this.requestType = requestType;
#if DEBUG_ASSET
            beginQueueTime = System.DateTime.Now;
            beginLoadTime = System.DateTime.Now; 
#endif

        }

    }
   
}

