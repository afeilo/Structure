using Assets.FrameWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Runtime
{
    class AssetCheckDependency : ICheckDependency
    {
        Dictionary<string,DependecyInfo> dependenacies = new Dictionary<string,DependecyInfo>();
        private AssetBundleManifest abManifest;
        public AssetCheckDependency() {

            loadManifest();
        }
        /// <summary>
        /// 加载manifest
        /// </summary>
        private void loadManifest()
        {
            MLog.D("loadManifest");
            var bundleLoadRequest = AssetBundle.LoadFromFile(UUtils.GetStreamingAssets("StreamingAssets"));
            abManifest = bundleLoadRequest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        /// <summary>
        /// 添加依賴
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dependencies"></param>
        public void AddDependencies(string name, string[] dependencies) { 
        
        }

        /// <summary>
        /// 查找依賴
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string[] GetDependencies(string name) {
            return abManifest.GetAllDependencies(name);
        }
    }
}
