using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace Assets.Framework
{
    public class SingtonMng : MonoBehaviour, ISington
    {
        static Dictionary<Type, ISington> map = new Dictionary<Type, ISington>();

        public static bool Contains<T>() where T : MonoBehaviour, ISington
        {
            return map.ContainsKey(typeof(T));
        }

        public static T GetMono<T>() where T : MonoBehaviour, ISington
        {
            Type type = typeof(T);
            if (!map.ContainsKey(type))
            {
                GameObject o = new GameObject(type.ToString());
                if (type != typeof(SingtonMng))
                    o.transform.SetParent(GetMono<SingtonMng>().transform);
                T t = o.AddComponent<T>();
                map.Add(type, t);
                t.SingtonInit();
            }
            return (T)map[type];
        }

        public static T PushMono<T>(T o) where T : MonoBehaviour, ISington
        {
            Type type = typeof(T);
            map.Add(type, o);
            return o;
        }

        public static void Destroy()
        {
            GameObject.Destroy(GetMono<SingtonMng>().gameObject);
        }

        void OnDestroy()
        {
            map.Clear();
        }

        public void SingtonInit()
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public interface ISington
    {
        void SingtonInit();
    }
}