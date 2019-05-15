using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Framework
{
    [ExecuteInEditMode]
    public class UIRoot : MonoBehaviour
    {
        public Transform[] UIStruct;
        public static UIRoot instance;
        public TImage background;
        // Use this for initialization
        void Awake()
        {
            
        }

        void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_EDITOR
            instance = this;
#endif
        }
    }
}
