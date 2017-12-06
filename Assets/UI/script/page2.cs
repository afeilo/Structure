using Assets.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class page2 : UIStateView
{
    public Button button;
    public Button popWindow;
    public override void PageCreate()
    {
        base.PageCreate();
        button.onClick.AddListener(back);
        popWindow.onClick.AddListener(pop);
    }
    private void back()
    {
        ModuleEntry.GetModule<UIComponent>().GoBack();
    }
    private void pop() {
        ModuleEntry.GetModule<UIComponent>().Open("popwindow");
    }
}
