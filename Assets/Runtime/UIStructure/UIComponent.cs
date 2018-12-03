using Assets.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Runtime
{
    class UIComponent : ComponentModule
    {
        private IStateManager m_UIManager;
        protected override void Awake()
        {
            base.Awake();
            m_UIManager = PluginModule.GetStateManager();
        }
        private void Start() {
            
        }

        /// <summary>
        /// 打开页面
        /// </summary>
        /// <param name="pageName"></param>
        public void Open(string pageName) {
            m_UIManager.SetCurrentView(pageName, pageName);
        }

        /// <summary>
        /// 退出当前页
        /// </summary>
        public void GoBack()
        {
            m_UIManager.GoBack();
        }
    }
}
