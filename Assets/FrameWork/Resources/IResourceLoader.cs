﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Assets.FrameWork.Resources
{
    /// <summary>
    /// 用来做资源加载
    /// </summary>
    public interface IResourceLoader
    {
        /// <summary>
        /// 加載AssetBundle
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        void LoadAsset(string name, LoadAssetCallbacks callback);

        /// <summary>
        /// 加載場景
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        void LoadScene(string name, LoadSceneCallbacks callback);

    }
    
}
