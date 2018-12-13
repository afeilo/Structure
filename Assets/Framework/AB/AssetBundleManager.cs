using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Framework
{
    public class AssetBundleManager : Singleton<AssetBundleManager>
    {
        private BaseAssetBundleLoader assetBundleLoader;
        private BaseAssetLoader assetLoader;
        private AssetCheckDependency assetDependency;
        public bool SimulateAssetBundleInEditor = false;
        public AssetBundleManager()
        {
            assetBundleLoader = new AssetBundleLoader();
            assetLoader = new AssetLoader();
            assetDependency = new AssetCheckDependency();
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
            string[] dependecies = assetDependency.GetDependencies(ALG.EncodeBundleName(name));
            for (int i = 0; i < dependecies.Length; i++)
            {
                RealUnloadAssetWithName(dependecies[i]);
            }
            var abCache = assetBundleLoader.GetAssetBundle(name);
            abCache.RemoveReference();
            if (0 >= abCache.ReferencesCount) {
                assetBundleLoader.UnloadAssetBundle(name);
                assetLoader.UnloadAsset(name);
                Debug.Log("UnloadAssetBundle  "+name);
                abCache.Target.Unload(true);
            }
            
        }

        IEnumerator RealLoadAssetGroup(string[] name, Action<UnityEngine.Object[]> comp)
        {
            AssetTask[] taskArray = new AssetTask[name.Length];
            UnityEngine.Object[] objects = new UnityEngine.Object[name.Length];
            for (int i = 0; i < name.Length; i++)
            {
                AssetTask task = new AssetTask(name[i], (UnityEngine.Object o) =>
                {
                    objects[i] = o;
                }, null);
                taskArray[i] = task;
                RealLoadAsset(task);
            }
            int j = 0;
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

        public void LoadAsset(string name, Action<UnityEngine.Object> success, Type type = null) 
        {
            AssetTask assetTask = new AssetTask(name, success,type);
            new Task(RealLoadAsset(assetTask), "AssetBundleManager_"+name);
        }

        IEnumerator RealLoadAssetBundle(AssetTask assetTask)
        {
            assetTask.name = ALG.DecodeBundleName(assetTask.name);
            string[] dependecies = assetDependency.GetDependencies(ALG.EncodeBundleName(assetTask.name));
            AssetTask[] taskArray = new AssetTask[dependecies.Length];
            for (int i = 0; i < dependecies.Length; i++)
            {
                AssetTask task = new AssetTask(dependecies[i], null, null);
                taskArray[i] = task;
                new Task(RealLoadAssetBundle(task), "AssetBundleManager_" + task.name);
            }
            int j = 0;
            while (j < taskArray.Length)
            {
                if (taskArray[j].isFinish)
                    j++;
                else
                    yield return null;
            }
            assetBundleLoader.LoadAssetBundle(assetTask.name, (AssetBundle ab) =>
            {
                assetBundleLoader.GetAssetBundle(assetTask.name).AddReference();
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
            if (SimulateAssetBundleInEditor)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.01f, 0.03f));//模拟加载时间
                var t = assetTask.type;
                if (null != t && t == typeof(Sprite))
                {
                    //做精灵加载
                    //string atlasName = AtlasRelative.instance.GetAtlasName(name);
                    //if (atlasName != null)
                    //{
                    //    string spriteRoot = "Assets/CResource/Sprite/";
                    //    List<string> files = new List<string>();
                    //    files.AddRange(Directory.GetFiles(spriteRoot + atlasName, name + ".png", SearchOption.AllDirectories));
                    //    files.AddRange(Directory.GetFiles(spriteRoot + atlasName, name + ".jpg", SearchOption.AllDirectories));
                    //    if (files.Count != 1) Debug.LogError("Simulate can not find " + key);
                    //    if (files.Count > 0)
                    //    {
                    //        Object o = UnityEditor.AssetDatabase.LoadAssetAtPath(files[0], t);
                    //        comp(o);
                    //    }
                    //    else
                    //        error();
                    //    yield break;
                    //}
                }


                //string fn = AssetBundleManager.GetHexABName(key);
                string[] fns = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(assetTask.name);
                if (fns.Length != 1) Debug.LogError("Simulate can not find " + assetTask.name);
                if (fns.Length > 0)
                {
                    UnityEngine.Object o = UnityEditor.AssetDatabase.LoadAssetAtPath(fns[0],typeof(UnityEngine.Object));
                    assetTask.success(o);
                }
                //else
                //    error();
                yield break;
            }
#endif
            yield return RealLoadAssetBundle(assetTask);
            var abCache = assetBundleLoader.GetAssetBundle(assetTask.name);
            assetLoader.LoadAsset(assetTask.name, abCache.Target, (UnityEngine.Object o) =>
            {
                assetTask.isFinish = true;
                assetTask.success(o);
            }, null);
        }


        public class AssetTask {
            public string name;
            public bool isFinish = false;
            public Action<UnityEngine.Object> success;
            public Type type;
            public AssetTask(string name, Action<UnityEngine.Object> success,Type type)
            {
                this.name = name;
                this.success = success;
                this.type = type;
                isFinish = false;
            }
        }
    }
}
