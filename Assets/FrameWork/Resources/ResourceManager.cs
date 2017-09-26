using Assets.FrameWork.Resources.cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.FrameWork.Resources

{
    class ResourceManager : MonoBehaviour, IResourceManager
    {
        private IAssetLoader assetLoader;
        public static ResourceManager instance;

        void Awake(){
            //TODO 指定一个默认的加载器
            if (assetLoader == null) {
                assetLoader = Module.getAssetLoader();
            }
            instance = this;
        }



        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loadAssetCallBack"></param>
        public void LoadAsset(string name, LoadAssetCallbacks loadAssetCallBack) {
            this.LoadAsset(name, loadAssetCallBack, null);
        }
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loadAssetCallBack"></param>
        /// <param name="userData"></param>
        public void LoadAsset(string name, LoadAssetCallbacks loadAssetCallBack, object userData) {
            assetLoader.LoadAsset(name, loadAssetCallBack);

        }
        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="asset"></param>
        public void UnloadAsset(object asset) { 
        }
        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">要加载场景资源的名称。</param>
        /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks){
        }

        /// <summary>
        /// 异步加载场景。
        /// </summary>
        /// <param name="sceneAssetName">要加载场景资源的名称。</param>
        /// <param name="loadSceneCallbacks">加载场景回调函数集。</param>
        /// <param name="userData">用户自定义数据。</param>
        public void LoadScene(string sceneAssetName, LoadSceneCallbacks loadSceneCallbacks, object userData) { 
        
        }
    }
}
