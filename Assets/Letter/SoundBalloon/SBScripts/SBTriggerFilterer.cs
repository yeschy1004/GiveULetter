using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBTriggerFilterer : MonoBehaviour
{
    //cyclefinished => tap으로 인해 확대 사진이 나타나고 확대사진 끝나면 cyclefinishedtrigger
    public enum TriggerTypes { Initial, Look, Tap, NotLooking, PanelFinished, NumberOfTriggers};

    [SerializeField] private bool[] useTrigger = new bool[(int)TriggerTypes.NumberOfTriggers];

    SBActionManager action;
    
    private void Awake()
    {
        action = GetComponent<SBActionManager>();
    }

    private void OnEnable()
    {
        SendTriggerInfo(TriggerTypes.Initial);
    }

    void SendTriggerInfo(TriggerTypes trigger)
    {
        if (!LoadSystem.contentIsPlaying) {
            return;
        }
        if (!useTrigger[(int)trigger]) return;
        action.ChooseAction(trigger);
    }

    public void SetTrigger(TriggerTypes trigger, bool use)
    {
        useTrigger[(int)trigger] = use;
    }
    #region TriggerFiltering
    public void OnLook()
    {
        SendTriggerInfo(TriggerTypes.Look);
    }

    public void NotLooking()
    {
        SendTriggerInfo(TriggerTypes.NotLooking);
    }

    public void OnTap()
    {
        SendTriggerInfo(TriggerTypes.Tap);
    }

    public void OnPanelFinished()
    {
        SendTriggerInfo(TriggerTypes.PanelFinished);
    }

    #endregion

    #region TestingPurpose
    public void PrintInfo()
    {
        string info = "Trigger usage : ";
        for (int i = 0; i < (int)TriggerTypes.NumberOfTriggers; i++)
        {
            info += (TriggerTypes)i + "-" + useTrigger[i] + " ";
        }
        Debug.Log(info);
        action.PrintInfo();
    }
    #endregion
}
