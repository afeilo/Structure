using Assets.FrameWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework
{
    sealed class AssetCheckDependency : ICheckDependency
    {
        private AssetBundleManifest _abManifest;
        public AssetCheckDependency() {

            LoadManifest();
        }
        /// <summary>
        /// 加载manifest
        /// </summary>
        private void LoadManifest()
        {
             foreach(string path in FilePathDefine.loadedPath){
                var _path = Path.Combine(path, ALG.EncodeHexString(FilePathDefine.platform));
                if (File.Exists(_path))
                {
                    var bundleLoadRequest = AssetBundle.LoadFromFile(_path);
                    _abManifest = bundleLoadRequest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                    Debug.Log(_abManifest);
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
            if (null == _abManifest)
                return new string[0];
            return _abManifest.GetAllDependencies(name);
        }
        
    }

}
