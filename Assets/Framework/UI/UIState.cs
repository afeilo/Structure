using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    public class UIState : Singleton<UIState>, IState
    {
        public void LoadMediator(BaseController controller, Action callback) 
        {
            controller.Awake();
            controller.state = STATE.AWAKE;
            Loader.LoadAsset(controller.GetMediatorName(), (UnityEngine.Object o) =>
            {
                if (null != callback)
                {
                    GameObject go = GameObject.Instantiate(o) as GameObject;
                    var mediator = go.GetComponent<IMediator>();
                    if (null == mediator){
                        Debug.LogError("没有meditor");
                        mediator = go.AddComponent<UIMediator>();
                    }
                    controller.mediator = mediator;
                    mediator.gameObject.SetActive(false);
                    controller.Start();
                    controller.state = STATE.START;
                    callback();
                }
            },null);   
        }

        public void UnLoadMediator(BaseController controller) 
        {
            if (STATE.ENABLE == controller.state) {
                DisableMediator(controller);
            }
            if (STATE.DISABLE != controller.state)
                return;
            controller.Destroy();
            controller.state = STATE.DESTORY;
            GameObject.Destroy(controller.mediator.gameObject);
            Loader.UnLoadAssetWithName(controller.GetMediatorName());
        }

        public void EnableMediator(BaseController controller)
        {
            if (STATE.START != controller.state && STATE.DISABLE != controller.state)
                return;
            controller.Enable();
            controller.state = STATE.ENABLE;
        }

        public void DisableMediator(BaseController controller) 
        {
            if (STATE.ENABLE != controller.state)
                return;
            controller.Disable();
            controller.state = STATE.DISABLE;
        }


    }
}