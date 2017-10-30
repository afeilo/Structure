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

        public System.DateTime beginQueueTime;
        public System.DateTime beginLoadTime;

        public Request(string bundleName, string assetName, string path,string[] dependencies)
        {
            this.bundleName = bundleName;
            this.assetName = assetName;
            this.path = path;
            this.dependencies = dependencies;
            beginQueueTime = System.DateTime.Now;
            beginLoadTime = System.DateTime.Now; 
        }
    }
}

