using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class FromToGetSetSystem
{
    readonly public static string fromPref = "sender";
    readonly public static string toPref = "receiver";

    public static void ResetToFrom()
    {
        ResetTo();
        ResetFrom();
//        Debug.LogWarning("Playerpref.deleteallkey not erased");
        PlayerPrefs.DeleteAll();
    }

    public static void ResetTo()
    {
        if(PlayerPrefs.HasKey(toPref))
            PlayerPrefs.DeleteKey(toPref);
    }

    public static void ResetFrom()
    {
        if (PlayerPrefs.HasKey(fromPref))
            PlayerPrefs.DeleteKey(fromPref);
    }
    public static void SetFrom(string sender)
    {
        PlayerPrefs.SetString(fromPref, sender);
    }
    public static void SetTo(string receiver)
    {
        PlayerPrefs.SetString(toPref, receiver);
    }

    public static string GetFrom()
    {
        return PlayerPrefs.GetString(fromPref);
    }

    public static string GetTo()
    {
        return PlayerPrefs.GetString(toPref);
    }

    public static bool HasFromAndTo()
    {
        return PlayerPrefs.HasKey(fromPref) && PlayerPrefs.HasKey(toPref);
    }
}

