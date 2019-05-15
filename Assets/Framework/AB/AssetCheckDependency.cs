using Assets.FrameWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework
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
             foreach(string path in FilePathDefine.loadedPath){
                var _path = Path.Combine(path, ALG.EncodeHexString(FilePathDefine.platform));
                if (File.Exists(_path))
                {
                    var bundleLoadRequest = AssetBundle.LoadFromFile(_path);
                    abManifest = bundleLoadRequest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                    Debug.Log(abManifest);
                    return;
                }
             }
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
            if (null == abManifest)
                return new string[0];
            return abManifest.GetAllDependencies(name);
        }

        /// <summary>
        /// 依賴信息
        /// </summary>
        public class DependecyInfo
        {
            /// <summary>
            /// 資源名
            /// </summary>
            public string name;
            /// <summary>
            /// 依賴列表
            /// </summary>
            public string[] dependencies;
            public DependecyInfo(string name)
                : this(name, null)
            {
            }
            public DependecyInfo(string name, string[] dependencies)
            {
                this.name = name;
                this.dependencies = dependencies;
            }
        }
    }

}
