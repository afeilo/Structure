using Assets.FrameWork;
using UnityEngine;

namespace Assets.Runtime
{
    class DefualtStateHelper : MonoBehaviour,IStateHelper
    {
        /// <summary>
        /// 实例化object 并返回实例
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object InstantObject(object obj) {
            return Instantiate((Object)obj);
        }

        /// <summary>
        /// 创建View
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IStateView BindView(object obj, string name) {
            GameObject gobj = obj as GameObject;
            return gobj.GetComponent<UIStateView>();
        }
    }
}
