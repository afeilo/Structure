using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    public class UIMng : Singleton<UIMng>
    {
        public BaseController current;
        private IState IState;
        private Stack<BaseController> stack;
        public UIMng() {
            IState = UIState.getInstance();
            stack = new Stack<BaseController>(); 
        }

        public void Add(string name) {
            Add(Type.GetType(name));
        }

        /// <summary>
        /// 跳转新页面 不压栈 用于处理弹窗
        /// </summary>
        /// <param name="name"></param>
        public void Add(Type t) {
            if (!t.IsSubclassOf(typeof(BaseController)))
            {
                Debug.LogError("type is not BaseController");
                return;
            }
            BaseController c = (TestController)Activator.CreateInstance(t);
            IState.LoadMediator(c,()=>{
                IState.EnableMediator(c);
            });
        }

        public void Goto(string name)
        {
            Goto(Type.GetType(name));
        }

        /// <summary>
        /// 跳转新页面 压栈 用于页面处理
        /// </summary>
        /// <param name="name"></param>
        public void Goto(Type t)
        {
            if (!t.IsSubclassOf(typeof(BaseController)))
            {
                Debug.LogError("type is not BaseController");
                return;
            }
            BaseController c = (BaseController)Activator.CreateInstance(t);
            IState.LoadMediator(c, () =>
            {
                if (null != current) {
                    IState.DisableMediator(current);
                    stack.Push(current);
                }
                current = c;
                IState.EnableMediator(current);
            });
        }

        /// <summary>
        /// 关闭弹窗
        /// </summary>
        /// <param name="c"></param>
        public void Close(BaseController c) {
            IState.UnLoadMediator(c);
        }

        /// <summary>
        /// 退栈
        /// </summary>
        public void Back() {
            current.Disable();
            IState.UnLoadMediator(current);
            current = stack.Pop();
            IState.EnableMediator(current);
        }

    }
}
