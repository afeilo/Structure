using Assets.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popupwindow : UIStateView
{
    public Button button;
    public override void PageCreate()
    {
        base.PageCreate();
        button.onClick.AddListener(back);
    }
    private void back()
    {
        ModuleEntry.GetModule<UIComponent>().GoBack();
    }
}
