using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    public abstract class BaseController
    {
        public IMediator mediator;
        public STATE state = STATE.NONE;
        public abstract string GetMediatorName();

        /// <summary>
        /// 用于初始化数据
        /// </summary>
        public virtual void Awake() 
        {
        
        }
        public virtual void Start()
        {

        }

        // Use this for initialization
        public virtual void Enable()
        {

        }

        public virtual void Disable()
        {

        }

        public virtual void Destroy()
        {

        }

        
    }
    public enum STATE
    {
        NONE,
        AWAKE,
        START,
        ENABLE,
        DISABLE,
        DESTORY,
    }
}