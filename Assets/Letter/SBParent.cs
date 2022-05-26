using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBParent : MonoBehaviour
{
    SBActionManager[] sbactions;
    int numberOfSBs;

    private void Start()
    {
        sbactions = GetComponentsInChildren<SBActionManager>(true);
        numberOfSBs = SBConverter.converter != null ? SBConverter.converter.transform.childCount : transform.childCount;
    }

    public void PauseSBsAfterThisSB(Transform sb)
    {
        for (int i = sb.GetSiblingIndex() + 1; i < numberOfSBs; i++) {
            sbactions[i].Pause();
        }
    }

    public void UnPauseSBsAfterThisSB(Transform sb)
    {
        for (int i = sb.GetSiblingIndex() + 1; i < numberOfSBs; i++)
        {
            sbactions[i].Unpause();
        }
    }
}
