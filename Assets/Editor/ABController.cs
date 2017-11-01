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
	}
}
