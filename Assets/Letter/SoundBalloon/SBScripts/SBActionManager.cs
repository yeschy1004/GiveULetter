using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBActionManager : MonoBehaviour
{
    public enum ActionTypes { SoundAction, SwitchAction, GrayScaleAction, PulseAction, ProgressBarAction, PanelAction, NumberOfActions };

    ISBActions[,] actionPerTrigger = new ISBActions[(int)SBTriggerFilterer.TriggerTypes.NumberOfTriggers, (int)ActionTypes.NumberOfActions];
    AudioSource audioSource;
    Animator animator;
    SBTriggerFilterer trigger;

    SBSoundAction currentSound;
    // Start is called before the first frame update
    void Awake()
    {
        trigger = GetComponent<SBTriggerFilterer>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void ChooseAction(SBTriggerFilterer.TriggerTypes trigger)
    {
        if(!CheckIfInterruptable(currentSound)){
            return;
        }

        for (int i = 0; i < (int)ActionTypes.NumberOfActions; i++)
        {
            ISBActions actions;
            actions = actionPerTrigger[(int)trigger, i];
            if (actions == null || !actions.WillBeUsed())
                continue;

            if (actions.ActionTypes() == ActionTypes.SoundAction)
            {
                currentSound = (SBSoundAction)actions;
            }
            actions.DoAction();
        }
    }

    public void Pause()
    {
        animator.enabled = false;
        audioSource.Pause();
    }

    public void Unpause()
    {
        animator.enabled = true;
        audioSource.UnPause();
    }

    bool CheckIfInterruptable(SBSoundAction action)
    {
        if (action != null && action.WillBeUsed() && !action.IsInterruptable()) return false;
        return true;
    }
    #region SettingOptions
    public void SetAction(SBTriggerFilterer.TriggerTypes triggerNumber, ActionTypes actionNumber, ISBActions action)
    {
        actionPerTrigger[(int)triggerNumber, (int)actionNumber] = action;
        if(trigger == null)
        {
        }
        trigger.SetTrigger(triggerNumber, true);
    }

    public ISBActions GetAction(SBTriggerFilterer.TriggerTypes triggerNumber, ActionTypes actionNumber)
    {
        return actionPerTrigger[(int)triggerNumber, (int)actionNumber];
    }
    #endregion
    #region TestingPurpose
    public void PrintInfo()
    {
        for (int i = 0; i < (int)SBTriggerFilterer.TriggerTypes.NumberOfTriggers; i++)
        {
            for (int j = 0; j < (int)ActionTypes.NumberOfActions; j++)
            {
                ISBActions actions;
                actions = actionPerTrigger[i, j];
                if (actions == null)
                    continue;
                Debug.Log(actions.PrintInfo());
            }
        }
    }
    #endregion
}

