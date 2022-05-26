using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorUIController : MonoBehaviour
{
    [SerializeField] MainPanelUI panelUI;
    [SerializeField] RawImage picPreview;
    [SerializeField] Image pictureStatus;
    [SerializeField] GameObject editingPanel;
    [SerializeField] GameObject mainPanel;
    [SerializeField] ButtonController sbSettingSaveButton;
    [SerializeField] Sprite setPictureMsg;
    [SerializeField] Sprite pictureSetMsg;

    readonly string nameDefault = "이미지 없음";
    Vector2 defaultSizeDelta;
    RectTransform picturePreviewDefaultSize;

    void Start()
    {
        defaultSizeDelta = picPreview.rectTransform.sizeDelta;
    }

    public void OpenEditorUI(Picture newPicture, int sbNumber)
    {

        editingPanel.SetActive(true);

        SetPreviewPicture(newPicture.texture);
        pictureStatus.sprite = setPictureMsg;
        
        panelUI.SetCurrentSBName(sbNumber);

        CheckIfSaveable();

        mainPanel.SetActive(false);
    }

    public void CloseEditorUI()
    {
        SBConverter.converter.TurnOnChildren();
        mainPanel.SetActive(true);
        editingPanel.SetActive(false);
        panelUI.CheckIfSavable();
    }

    public void SetPreviewPicture(Texture2D texture)
    {
        picPreview.rectTransform.sizeDelta = defaultSizeDelta;
        picPreview.texture = texture;
        if (texture.height < texture.width)
        {
            picPreview.rectTransform.sizeDelta = new Vector3(picPreview.rectTransform.sizeDelta.x, texture.height * picPreview.rectTransform.sizeDelta.x / texture.width, 1f);
        }
        else
        {
            picPreview.rectTransform.sizeDelta = new Vector3(texture.width * picPreview.rectTransform.sizeDelta.y / texture.height, picPreview.rectTransform.sizeDelta.y, 1f);
        }
        pictureStatus.sprite = pictureSetMsg;
    }

    public void CheckIfSaveable()
    {
        if (SBEditor.editor.IsSBSavable())
        {
            sbSettingSaveButton.ActivateButton();
        }
        else
        {
            sbSettingSaveButton.DeactivateButton();
        }
    }
}
