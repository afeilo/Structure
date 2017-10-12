using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Runtime
{
    /// <summary>
    /// 工具类
    /// </summary>
    class UUtils
    {
        private static String streamingAssetsPath = Application.streamingAssetsPath;
        /// <summary>
        /// 获取StreamingAssets 路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetStreamingAssets(string name)
        {
            return Path.Combine(streamingAssetsPath, name);
        }
    }
}
