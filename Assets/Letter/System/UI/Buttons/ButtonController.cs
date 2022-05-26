using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Sprite activatedSprite;
    [SerializeField] Sprite deactivatedSprite;
    [SerializeField] Color activatedTextColor;
    [SerializeField] Color deactivatedTextColor;

    Image image;
    Button button;
    Text text;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
    }

    public void ActivateButton()
    {
        if (image == null || button == null) return;
        SetSprite(activatedSprite);
        button.enabled = true;
        if(text!= null)
        {
            text.color = activatedTextColor;
        }
    }

    public void DeactivateButton()
    {
        if (image == null || button == null) return;
        
        SetSprite(deactivatedSprite);
        button.enabled = false;
        if (text != null)
        {
            text.color = deactivatedTextColor;
        }
    }

    void SetSprite(Sprite sprite)
    {
        if(sprite == null)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
            image.sprite = sprite;
        }
    }
}
