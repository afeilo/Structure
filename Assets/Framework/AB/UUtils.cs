using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Framework
{
    /// <summary>
    /// 工具类
    /// </summary>
    class UUtils
    {
        private static String streamingAssetsPath = Application.streamingAssetsPath;
        private static String remoteAssetsPath = "http://192.168.99.230:8080/ab";
        /// <summary>
        /// 获取StreamingAssets 路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetStreamingAssets(string name)
        {
            return Path.Combine(streamingAssetsPath, name);
        }

        /// <summary>
        /// 获取网络地址
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetRemoteAssets(string name)
        {
            return Path.Combine(remoteAssetsPath, name);
        }
    }
}
