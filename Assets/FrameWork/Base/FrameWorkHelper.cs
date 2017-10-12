using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork
{
    public class FrameWorkHelper
    {
        private static Dictionary<string, FrameWorkModule> m_FrameworkModules = new Dictionary<string, FrameWorkModule>();

        public static T getModule<T>() where T : FrameWorkModule {
            Type type = typeof(T);

            return getModule(type) as T;
        }
        private static FrameWorkModule getModule(Type type)
        {
            FrameWorkModule module;
            m_FrameworkModules.TryGetValue(type.Name, out module);
            if (module != null)
                return module;
            return createModule(type);
        }
        private static FrameWorkModule createModule(Type type)
        {
            FrameWorkModule frameWorkModule = (FrameWorkModule)Activator.CreateInstance(type);
            m_FrameworkModules.Add(type.Name, frameWorkModule);
            return frameWorkModule;

        }
    }
}
