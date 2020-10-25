
using System;

namespace Assets.Framework
{
    public class Loader
    {

        public static void LoadAsset(string name, Action<UnityEngine.Object> comp, Action error,Type type = null)
        {
            AssetBundleManager.getInstance().LoadAsset(name, comp,error, type);
        }

        public static void LoadAsset(string name, string assetName, Action<UnityEngine.Object> comp, Action error, Type type = null)
        {
            AssetBundleManager.getInstance().LoadAsset(name, assetName, comp, error, type);
        }

        public static void LoadAssetGroup(string[] names,Action<UnityEngine.Object[]> comp){
            AssetBundleManager.getInstance().LoadAssetGroup(names, comp);
        }
        public static void UnLoadAssetWithName(string name) {
            AssetBundleManager.getInstance().UnLoadAssetWithName(name);
        }

        public static object LoadHttp(string url, Action<Object> comp, Action error)
        {
            return null;
        }

    }
}
