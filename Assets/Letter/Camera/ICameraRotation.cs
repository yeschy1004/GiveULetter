using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraRotation
{
    void ResetBase();

    void PauseRotation();
    void UnPauseRotation();
}
