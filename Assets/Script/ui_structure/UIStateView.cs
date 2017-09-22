using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateView : IStateView
{

	// Use this for initialization
	private void Start () {
        MLog.D("Start");
	}

    public void PageCreate() {
        MLog.D("PageCreate");
    }

    public void PagePause()
    {
        MLog.D("PagePause");
    }

    public void PageResume()
    {
        MLog.D("PageResume");
    }

    public void PageStart() {
        MLog.D("PageResume");
    }

    public void PageStop()
    {
        MLog.D("PageStop");
    }

    public void PageDestory() {
        MLog.D("PageDestory");
    }

    public bool IsPopupWindow(){
        return false;
    }


    private ViewContainer viewContainer;

    public static UIStateView BindView(GameObject gameObject)
    {
        return new UIStateView(gameObject);
    }
    private UIStateView(GameObject gameObject)
    {
        viewContainer = gameObject.GetComponent<ViewContainer>();
    }

}
