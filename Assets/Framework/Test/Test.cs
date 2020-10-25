using Assets.Framework;
using System;
using UnityEngine;

public class Test : MonoBehaviour {
	void Start () {
        
        UIMng.getInstance().Goto("TestController");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
