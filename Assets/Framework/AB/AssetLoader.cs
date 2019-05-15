using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    public class AssetLoader : BaseAssetLoader
    {

        public override void LoadAsset(string name, AssetBundle assetBundle, Action<UnityEngine.Object> success, Action err, Type type)
        {
            var assetCache = assetPool.Spawn(name);
            if (null == assetCache)
            {
                assetCache = new AssetCache(name, null);
                assetPool.Add(name, assetCache);
            }
            else if (!assetCache.isLock)
            {
                success(assetCache.Target);
                return;
            }
            assetCache.assetBundle = assetBundle;
            assetCache.LoadAssetSuccessCallback += success;
            assetCache.LoadAssetFailCallback += err;
            if (!assetCache.isLock)
            {
                assetCache.isLock = true;
                var task = TaskManager.CreateTask(RealLoadAssetBundle(assetCache,type), "Asset_" + name);
                task.Start();
            }
        }

        IEnumerator RealLoadAssetBundle(AssetCache assetCache, Type type)
        {
            AssetBundleRequest assetLoadRequest;
            if (type == typeof(Sprite))
            {
                assetLoadRequest = assetCache.assetBundle.LoadAssetAsync<Sprite>(assetCache.Name);
            }
            else
            {
                assetLoadRequest = assetCache.assetBundle.LoadAssetAsync(assetCache.Name);
            }
            
            yield return assetLoadRequest;
            assetCache.Target = assetLoadRequest.asset;
            assetCache.LoadAssetSuccessCallback(assetLoadRequest.asset);
            assetCache.isLock = false;
        }
        
        
    }
}