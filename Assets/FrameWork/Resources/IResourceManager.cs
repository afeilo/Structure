using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.FrameWork
{
    public interface IResourceManager
    {
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loadAssetCallBack"></param>
        void LoadAsset(string name,LoadAssetCallbacks loadAssetCallBack);
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loadAssetCallBack"></param>
        /// <param name="userData"></param>
        void LoadAsset(string name, LoadAssetCallbacks loadAssetCallBack, object userData);
        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="asset"></param>
        void UnloadAsset(object asset);
        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">要加载场景资源的名称。</param>
        /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
        void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks);

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">要加载场景资源的名称。</param>
        /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
        /// <param name="userData">用户自定义数据。</param>
        void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData);
       /// <summary>
       /// 设置ResourceLoader
       /// </summary>
       /// <param name="resourceLoader"></param>
        void SetResourceLoader(IResourceLoader resourceLoader);
    }
}
