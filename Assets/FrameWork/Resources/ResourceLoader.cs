using Assets.FrameWork.Resources.cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork{
    public class ResourceLoader:IResourceLoader
    {
        private ICheckDependency dependency;
        private IAssetLoader assetLoader;

        public ResourceLoader(ICheckDependency dependency, IAssetLoader assetLoader) {
            this.dependency = dependency;
            this.assetLoader = assetLoader;
        }


        /// <summary>
        /// 加載資源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public void LoadAsset(string name, LoadAssetCallbacks callback) {

            string[] dependencies = dependency.GetDependencies(name);
            if (dependencies != null)
            {
                for (int i = 0, len = dependencies.Length; i < len; i++)
                {
                    LoadAsset(dependencies[i], null);
                }
            }
          
            assetLoader.LoadAsset(name, callback);
            //TODO 加载
        }



        /// <summary>
        /// 加載場景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public  void LoadScene(string name, LoadSceneCallbacks callback) { 
        
        }
    }
}
