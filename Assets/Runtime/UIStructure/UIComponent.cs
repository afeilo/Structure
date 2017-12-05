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
        private UIStateManager m_UIManager;
        protected override void Awake()
        {
            base.Awake();
            m_UIManager = FrameWorkHelper.getModule<UIStateManager>();
            m_UIManager.SetStateHelper(new DefaultStateHelper());
            m_UIManager.SetResourceManager(FrameWorkHelper.getModule<ResourceManager>());
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
        
    }
}
