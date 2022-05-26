using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBSwitchAction : ISBActions
{
    GameObject target;
    bool turnOn;
    bool inUse;

    public SBSwitchAction()
    {
        inUse = true;
    }

    public SBSwitchAction(GameObject target, bool turnOn)
    {
        this.target = target;
        this.turnOn = turnOn;
        inUse = true;
    }

    public void DoAction()
    {
        target.SetActive(turnOn);
    }
    #region Set

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetSwitch(bool switchOn)
    {
        turnOn = switchOn;
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
        return "SwitchAction : Target - " + target + ", turnOn - " + turnOn;
    }
    #endregion
    public SBActionManager.ActionTypes ActionTypes()
    {
        return SBActionManager.ActionTypes.SwitchAction;
    }
}
