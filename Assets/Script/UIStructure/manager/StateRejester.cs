using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRejester : Singleton<StateRejester>
{
    private List<StateObject> rejesterPages;

    public StateRejester() {
        init();
    }
    private void init() {
        rejesterPages = new List<StateObject>();
        rejesterPages.Add(new StateObject("main","main"));
    }

    public StateObject getStateObject(UIStateId id) {
        return rejesterPages[(int)id];
    }

    public enum UIStateId
    {
        main = 0,
    }
}
