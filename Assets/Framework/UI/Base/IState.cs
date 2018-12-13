using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    public interface IState
    {

        void LoadMediator(BaseController controller, Action callback);

        void UnLoadMediator(BaseController controller);

        void EnableMediator(BaseController controller);

        void DisableMediator(BaseController controller);

    }
}