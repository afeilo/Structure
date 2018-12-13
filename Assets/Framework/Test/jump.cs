using Assets.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void jump2()
    {
        UIMng.getInstance().Goto(Type.GetType("TestController2"));
    }
}
