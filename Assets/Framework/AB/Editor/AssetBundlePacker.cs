using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundlePacker{
    const string menuRoot = "AssetBundle/";
    const string menuAssetsRoot = "Assets/AssetBundle/";
    static BuildAssetBundleOptions babo = BuildAssetBundleOptions.DeterministicAssetBundle;

    [MenuItem(menuRoot + "AssetBundle Build &b")]
    public static void BuildAssetBundle()
    {
        QuickAcessUtils.CheckDirectory(FilePathDefine.projectExportPath, false);
        string p = FilePathDefine.projectExportPath;
        BuildTarget bt = EditorUserBuildSettings.activeBuildTarget;
        Debug.Log(p);
        BuildPipeline.BuildAssetBundles(p, babo, bt);
    }

    public static void BuildAssetBundle(string path)
    {
        AssetBundleBuild abb = new AssetBundleBuild();
        abb.assetNames = new string[] { path };
        abb.assetBundleName = ALG.EncodeHexString(Path.GetFileNameWithoutExtension(path)) + ".ab";
        BuildPipeline.BuildAssetBundles(FilePathDefine.projectExportPath, new AssetBundleBuild[] { abb }, babo, EditorUserBuildSettings.activeBuildTarget);
    }

    [MenuItem(menuAssetsRoot + "Set AssetBundle Name", false, 1)]
    public static void SetAssetBundleName()
    {
        Object[] os = Selection.objects;
        for (int i = 0; i < os.Length; i++)
        {
            SetAssetBundleName(AssetDatabase.GetAssetPath(os[i]));
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem(menuAssetsRoot + "Clear AssetBundle Name", false, 2)]
    public static void ClearAssetBundleName()
    {
        Object[] os = Selection.objects;
        for (int i = 0; i < os.Length; i++)
        {
            SetAssetBundleName(AssetDatabase.GetAssetPath(os[i]), "");
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public static void SetAssetBundleName(string path)
    {
        string n = Path.GetFileNameWithoutExtension(path);
        n = ALG.EncodeBundleName(n);
        SetAssetBundleName(path, n);
    }

    public static void SetAssetBundleName(string path, string n)
    {
        AssetImporter ai = AssetImporter.GetAtPath(path);
        ai.assetBundleName = n;
        Debug.LogFormat("Set <color=#00ff00>[{0}]</color> Assetbundle Name as :  <color=#ffff00>[{1}]</color>", path, n);
    }

    public static void SetAssetBundleName(AssetImporter ai)
    {
        string n = Path.GetFileNameWithoutExtension(ai.assetPath);
        n = ALG.EncodeBundleName(n);
        ai.assetBundleName = n;
    }

}
