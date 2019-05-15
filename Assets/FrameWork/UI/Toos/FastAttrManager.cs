using System.Reflection;
using UnityEngine;

namespace Assets.Framework
{
    public class FAM {
        public static void bind(object obj,IMediator mediator)
        {
            foreach (FieldInfo fieldInfo in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(FastObtainAttribute), true);
                if (1 == attrs.Length)
                {
                    FastObtainAttribute attr = attrs[0] as FastObtainAttribute;
                    var name = attr.monoName!=null ? attr.monoName : fieldInfo.Name;
                    fieldInfo.SetValue(obj,mediator.Get(name));
                }

            }
        }
    }
}