using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileLoader : MonoBehaviour{
	private static string path = Application.streamingAssetsPath;
	public void Load(string name){
		Request request = GetRequest (name);
		StartCoroutine(new WWWLoader ().LoadAssets (request,null));
	}
	public Request GetRequest(string name){
		return new Request (name, @"file://"+path + "/" + name);
	}

	public class WWWLoader : ILoader{
		public override IEnumerator LoadAssets(Request request,Transform parent){
			WWW www = new WWW (request.path);
			yield return www;
			request.ab = www.assetBundle;
			//Object obj = www.assetBundle.mainAsset;
			Object[] objs = www.assetBundle.LoadAllAssets();
			yield return objs;
			request.obj = objs[0];
			GameObject clone = Instantiate (request.obj) as GameObject;
			clone.name = request.name;
			if (parent) {
				clone.transform.parent = parent;
			}
		}
	}
}
