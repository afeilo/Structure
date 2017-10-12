using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Runtime
{
    public class UIRoot : MonoBehaviour
    {
        public Transform[] UIStruct;
        public static UIRoot instance;

        // Use this for initialization
        void Awake()
        {
            instance = this;
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
