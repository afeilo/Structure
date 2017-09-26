using Assets.FrameWork.Resources.cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork.Resources.simulate
{
    public class ResourceLoader:IResourceLoader
    {
        private ICheckDependency<DependecyInfo> dependency;
        private IAssetLoader assetLoader;
        private static Dictionary<string, Request> loadingList = new Dictionary<string, Request>();
        private ISpawnPool<ResourceObject> resourcePool;

        public void Init() { 
        
        }

        /// <summary>
        /// 加載資源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public void LoadAsset(string name, LoadAssetCallbacks callback) {
            string[] dependencies = dependency.GetDependencies(name);

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
