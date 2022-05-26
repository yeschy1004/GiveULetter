using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SBSaveObject {
    public string pictureName;
    public string recordName;
    public Vector3 position;
    public int orderNumber;

    public bool IsSaveable()
    {
        if(string.IsNullOrEmpty(pictureName) ||
            string.IsNullOrEmpty(recordName))
        {
            return false;
        }
        return true;
    }
}
