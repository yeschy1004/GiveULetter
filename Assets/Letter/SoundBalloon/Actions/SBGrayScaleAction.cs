using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBGrayScaleAction : ISBActions
{
    const float grayScale = 1f;
    const float rgb = 0f;
    [SerializeField] Material mat;
    [SerializeField] bool toColor;
    bool actionInUse;

    public SBGrayScaleAction (SoundBalloon sb)
    {
        mat = sb.GetComponentInChildren<Renderer>().material;
        actionInUse = true;
    }

    public SBGrayScaleAction(SoundBalloon sb, bool toColor)
    {
        mat = sb.GetComponentInChildren<Renderer>().material;
        actionInUse = true;
        this.toColor = toColor;
    }

    public void DoAction()
    {
        mat.SetFloat("_EffectAmount", toColor ? rgb : grayScale);
    }
    #region Set
    public void SetToColor(bool toColor)
    {
        this.toColor = toColor;
    }

    public bool WillBeUsed()
    {
        return actionInUse;
    }

    public void SetWillBeUsed(bool use)
    {
        actionInUse = use;
    }

    #endregion

    #region TestingPurpose
    public string PrintInfo()
    {
        return "GrayScaleAction: tocolor - " + toColor;
    }

    public SBActionManager.ActionTypes ActionTypes()
    {
        return SBActionManager.ActionTypes.GrayScaleAction;
    }
    #endregion
}
