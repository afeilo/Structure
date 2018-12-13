using Assets.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIMng.getInstance().Goto("TestController");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
