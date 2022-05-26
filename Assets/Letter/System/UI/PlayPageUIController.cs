using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPageUIController : MonoBehaviour
{
    public static PlayPageUIController mainUI;

    [SerializeField] Animator topUI;
    [SerializeField] Text fromTo;

    private void Awake()
    {
        SetSingleton();
    }
    
    public void StartContent()
    {
       // FromToGetSetSystem.SetFrom("나로부터");
        //FromToGetSetSystem.SetTo("너에게");
        SetToFrom();
    }

    public void ShowTopUI()
    {
        topUI.SetTrigger("ShowUI");
    }

    public void SetToFrom()
    {
        fromTo.text = "To. " + FromToGetSetSystem.GetTo() + "\nFrom. " + FromToGetSetSystem.GetFrom();
    }

    void SetSingleton()
    {
        if(mainUI == null)
        {
            mainUI = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
