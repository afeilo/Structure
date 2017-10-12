using Assets.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Runtime.UIStructure
{
    class UIComponent : MonoBehaviour
    {
        private UIStateManager m_UIManager;
        private void Awake()
        {
            m_UIManager = FrameWorkHelper.getModule<UIStateManager>();
            m_UIManager.SetStateHelper(new DefualtStateHelper());
        }
        private void Start() {
            m_UIManager.SetResourceManager(FrameWorkHelper.getModule<ResourceManager>());
            m_UIManager.SetCurrentView("baseui_1");
        }
    }
}
