using UnityEngine;

public class MyDebug
{
    public static void Log(string str)
    {
        if (BuildSettings.isDebug)
        {
            Debug.Log(str);
        }
    }

    public static void LogError(string str)
    {
        if (BuildSettings.isDebug)
        {
            Debug.LogError(str);
        }

    }
}
