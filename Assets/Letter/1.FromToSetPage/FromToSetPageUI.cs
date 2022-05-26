using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FromToSetPageUI : MonoBehaviour
{
    [SerializeField] InputField fromInput;
    [SerializeField] InputField toInput;
    [SerializeField] Text fromText;
    [SerializeField] Text toText;
    [SerializeField] GameObject fromError;
    [SerializeField] GameObject toError;
    [SerializeField] ButtonController gotoEditPageButton;
    // Start is called before the first frame update
    void Start()
    {
        if(SBConverter.converter != null)
        {
            fromText.text = FromToGetSetSystem.GetFrom();
            toText.text = FromToGetSetSystem.GetTo();
        }
        else
        {
            FromToGetSetSystem.ResetToFrom();
        }
        CheckButtonValid();
    }
    
    public void SetFrom()
    {
        FromToGetSetSystem.ResetFrom();
        if (IsNameValid(fromInput.text)) {
            FromToGetSetSystem.SetFrom(fromInput.text);
            fromError.SetActive(false);
        }
        else
        {
            fromError.SetActive(true);
        }
        CheckButtonValid();
    }
    public void SetTo()
    {
        FromToGetSetSystem.ResetTo();
        if (IsNameValid(toInput.text))
        {
            FromToGetSetSystem.SetTo(toInput.text);
            toError.SetActive(false);
        }
        else
        {
            toError.SetActive(true);
        }
        CheckButtonValid();
    }
    public void Confirm()
    {
        SetFrom();
        SetTo();
    }

    public bool IsNameValid(string message)
    {
        return !message.Contains("?");
    }

    public void CheckButtonValid()
    {
        if (FromToGetSetSystem.HasFromAndTo())
        {
            gotoEditPageButton.ActivateButton();
        }
        else
        {
            gotoEditPageButton.DeactivateButton();
        }
    }

}
