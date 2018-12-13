using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Framework
{
    /// <summary>
    /// 依赖信息
    /// </summary>
    public interface ICheckDependency
    {
        /// <summary>
        /// 添加依賴
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dependencies"></param>
        void AddDependencies(string name, string[] dependencies);

        /// <summary>
        /// 查找依賴
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string[] GetDependencies(string name);
    }
    
}
