using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Runtime;
using UnityEngine.UI;

public class mainui : UIStateView {
	public Button button;
    public Button button1;
    public override void PageCreate()
    {
        base.PageCreate();
        button.onClick.AddListener(jumpPage1);
        button1.onClick.AddListener(jumpPage2);
    }
    private void jumpPage1(){
        ModuleEntry.GetModule<UIComponent>().Open("page1");
    }
    private void jumpPage2()
    {
        ModuleEntry.GetModule<UIComponent>().Open("page2");
    }
}
