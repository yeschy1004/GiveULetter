using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBProgressAction : ISBActions
{
    bool isInUse = false;
    bool turnOn;
    int orderNumber;

    public SBProgressAction()
    {
        isInUse = true;
    }

    public SBProgressAction(SoundBalloon balloon, bool turnOn)
    {
        orderNumber = balloon.orderNumber;
        this.turnOn = turnOn;
        isInUse = true;
    }

    public SBActionManager.ActionTypes ActionTypes()
    {
        return SBActionManager.ActionTypes.ProgressBarAction;
    }

    public void DoAction()
    {
        if (turnOn)
        {
            OrderBar.orderBar.ShowBar(orderNumber);
        }
        else
        {
            OrderBar.orderBar.HideBar();    
        }
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
