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
        public void LoadAsset(string bundleName, string assetName, LoadAssetCallbacks callback)
        {

            List<string> dependencies = new List<string>();
            getDependencies(bundleName, dependencies);
            assetLoader.LoadAsset(bundleName, assetName, dependencies.ToArray(), callback);
            //TODO 加载
        }



        /// <summary>
        /// 加載場景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public  void LoadScene(string name, LoadSceneCallbacks callback) { 
        
        }

        private void getDependencies(string name,List<string> dependencies)
        {
            dependencies.Add(name);
            string[] dependence = dependency.GetDependencies(name);
            for (int i = 0, len = dependence.Length; i < len; i++)
            {
                getDependencies(dependence[i], dependencies);
            }
        }
    }
}
