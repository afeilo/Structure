using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downloadTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		StartCoroutine (downloadTask ());

	}


	public void download(){
		
	}

	private IEnumerator downloadTask(){
		Downloader.DownloadRequest request = Downloader.getInstance ().AddDownload ("http://182.135.76.3/bigota.d.miui.com/tools/MiPhone20141107.exe", Application.streamingAssetsPath + "/5.exe");
		yield return request;
		Debug.Log ("downloadSuccess");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
