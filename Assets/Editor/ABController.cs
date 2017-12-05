using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class ABController{
	private static string path = Application.streamingAssetsPath;
	[MenuItem("AssetBundle/make")]
	public static void ab(){
		if (Directory.Exists (path)) {
            //Directory.Delete (path);
            //File.Delete(path);
			//Directory.CreateDirectory (path);
		} else {
			Directory.CreateDirectory (path);
		}
		BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
		VersionFile versionFile = VersionFile.CreateInstance<VersionFile> ();
		versionFile.abInfos = new List<VersionFile.ABInfo> ();
		AssetDatabase.CreateAsset (versionFile, "Assets/VersionFile.asset");
		AssetImporter.GetAtPath("Assets/VersionFile.asset").assetBundleName = "VersionFile";
		string[] filesPath = Directory.GetFiles (path);
		for (int i = 0; i < filesPath.Length; i++) {
			string filepath = filesPath [i];
			if (filepath.EndsWith (".meta") || filepath.EndsWith (".manifest"))
				continue;
			FileInfo fileInfo = new FileInfo (filepath);
			VersionFile.ABInfo abinfo = new VersionFile.ABInfo ();
			abinfo.bundleName = fileInfo.Name;
			abinfo.size = fileInfo.Length;
			abinfo.crc = fileInfo.Length.ToString();
			versionFile.abInfos.Add (abinfo);
		}

	}
}
