using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request{

	public string name;
	public string path;
	public AssetBundle ab;
	public Object obj;
    public string[] dependencies;
    public System.Action<Object> onComplete;
	public Request(string name,string path){
		this.name = name;
		this.path = path;
	}
}
