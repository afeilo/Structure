﻿
using System;

namespace Assets.Framework
{
    public class Loader
    {

        public static void LoadAsset(string name, Action<UnityEngine.Object> comp, Action error)
        {
            AssetBundleManager.getInstance().LoadAsset(name, comp);
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
