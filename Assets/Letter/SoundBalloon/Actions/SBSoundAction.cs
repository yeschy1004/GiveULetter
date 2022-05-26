using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBSoundAction : ISBActions
{
    AudioSource audioSource;
    Recording record = new Recording();
    bool loop;
    bool inUse;
    bool allowInterruption;
    
    public SBSoundAction(SoundBalloon balloon)
    {
        audioSource = balloon.GetComponent<AudioSource>();
        inUse = true;
    }

    public SBSoundAction(SoundBalloon balloon, Recording sound, bool allowInterruption, bool loop = false)
    {
        audioSource = balloon.GetComponent<AudioSource>();
        record.CopyRecord   (sound);
        this.loop = loop;
        this.allowInterruption = allowInterruption;
        inUse = true;
    }

    public SBSoundAction(SoundBalloon balloon, AudioClip sound, bool allowInterruption, bool loop = false)
    {
        audioSource = balloon.GetComponent<AudioSource>();
        record.SetNewClip(sound);
        this.loop = loop;
        this.allowInterruption = allowInterruption;
        inUse = true;
    }
    
    public SBSoundAction(SoundBalloon balloon, AudioClip noSound)
    {
        audioSource = balloon.GetComponent<AudioSource>();
        record.SetNewClip(noSound);
        loop = false;
        allowInterruption = true;
        inUse = true;
    }

    public void DoAction()
    {
        if (record == null)
        {
            audioSource.Stop();
            return;
        }
        audioSource.clip = record.audio;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public bool IsInterruptable()
    {
        if (allowInterruption || record.audio == null) return true;
        if (audioSource.clip == record.audio && audioSource.isPlaying) return false;
        return true;
    }
    #region Set
    public void SetAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    public void SetRecording(Recording sound, bool loop)
    {
        record.CopyRecord(sound);
        this.loop = loop;
    }

    public void SetRecording(AudioClip sound, bool loop)
    {
        record.SetNewClip(sound);
        this.loop = loop;
    }

    public void SetLoop(bool loop)
    {
        this.loop = loop;
    }

    public bool WillBeUsed()
    {
        return inUse;
    }

    public void SetWillBeUsed(bool use)
    {
        inUse = use;
    }

    #endregion

    #region TestingPurpose
    public string PrintInfo()
    {
        if(record.audio == null)
        {
            return "SoundAction : TurnOff";
        }
        return "SoundAction : Clip - " + record.audio.name + ", Loop - " + loop + ", Interruption - " + allowInterruption;
    }
    #endregion
    public SBActionManager.ActionTypes ActionTypes()
    {
        return SBActionManager.ActionTypes.SoundAction;
    }
}
