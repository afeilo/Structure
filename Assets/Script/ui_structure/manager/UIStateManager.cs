using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateManager : MonoBehaviour,IStateManager {

    public string main;

    public Transform[] UIStruct;
    void Awake()
    {
        manager = this;
    }
	// Use this for initialization
	void Start () {
        SetCurrentView(main);
	}

    public Transform GetParent(int index) {
        return UIStruct[index];
    }
    

	// Update is called once per frame
	void Update () {
		
	}
    #region 状态相关
    private static UIStateManager manager;
    private static string manager_name = "StateManager";
    private IStateView currentUIState;
    private IStateView newUIState;
    public static UIStateManager instance
    {
        get
        {

            if (manager == null)
            {
                GameObject go = new GameObject(manager_name);
                manager = go.AddComponent<UIStateManager>();
            }

            return manager;
        }
    }

    private Stack listStack = new Stack();

    /// <summary>
    /// 设置当前页面
    /// </summary>
    /// <param name="stateId"></param>
    public void SetCurrentView(string abName)
    {
        if (currentUIState!=null)
            currentUIState.PagePause();
      //  StateObject stateObject = StateRejester.getInstance().getStateObject(stateId);
        ABLoader.instance.Load(abName, loadSuccess);
    }

    /// <summary>
    /// 加载成功
    /// </summary>
    /// <param name="obj"></param>
    private void loadSuccess(Object obj)
    {
        GameObject gameObject = Instantiate(obj) as GameObject;
        IStateView view = UIStateView.BindView(gameObject);
        view.PageCreate();
        if (!view.IsPopupWindow())
        {
            if (currentUIState != null)
                currentUIState.PageStop();
        }
        view.PageResume();
        if (currentUIState != null) listStack.Push(currentUIState);
        currentUIState = view;
    }


    public void GoBack()
    {

    }

    public IStateView getCurrentView() {
        return currentUIState;
    }


    #endregion
}
