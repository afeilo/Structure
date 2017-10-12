﻿using Assets.FrameWork.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Runtime
{
    public class Request
    {

        public string name;
        public string path;
        public AssetBundle ab;
        public Object obj;

        public Request(string name, string path)
        {
            this.name = name;
            this.path = path;
        }
    }
}
