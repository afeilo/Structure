using System;
using UnityEngine;
namespace Assets.Framework
{
    public abstract class BaseAssetBundleLoader
    {
        protected ISpawnPool<AssetBundleCache> abPool = new SpawnPool<AssetBundleCache, AssetBundle>();

        public abstract void LoadAssetBundle(string name, Action<AssetBundle> success, Action err);

        public AssetBundleCache GetAssetBundle(string name)
        {
            var abCache = abPool.Spawn(name);
            if (null != abCache)
            {
                return abCache;
            }
            return null;
        }

        public void UnloadAssetBundle(string name)
        {
            abPool.Remove(name);
        }

        public class AssetBundleCache : ABBaseObject<AssetBundle>
        {
            public AssetBundleCache(string name, AssetBundle target)
                : base(name, target)
            {
            }
            public override void Release()
            {

            }
        }
    }
}

