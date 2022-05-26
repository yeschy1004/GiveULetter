using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISBActions
{
    void DoAction();
    bool WillBeUsed();
    void SetWillBeUsed(bool use);
    string PrintInfo();
    SBActionManager.ActionTypes ActionTypes();
}
