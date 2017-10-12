using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 资源加载父类，定义资源加载的接口
 */
namespace Assets.FrameWork
{
    public abstract class ISceneLoader
    {
        public abstract void LoadScene(string request);
    }
}
