﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 资源加载父类，定义资源加载的接口
 */
namespace Assets.FrameWork.Resources
{
    public abstract class IAssetLoader
    {
        public abstract void LoadAsset(string name,LoadAssetCallbacks callback);
    }
}
