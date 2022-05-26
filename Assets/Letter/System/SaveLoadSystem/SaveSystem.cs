using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static string tempFolderPath;
    public static SaveSystem saveSystem;
    [SerializeField] UnityEngine.UI.Text pathText;
    LetterSaveObject letterSaveObject;
    string jsonFile;

    private void Awake()
    {
        if(saveSystem == null)
        {
            saveSystem = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PrepareTempFolder();
        letterSaveObject = new LetterSaveObject();
    }
    
    public void SaveFile()
    {
        letterSaveObject.from = FromToGetSetSystem.GetFrom();
        letterSaveObject.to = FromToGetSetSystem.GetTo();
        int i = 0;
        letterSaveObject.sbs = new SBSaveObject[SBConverter.converter.transform.childCount];
        letterSaveObject.audioNames = new string[SBConverter.converter.transform.childCount];
        letterSaveObject.pictureNames = new string[SBConverter.converter.transform.childCount];

        foreach (SBEditingObject sb in SBConverter.converter.transform.GetComponentsInChildren<SBEditingObject>())
        {
            int index = sb.transform.GetSiblingIndex();
            SBSaveObject saveObject = sb.GetSaveObject();
            Debug.Log("got saveObject");
            letterSaveObject.sbs[index] = saveObject;
            letterSaveObject.audioNames[index] = saveObject.recordName;
            letterSaveObject.pictureNames[index] = saveObject.pictureName;
            /*
            if (!DoesExist(letterSaveObject.audioNames, saveObject.recordName))
            {
                letterSaveObject.audioNames[index] = saveObject.recordName;
            }
            if (!DoesExist(letterSaveObject.pictureNames, saveObject.pictureName))
            {
                letterSaveObject.pictureNames[index] = saveObject.pictureName;
            }*/
        }
        pathText.text = "done, starting to convert";
        jsonFile = JsonUtility.ToJson(letterSaveObject);
        pathText.text = "convert done";
        string path = Path.Combine(tempFolderPath, "temp.json");
        File.WriteAllText(path, jsonFile);
        pathText.text = path;
    }

    void PrepareTempFolder()
    {
        tempFolderPath = Application.persistentDataPath + "/SaveFiles/temp";
        //text.text = tempFolderPath;
        if (Directory.Exists(tempFolderPath))
        {
            Directory.Delete(tempFolderPath, true);
        }
        Directory.CreateDirectory(tempFolderPath);
        pathText.text = tempFolderPath;
    }

    bool DoesExist(string [] array, string word)
    {
        foreach(string str in array)
        {
            if (str.Equals(word))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsSaveable()
    {
        if (SBConverter.converter.GetComponentsInChildren<Transform>().Length == 1) return false;
        foreach(SBEditingObject sb in SBConverter.converter.transform.GetComponentsInChildren<SBEditingObject>())
        {
            if (!sb.GetSaveObject().IsSaveable())
            {
                return false;
            }
        }
        return true;
    }
}
