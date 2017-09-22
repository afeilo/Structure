using Assets.FrameWork.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateManager : MonoBehaviour,IStateManager {

    public string main;


    private LoadAssetCallbacks loadAssetCallback;
    void Awake()
    {
        manager = this;
    }
	// Use this for initialization
	void Start () {
        loadAssetCallback = new LoadAssetCallbacks(loadSuccess,loadFail);
        SetCurrentView(main);
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

    /// <summary>
    /// 任务栈
    /// </summary>
    private Stack listStack = new Stack();

    /// <summary>
    /// 加载过的任务
    /// </summary>
    private Dictionary<string, IStateView> recentView = new Dictionary<string, IStateView>();

    /// <summary>
    /// 设置当前页面
    /// </summary>
    /// <param name="stateId"></param>
    public void SetCurrentView(string abName)
    {
        if (currentUIState!=null)
            currentUIState.PagePause();
      //  StateObject stateObject = StateRejester.getInstance().getStateObject(stateId);
        ResourceManager.instance.LoadAsset(abName, loadAssetCallback);
    }

    /// <summary>
    /// 加载成功
    /// </summary>
    /// <param name="obj"></param>
    private void loadSuccess(string abname, System.Object obj)
    {
        GameObject gameObject = Instantiate(obj as Object) as GameObject;
        IStateView view = UIStateView.BindView(gameObject);
        recentView.Add(name, view);
        gameObject.name = abname;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="errorMessage"></param>
    private void loadFail(string name, string errorMessage)
    {
       
    }

    /// <summary>
    /// 返回上一页
    /// </summary>
    public void GoBack()
    {
        IStateView newStateView = listStack.Pop() as IStateView;
        currentUIState.PagePause();
        if (newStateView != null) {
            if (!currentUIState.IsPopupWindow())
            {
                newStateView.PageStart();
            }
        }
        currentUIState.PageStop();
        //recentView.Add();
        if (newStateView != null) {
            newStateView.PageResume();
            currentUIState = newStateView;
        }
        
    }

    public IStateView getCurrentView() {
        return currentUIState;
    }


    #endregion
}
