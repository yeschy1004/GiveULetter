using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBPulseAction : ISBActions
{
    Animator animator;
    bool startPulse;
    bool isInUse;

    public SBPulseAction(SoundBalloon sb)
    {
        animator = sb.GetComponent<Animator>();
        isInUse = true;
    }

    public SBPulseAction(SoundBalloon sb, bool startPulse)
    {
        animator = sb.GetComponent<Animator>();
        this.startPulse = startPulse;
        isInUse = true;
    }

    public SBActionManager.ActionTypes ActionTypes()
    {
        return SBActionManager.ActionTypes.PulseAction;
    }

    public void DoAction()
    {
        string anim = startPulse ? "StartPulse" : "StopPulse";
        animator.SetTrigger(startPulse? "StartPulse" : "StopPulse");
    }

    public bool WillBeUsed()
    {
        return isInUse;
    }

    public string PrintInfo()
    {
        throw new System.NotImplementedException();
    }

    public void SetWillBeUsed(bool use)
    {
        isInUse = use;
    }
}
