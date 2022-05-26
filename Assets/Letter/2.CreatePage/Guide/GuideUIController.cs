using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUIController : MonoBehaviour
{
    [SerializeField] GameObject addSBGuide;
    [SerializeField] GameObject setSBGuide;
    [SerializeField] Animator animator;

    static public string guideShownPref = "Shown";
    bool addGuideShown = false;
    bool setGuideShown = false;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(guideShownPref)) {
            addGuideShown = PlayerPrefs.GetInt(guideShownPref) == 1;
            setGuideShown = addGuideShown;
        }
        if(addGuideShown && setGuideShown)
        {
            gameObject.SetActive(false);
        }
    }

    public void OpenAddSBGuide()
    {
        if (addGuideShown) return;
        addGuideShown = true;
        animator.SetTrigger("ShowAddGuide");
    }

    public void OpenSetSBGuide()
    {
        if (setGuideShown) return;
        setGuideShown = true;
        animator.SetTrigger("ShowSetGuide");
        PlayerPrefs.SetInt(guideShownPref, 1);
    }

    public void HideAddSBGuide()
    {
        addSBGuide.SetActive(false);
        OpenSetSBGuide();
    }

    public void HideSetSBGuide()
    {
        setSBGuide.SetActive(false);
    }
}
