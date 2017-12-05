using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Runtime
{
    /// <summary>
    /// 注册组件
    /// </summary>
    class ModuleEntry
    {
        private static Dictionary<string, ComponentModule> m_FrameworkModules = new Dictionary<string, ComponentModule>();

        public static T GetModule<T>() where T : ComponentModule
        {
            Type type = typeof(T);

            return getModule(type) as T;
        }
        private static ComponentModule getModule(Type type)
        {
            ComponentModule module;
            m_FrameworkModules.TryGetValue(type.Name, out module);
            if (module != null)
                return module;
            return null;
        }
        public static void RejestModule(ComponentModule module)
        {
            m_FrameworkModules.Add(module.GetType().Name, module);
        }
    }
}
