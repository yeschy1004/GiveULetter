using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recording {
    public AudioClip audio;
    public string path;

    public Recording(AudioClip audio, string path)
    {
        this.audio = audio;
        this.path = path;
    }

    public Recording(Recording record)
    {
        audio = record.audio;
        path = record.path;
    }

    public Recording() {
        audio = null;
        path = "";
    }

    public void SetNewClip(AudioClip audio)
    {
        this.audio = audio;
        path = "";
    }
    public void CopyRecord(Recording record)
    {
        if (record == null)
        {
            audio = null;
            path = "";
        }
        else
        {
            audio = record.audio;
            path = record.path;
        }
    }

    public void SetNewClip(AudioClip audio, string path)
    {
        this.audio = audio;
        this.path = path;
    }
    public void Reset()
    {
        audio = null;
        path = "";
    }
    public bool IsEmpty()
    {
        if(audio == null || path == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
