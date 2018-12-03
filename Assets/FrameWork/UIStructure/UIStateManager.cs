using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.FrameWork
{
    /**这个类应该用来控制页面的状态
     */
    public class UIStateManager : Singleton<UIStateManager>,IStateManager
    {

        private LoadAssetCallbacks loadAssetCallback;

        // Use this for initialization


        // Update is called once per frame
        void Update()
        {

        }
        #region 状态相关
        private static UIStateManager manager;
        private static string manager_name = "StateManager";
        private IStateHelper stateHelper;
        private IResourceManager mResourceManager;
        private IStateView currentUIState;
        private IStateView newUIState;

        public UIStateManager() {
            loadAssetCallback = new LoadAssetCallbacks(loadSuccess,loadFail);
            mResourceManager = FrameWorkHelper.getModule<ResourceManager>();
        }

        public void SetStateHelper(IStateHelper helper) {
            stateHelper = helper;
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
        public void SetCurrentView(string abName,string assetName)
        {
            if (currentUIState != null)
                currentUIState.PagePause();
            //  StateObject stateObject = StateRejester.getInstance().getStateObject(stateId);
            IStateView view;
            recentView.TryGetValue(assetName, out view);
            if (null != view){
                showNewView(view);
            }
            else {
                mResourceManager.LoadAsset(abName, assetName, loadAssetCallback);
            }

        }

        public void GoBack()
        {
            MLog.D("goback");
            IStateView newStateView = listStack.Pop() as IStateView;
            currentUIState.PagePause();
            if (newStateView != null)
            {
                if (!currentUIState.IsPopupWindow())
                {
                    newStateView.PageStart();
                }
            }
            currentUIState.PageStop();
            //recentView.Add();
            if (newStateView != null)
            {
                newStateView.PageResume();
                currentUIState = newStateView;
            }

        }



        public IStateView getCurrentView()
        {
            return currentUIState;
        }

        private void showNewView(IStateView view) {
            if (currentUIState == null) {
                view.PageStart();
                view.PageResume();
                currentUIState = view;
                return;
            }
            currentUIState.PagePause();
            if (!view.IsPopupWindow())
            {
                currentUIState.PageStop();
            }
            view.PageStart();
            view.PageResume();
            listStack.Push(currentUIState); 
            currentUIState = view;
        }

        /// <summary>
        /// 加载成功
        /// </summary>
        /// <param name="obj"></param>
        private void loadSuccess(string abname, System.Object obj)
        {
            MLog.D("loadSuccess");
            GameObject gameObject = GameHelper.GetInstance().InstantiateObject((Object)obj);
            //todo
            IStateView view = stateHelper.BindView(gameObject, abname);
            // recentView.Add(abname, view);
            gameObject.name = abname;
            view.PageCreate();
            showNewView(view);
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



        #endregion
    }
}