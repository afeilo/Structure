using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.FrameWork
{
    interface IResourceHelper<T>
    {
        void Release(T target);
    }
}
