using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Runtime
{
    public class ComponentModule:MonoBehaviour
    {
        protected virtual void Awake()
        {
            ModuleEntry.RejestModule(this);
        }
    }
}
