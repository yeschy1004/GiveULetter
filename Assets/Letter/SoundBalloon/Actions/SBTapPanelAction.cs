using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBTapPanelAction : ISBActions
{
    SBTriggerFilterer myTrigger;
    AudioClip clip;
    Texture2D texture;
    bool willBeUsed;

    public SBTapPanelAction(SoundBalloon balloon, AudioClip clip, Texture2D tex)
    {
        myTrigger = balloon.GetComponent<SBTriggerFilterer>();
        this.clip = clip;
        texture = tex;
        willBeUsed = true;
    }

    public SBActionManager.ActionTypes ActionTypes()
    {
        return SBActionManager.ActionTypes.PanelAction;
    }

    public void DoAction()
    {
        TapPanel.tapPanel.TurnPanelOn(myTrigger, clip, texture);
    }

    public bool WillBeUsed()
    {
        return willBeUsed;
    }

    public string PrintInfo()
    {
        throw new System.NotImplementedException();
    }

    public void SetWillBeUsed(bool use)
    {
        willBeUsed = use;
    }
    
}