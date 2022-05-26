using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture
{
    public Texture2D texture;
    public string path;

    public Picture(Texture2D texture, string path)
    {
        this.texture = texture;
        this.path = path;
    }

    public Picture() { }

    public void CopyPic(Picture picture)
    {
        texture = picture.texture;
        path = picture.path;
    }
    public void Reset()
    {
        texture = null;
        path = "";
    }


    public bool IsEmpty()
    {
        if(texture == null || path == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
