using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerUnlock
{
    public static bool IsUnlocked(string feature)
    {
        return PlayerPrefs.GetInt(feature) == 1;
    }
    public static void Unlock(string feature)
    {
        PlayerPrefs.SetInt(feature, 1); 
    }
}
