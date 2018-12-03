using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelper : MonoBehaviour {
	private static GameHelper instance;

	public static GameHelper GetInstance(){
		if (null == instance){
			GameObject go = new GameObject("GameHelper");
			instance = go.AddComponent<GameHelper>();
		}
		return instance;
	}

	public GameObject InstantiateObject(Object o){
		return Instantiate(o) as GameObject;
	}
}
