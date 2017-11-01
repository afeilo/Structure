using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 资源加载父类，定义资源加载的接口
 */
namespace Assets.FrameWork
{
    public abstract class IAssetLoader
    {
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="assetName"></param>
        /// <param name="callback"></param>
        public abstract void LoadAsset(string bundleName, string assetName,string[] dependencies, LoadAssetCallbacks callback,REQUEST_TYPE request = REQUEST_TYPE.FILE);
    }
    public enum REQUEST_TYPE
    {
        FILE,
        HTTP
    }
}
