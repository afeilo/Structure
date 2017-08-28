using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
	private string path;
	// Use this for initialization
	void Start () {
		path = Application.streamingAssetsPath;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void load(){
		StartCoroutine (loadAB ("prefab-1"));
	}
	IEnumerator loadAB(string name){
//		WWW www1 = new WWW (@"file://"+path+"/"+"texture-1");
//		yield return www1;
//		AssetBundle ab1 = www1.assetBundle;
//		ab1.LoadAllAssets ();
		Debug.Log (path);
		WWW www = new WWW (@"file://"+path+"/"+name);
		yield return www;
		AssetBundle ab = www.assetBundle;
		Object[] objs = ab.LoadAllAssets ();
		Instantiate (objs [0]);
	}
}
