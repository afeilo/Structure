using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    public abstract class IMediator : MonoBehaviour
    {
        public abstract T Get<T>(string name);
    }
}