using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SBEditingObject : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform texturePart;
    [SerializeField] Texture2D basicImage;
    Recording record;
    Picture picture;
    SBSaveObject saveObject;
    Material mat;
    
    private void Awake()
    {
        saveObject = new SBSaveObject();
        mat = GetComponentInChildren<Renderer>().material;
        record = new Recording();
        picture = new Picture();
        ResetPicture();
    }

    public void SetPicture(Picture picture)
    {
        this.picture.CopyPic(picture);
        if (picture.texture.height < picture.texture.width)
        {
            texturePart.localScale = new Vector3(texturePart.localScale.x, picture.texture.height * texturePart.localScale.x / picture.texture.width, 1f);
        }
        else
        {
            texturePart.localScale = new Vector3(picture.texture.width * texturePart.localScale.y / picture.texture.height, texturePart.localScale.y, 1f);
        }
        
        mat.SetTexture("_MainTex", picture.texture);
    }
    public void SetRecord(Recording record)
    {
        this.record.CopyRecord(record);
    }

    public void UpdateSaveObject()
    {
        saveObject.position = transform.position;
        saveObject.pictureName = Path.GetFileNameWithoutExtension(picture.path);
        saveObject.recordName = Path.GetFileNameWithoutExtension(record.path);
        saveObject.orderNumber = transform.GetSiblingIndex();
    }

    public SBSaveObject GetSaveObject()
    {
        UpdateSaveObject();
        return saveObject;
    }

    /*
    public void Debugging()
    {
        Debug.Log("startDebugging " + gameObject.name);
        if (audioSource == null) Debug.Log("no audioSource");
        if (record.audio == null) Debug.Log("no clip");
        //audioSource.clip = record.audio;
        //audioSource.Play();
        Debug.Log(record.path);
        Debug.Log(picture.path);
    }
    */
    public void ResetPicture()
    {
        picture.Reset();
        picture.texture = basicImage;
        SetPicture(picture);
    }
    
    public void ResetRecord()
    {
        record.Reset();
    }

    public Picture GetPicture()
    {
        return picture;
    }
    public Recording GetRecording()
    {
        return record;
    }
}
    