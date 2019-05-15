using UnityEngine;
using System.Collections;
using System.IO;

public class FilePathDefine
{
#if UNITY_IOS
   public const string platform = "ios";
#elif UNITY_ANDROID
   public const string platform = "android";
#elif UNITY_OSX || UNITY_STANDALONE_OSX
   public const string platform = "ios";//standaloneosxintel
#else
    public const string platform = "windows";// standalonewindows
#endif
    public const string outTmpPath = "SlothOutput/AssetBundle/";

    public static string[] loadedPath = new string[] {
        deCompressPath,
        projectExportPath,
    };

    static string _platformHexName;
    static string platformHexName
    {
        get
        {
            if (_platformHexName == null)
                _platformHexName = ALG.EncodeHexString(platform);
            return _platformHexName;
        }
    }

    static string _editorExportPath;
    public static string editorExportPath
    {
        get
        {
            if (_editorExportPath == null)
                _editorExportPath = Path.Combine(outTmpPath, platformHexName);
            return _editorExportPath;
        }
    }

    static string _projectExportPath;
    public static string projectExportPath
    {
        get
        {
            if (_projectExportPath == null)
                _projectExportPath = Path.Combine(Application.streamingAssetsPath, platformHexName);
            return _projectExportPath;
        }
    }

    static string _deCompressPath;
    public static string deCompressPath
    {
        get
        {
            if (_deCompressPath == null)
                _deCompressPath = Path.Combine(Application.persistentDataPath, platformHexName);
            return _deCompressPath;
        }
    }

    static string _downloadTempPath;
    public static string downloadTmpPath
    {
        get
        {
            if (_downloadTempPath == null)
                _downloadTempPath = Path.Combine(Application.persistentDataPath, "download_tmp");
            return _downloadTempPath;
        }
    }

    static string _progressCachePath;
    public static string progressCachePath
    {
        get
        {
            if (_progressCachePath == null)
                _progressCachePath = GetHexPath(Application.persistentDataPath, "pro");
            return _progressCachePath;
        }
    }

    public static string GetHexPath(string dir,string name)
    {
        return Path.Combine(dir, name);
    }

    public static string configPath{

        get
        {
#if UNITY_STANDALONE && !UNITY_EDITOR
            return Path.Combine(Application.dataPath , "Config");
#elif UNITY_EDITOR
            return "Assets/AVG/config/files";
#endif
            return FilePathDefine.projectExportPath;
        }
    }

}
