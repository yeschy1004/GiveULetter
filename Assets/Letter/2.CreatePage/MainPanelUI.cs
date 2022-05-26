using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelUI : MonoBehaviour
{
    [SerializeField] Text sbName;
    [SerializeField] ButtonController letterSaveButton;
    [SerializeField] ButtonController letterPreviewButton;
    [SerializeField] ButtonController deleteButton;
    [SerializeField] ButtonController addButton;

    readonly public static string[] numberInKorean = { "첫 번째", "두 번째", "세 번째", "네 번째", "다섯 번째", "여섯 번째", "일곱 번째", "여덟 번째", "아홉 번째", "열 번째", };
    public static MainPanelUI mainUIController;

    private void Awake()
    {
        if(mainUIController == null)
        {
            mainUIController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
         Setup();
    }


    void Setup()
    {
        if (SaveSystem.saveSystem.IsSaveable())
        {
            letterSaveButton.ActivateButton();
            letterPreviewButton.ActivateButton();
            addButton.ActivateButton();
            deleteButton.ActivateButton();
            deleteButton.gameObject.SetActive(true);
        }
        else
        {
            letterSaveButton.DeactivateButton();
            letterPreviewButton.DeactivateButton();
            addButton.DeactivateButton();
            deleteButton.DeactivateButton();
            deleteButton.gameObject.SetActive(false);
        }
    }

    public void CheckIfSavable()
    {
        if (SaveSystem.saveSystem.IsSaveable())
        {
            letterSaveButton.ActivateButton();
            letterPreviewButton.ActivateButton();
        }
        else
        {
            letterSaveButton.DeactivateButton();
            letterPreviewButton.DeactivateButton();
        }
    }
    
    // Start is called before the first frame update

    public void SetCurrentSBName(int number)
    {
        
        if (number == -1)
            sbName.text = "";
        else
            sbName.text = numberInKorean[number] + " 서랍";
    }

    public void LookingAtSB(int sbNumber)
    {
        if (SBEditor.editor.isEditing) return;
        SetCurrentSBName(sbNumber);
        if (!SBCreator.creator.GetIsSettingSB())
        {
            deleteButton.ActivateButton();
        }
    }
    public void LookingAway()
    {
        SetCurrentSBName(-1);
        deleteButton.DeactivateButton();
    }
}
