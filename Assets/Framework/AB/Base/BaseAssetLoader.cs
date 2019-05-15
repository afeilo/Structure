using System;
using UnityEngine;

namespace Assets.Framework
{
    public abstract class BaseAssetLoader
    {
        /// <summary>
        /// 缓存池
        /// </summary>
        protected ISpawnPool<AssetCache> assetPool = new SpawnPool<AssetCache, UnityEngine.Object>();

        /// <summary>
        /// 正儿八经加载方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="assetBundle"></param>
        /// <param name="success"></param>
        /// <param name="err"></param>
        public abstract void LoadAsset(string name, AssetBundle assetBundle, Action<UnityEngine.Object> success, Action err, Type type);

        public AssetCache GetAsset(string name)
        {
            var assetCache = assetPool.Spawn(name);
            if (null != assetCache)
            {
                return assetCache;
            }
            return null;
        }

        public void UnloadAsset(string name)
        {
            assetPool.Remove(name);
        }

        /// <summary>
        /// Cache类
        /// </summary>
        public class AssetCache : ABBaseObject<UnityEngine.Object>
        {
            public AssetBundle assetBundle;
            public AssetCache(string name, UnityEngine.Object target)
                : base(name, target)
            {
            }
            public override void Release()
            {

            }
        }
    }
}