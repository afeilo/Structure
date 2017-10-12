using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork
{
    /// <summary>
    /// 依賴信息
    /// </summary>
    public class DependecyInfo
    {
        /// <summary>
        /// 資源名
        /// </summary>
        public string name;
        /// <summary>
        /// 依賴列表
        /// </summary>
        public string[] dependencies;
        public DependecyInfo(string name) : this(name, null)
        {
        }
        public DependecyInfo(string name, string[] dependencies) {
            this.name = name;
            this.dependencies = dependencies;
        }
    }
}
