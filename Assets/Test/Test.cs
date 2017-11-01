using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var now = System.DateTime.Now;
        Debug.Log((System.DateTime.Now - now).TotalSeconds);
        now = System.DateTime.Now;
        Debug.Log((System.DateTime.Now - now).TotalSeconds);
        now = System.DateTime.Now;
        Debug.Log((System.DateTime.Now - now).TotalSeconds);
        now = System.DateTime.Now;
        Debug.Log((System.DateTime.Now - now).TotalSeconds);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
