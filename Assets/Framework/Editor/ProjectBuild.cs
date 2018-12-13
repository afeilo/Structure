
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
public class ProjectBuild  {

    public static string scene
    {
        get
        {
            string s = ProjectBuildUtils.GetConsoleArgsByName("scene");
            if (s == "")
            {
                return "Assets/Scene/first_loading.unity";
            }
            else
            {
                return s;
            }
        }
    }
    public static void BuildWindows()
    {
        BuildPlayer();
    }

    static void BuildForAndroidIL2CPP()
    {
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        BuildPlayer();

    }

    //public static void BuildIOS()
    //{
    //    PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, bundleID);//测试用
    //    Debug.Log("CURRENT BUNDLE " + PlayerSettings.applicationIdentifier);
    //    BuildPlayer();
    //}
    static void BuildPlayer()
    {
        Debug.Log("Build is start" + System.DateTime.Now);
        string[] scenes = new string[] { scene };

#if !RELEASE
        EditorSceneManager.OpenScene(scene, OpenSceneMode.Single);
        GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/TapEnjoy/ZPlugins/GMPlugin/GMPlugin.prefab")).SetActive(true);
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
#endif

#if UNITY_ANDROID
        AndroidSettings();
        string to = "../android/game.apk";
        QuickAcessUtils.CheckDirectory(Path.GetDirectoryName(to));
        BuildPipeline.BuildPlayer(scenes, to, BuildTarget.Android, BuildOptions.None);
#elif UNITY_IOS
        string to = "../ios";
        to = Path.GetFullPath(to);
        QuickAcessUtils.CheckDirectory(to);
        BuildPipeline.BuildPlayer(scenes, to, BuildTarget.iOS, BuildOptions.None);
#else
        string to = "../win/game.exe";
        QuickAcessUtils.CheckDirectory(Path.GetDirectoryName(to));
        BuildPipeline.BuildPlayer(scenes, to, BuildTarget.StandaloneWindows, BuildOptions.None);
#endif
    }


    static void AndroidSettings(string appExtension = "il2cpp")
    {
        //string keystorePath = Application.dataPath + "";
        //PlayerSettings.Android.keystoreName = "";
        //PlayerSettings.Android.keystorePass = "";
        //PlayerSettings.Android.keyaliasName = "";
        //PlayerSettings.Android.keyaliasPass = "";
    }
}
