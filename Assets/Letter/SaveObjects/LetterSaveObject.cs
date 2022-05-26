using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LetterSaveObject
{
    public string from;
    public string to;
    public int themeNumber;
    public SBSaveObject[] sbs;
    public string[] audioNames;
    public string[] pictureNames;
}
