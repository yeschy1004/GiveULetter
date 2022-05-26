using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    MouseRotation mouseCam;
    GyroRotation gyroCam;
    ICameraRotation currentCam;

    // Start is called before the first frame update
    void Start()
    {
        mouseCam = GetComponent<MouseRotation>();
        gyroCam = GetComponent<GyroRotation>();
        ChooseCamScript();
        UnPauseCamera();
        RestartCamera();
    }
    
    void ChooseCamScript()
    {
        if (Application.isEditor)
        {
            mouseCam.enabled = true;
            currentCam = mouseCam;
        }
        else
        {
            gyroCam.enabled = true;
            currentCam = gyroCam;
        }
    }

    public void PauseCamera()
    {
        currentCam.PauseRotation();
    }

    public void UnPauseCamera()
    {
        currentCam.UnPauseRotation();
    }

    public void RestartCamera()
    {
        currentCam.ResetBase();
    }
}
