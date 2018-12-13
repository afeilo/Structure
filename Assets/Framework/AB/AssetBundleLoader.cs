using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Assets.Framework
{
    public class AssetBundleLoader : BaseAssetBundleLoader
    {

        public override void LoadAssetBundle(string name, Action<AssetBundle> success, Action err)
        {
            
            var abCache = abPool.Spawn(name);
            
            if (null == abCache)
            {
                abCache = new AssetBundleCache(name, null);
                abPool.Add(name,abCache);
            }
            else if(!abCache.isLock)
            {
                success(abCache.Target);
                return;
            }
            abCache.LoadAssetSuccessCallback += success;
            abCache.LoadAssetFailCallback += err;
            if (!abCache.isLock) {
                abCache.isLock = true;
                var task = TaskManager.CreateTask(RealLoadAssetBundle(abCache), "AssetBundle_" + name);
                task.Start();
            }
        }

        IEnumerator RealLoadAssetBundle(AssetBundleCache abCache)
        {            
            foreach(string path in AssetBundlePathDefine.loadedPath){
                var _path = Path.Combine(path, ALG.EncodeBundleName(abCache.Name));
                if (File.Exists(_path)) {
                    var bundleLoadRequest = AssetBundle.LoadFromFileAsync(_path);
                    yield return bundleLoadRequest;
                    var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
                    abCache.Target = myLoadedAssetBundle;
                    abCache.LoadAssetSuccessCallback(myLoadedAssetBundle);
                    abCache.isLock = false;
                    break;
                }
            }
        }

       
    }
}