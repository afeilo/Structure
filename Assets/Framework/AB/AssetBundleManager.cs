using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Framework
{
    public sealed class AssetBundleManager : MonoBehaviour
    {

        private static AssetBundleManager instance;
        public static AssetBundleManager getInstance()
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AssetBundleManager");
                instance = go.AddComponent<AssetBundleManager>();
                instance.Init();
            }
            return instance;
        }

        private BaseAssetBundleLoader _assetBundleLoader;
        private BaseAssetLoader _assetLoader;
        private AssetCheckDependency _assetDependency;

        private List<BaseAssetBundleLoader.AssetBundleCache> releaseList =
            new List<BaseAssetBundleLoader.AssetBundleCache>();
        public void Init()
        {
            _assetBundleLoader = new AssetBundleLoader();
            _assetLoader = new AssetLoader();
            _assetDependency = new AssetCheckDependency();
        }

        public void LoadAssetGroup(string[] name, Action<UnityEngine.Object[]> comp) {
            new Task(RealLoadAssetGroup(name,comp), "AssetBundleManager_" + name);
        }

        public void UnLoadAssetWithName(string name) {
            RealUnloadAssetWithName(name);
        }

        private void RealUnloadAssetWithName(string name)
        {

            name = ALG.DecodeBundleName(name);
            string[] dependencies = _assetDependency.GetDependencies(ALG.EncodeBundleName(name));
            for (int i = 0; i < dependencies.Length; i++)
            {
                RealUnloadAssetWithName(dependencies[i]);
            }
            var abCache = _assetBundleLoader.GetAssetBundle(name);
            if (null == abCache) return;
            abCache.RemoveReference();
            if (0 >= abCache.ReferencesCount)
            {
                releaseList.Add(abCache);
            }
            
        }

        IEnumerator RealLoadAssetGroup(string[] name, Action<UnityEngine.Object[]> comp)
        {
            var taskArray = new AssetTask[name.Length];
            var objects = new UnityEngine.Object[name.Length];
            for (var i = 0; i < name.Length; i++)
            {
                var task = new AssetTask(name[i], name[i], (UnityEngine.Object o) =>
                {
                    objects[i] = o;
                },null, null);
                taskArray[i] = task;
                RealLoadAsset(task);
            }
            var j = 0;
            while (j < taskArray.Length)
            {
                if (taskArray[j].isFinish)
                    j++;
                else
                    yield return null;
            }
            if (null != comp)
                comp(objects);

        }

        public void LoadAsset(string name, Action<UnityEngine.Object> success, Action error, Type type = null) 
        {
            AssetTask assetTask = new AssetTask(name,name, success,error,type);
            new Task(RealLoadAsset(assetTask), "AssetBundleManager_"+name);
        }

        public void LoadAsset(string name,string abName, Action<UnityEngine.Object> success, Action error, Type type = null)
        {
            AssetTask assetTask = new AssetTask(name, abName, success, error, type);
            new Task(RealLoadAsset(assetTask), "AssetBundleManager_" + name);
        }

        IEnumerator RealLoadAssetBundle(AssetTask assetTask)
        {
            assetTask.abName = ALG.DecodeBundleName(assetTask.abName);
            var dependencies = _assetDependency.GetDependencies(ALG.EncodeBundleName(assetTask.abName));
            var taskArray = new AssetTask[dependencies.Length];
            for (var i = 0; i < dependencies.Length; i++)
            {
                var task = new AssetTask(dependencies[i],null, null,null, null);
                taskArray[i] = task;
                new Task(RealLoadAssetBundle(task), "AssetBundleManager_" + task.abName);
            }
            var j = 0;
            while (j < taskArray.Length)
            {
                if (taskArray[j].isFinish)
                    j++;
                else
                    yield return null;
            }
            _assetBundleLoader.LoadAssetBundle(assetTask.abName, (AssetBundle ab) =>
            {
                _assetBundleLoader.GetAssetBundle(assetTask.abName).AddReference();
                assetTask.isFinish = true;
            }, null);
            while (!assetTask.isFinish)
            {
                yield return null;
            }
        }


        IEnumerator RealLoadAsset(AssetTask assetTask) 
        {
#if UNITY_EDITOR
            if (Config.simulateAssetBundleInEditor)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.03f));//模拟加载时间
                var t = assetTask.type;
                if (null != t && t == typeof(Sprite))
                {
                    //做精灵加载
                    if (assetTask.abName != null)
                    {
                        string spriteRoot = Config.spritePath;
                        List<string> files = new List<string>();
                        files.AddRange(Directory.GetFiles(spriteRoot + assetTask.abName + "/", assetTask.assetName + ".png", SearchOption.AllDirectories));
                        files.AddRange(Directory.GetFiles(spriteRoot + assetTask.abName + "/", assetTask.assetName  + ".jpg", SearchOption.AllDirectories));
                        if (files.Count != 1) Debug.LogError("Simulate can not find " + assetTask.assetName);
                        if (files.Count > 0)
                        {
                            UnityEngine.Object o = UnityEditor.AssetDatabase.LoadAssetAtPath(files[0], t);
                            assetTask.success(o);
                        }
                        else
                            assetTask.error();
                        yield break;
                    }
                }
                else
                {
                    //string fn = AssetBundleManager.GetHexABName(key);
                    //暂时修改方案
                    UnityEngine.Object o = UnityEditor.AssetDatabase.LoadAssetAtPath(Config.prefabPath + assetTask.abName + ".prefab", typeof(UnityEngine.Object));
                    assetTask.success(o);
                    //string[] fns = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(assetTask.name);

                    //if (fns.Length != 1) Debug.LogError("Simulate can not find " + assetTask.name);
                    //if (fns.Length > 0)
                    //{

                    //}

                    //else
                    //    error();
                    yield break;
                }
            }
                
#endif
            yield return RealLoadAssetBundle(assetTask);
            var abCache = _assetBundleLoader.GetAssetBundle(assetTask.abName);
            _assetLoader.LoadAsset(assetTask.assetName, abCache.Target, (UnityEngine.Object o) =>
            {
                assetTask.isFinish = true;
                assetTask.success(o);
            }, null,assetTask.type);

        }

        void LateUpdate()
        {
            for (int i = releaseList.Count - 1; i >= 0; i--)
            {
                var abCache = releaseList[i];
                if (!abCache.isLock)
                {
                    if (abCache.ReferencesCount <= 0)
                    {
                        _assetBundleLoader.UnloadAssetBundle(abCache.Name);
                        _assetLoader.UnloadAsset(abCache.Name);
                        abCache.Target.Unload(true);
                    }
                    releaseList.RemoveAt(i);
                }
                
            }
            
        }


        public class AssetTask {
            public string abName;
            public string assetName;
            public bool isFinish = false;
            public Action<UnityEngine.Object> success;
            public Action error;
            public Type type;
            public AssetTask(string abName,string assetName, Action<UnityEngine.Object> success, Action error, Type type)
            {
                this.abName = abName;
                this.assetName = assetName;
                this.success = success;
                this.error = error;
                this.type = type;
                isFinish = false;
            }
        }
    }
}
