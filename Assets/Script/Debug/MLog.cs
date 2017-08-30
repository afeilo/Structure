using UnityEngine;

class MLog
{
    public static void D(string message) { 
//#if Debug
        log(message);
//#endif
    }
    public static void E(string message)
    {
//#if Debug
        log(message);
//#endif
    }
    private static void log(string message) {
        Debug.Log(message);
    }

}

