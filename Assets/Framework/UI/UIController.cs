using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    public abstract class UIController : BaseController
    {
        public override void Awake()
        {

        }
        public override void Start()
        {

        }

        // Use this for initialization
        public override void Enable()
        {
            mediator.gameObject.SetActive(true);
        }

        public override void Disable()
        {
            mediator.gameObject.SetActive(false);
        }

        public override void Destroy()
        {

        }
    }
}