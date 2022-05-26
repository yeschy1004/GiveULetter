using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBConverter : MonoBehaviour
{
    public static SBConverter converter;
 
    public Texture2D[] testTextures;
    public AudioClip[] testClips;


    private void Awake()
    {
        if(converter == null)
        {
            converter = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //SetTest();
    }
    public void DestroySBs()
    {
        Destroy(gameObject);
    }

    public void TurnOffChildren()
    {
        foreach(SBEditingObject sb in GetComponentsInChildren<SBEditingObject>())
        {
            sb.gameObject.SetActive(false);
        }
    }
    public void TurnOnChildren()
    {
        foreach (SBEditingObject sb in GetComponentsInChildren<SBEditingObject>(true))
        {
            sb.gameObject.SetActive(true);
        }
    }
 
    void SetTest()
    {
        Picture pic = new Picture();
        Recording rec = new Recording();
        int i = 0;
        foreach(SBEditingObject sb in GetComponentsInChildren<SBEditingObject>())
        {
            pic.texture = testTextures[i];
            pic.path = "pic";
            rec.audio = testClips[i];
            rec.path = "rec";
            i++;
            sb.SetPicture(pic);
            sb.SetRecord(rec);
            //sb.Debugging();
        }
    }
}
