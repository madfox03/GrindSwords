using UnityEngine;

public class GameSettings
{

    #region Settings
    public static string language
    {
        get
        {
            return PlayerPrefs.GetString("language", "");
        }
        set
        {
            PlayerPrefs.SetString("language", value);
            save();
        }
    }
    #endregion

    public static void save()
    {
        PlayerPrefs.Save();
    }
}

