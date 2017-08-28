using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 资源加载父类，定义资源加载的接口
 */

public abstract class ILoader{
	public abstract IEnumerator LoadAssets (Request request,Transform parent);
}
