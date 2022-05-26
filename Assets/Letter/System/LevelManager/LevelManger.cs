using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManger : MonoBehaviour
{
    public UIEventHandler eventHandler;
    public void GoToScene(int i)
    {
        eventHandler.GoToScene(i);
    }
}
