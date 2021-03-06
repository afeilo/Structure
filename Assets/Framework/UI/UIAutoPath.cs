﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    [ExecuteInEditMode]
    public class UIAutoPath : MonoBehaviour
    {
        /// <summary>
        /// 用来描述页面层级
        /// </summary>
        public enum State
        {
            /// <summary>
            /// 最基本ui
            /// </summary>
            baseUI = 0,
            /// <summary>
            /// 页面ui
            /// </summary>
            pageUI = 1,
            /// <summary>
            /// 弹出框
            /// </summary>
            popupUI = 2,
            /// <summary>
            /// 加载框
            /// </summary>
            loadingUI = 3,
        }


        public State state = State.baseUI;

        // Use this for initialization
        void Start()
        {
            //  Vector3 position = transform.position;
            Reset();
            //transform.position = position;
        }

        public void Reset()
        {
              transform.SetParent(UIRoot.instance.UIStruct[(int)state], false);

        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            Reset();
#endif

        }
    }
}