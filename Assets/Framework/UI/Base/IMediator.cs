using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    public abstract class IMediator : MonoBehaviour
    {
        public abstract Object Get(string name);
        public void bind(object obj)
        {
            FAM.bind(obj, this);
        }
    }
}