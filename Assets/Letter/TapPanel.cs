using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapPanel : MonoBehaviour
{
    public static TapPanel tapPanel;
    [SerializeField] RawImage image;
    [SerializeField] RectTransform mainCanvas;
    [SerializeField] Sightline sightline;
    [SerializeField] Text time;
    [SerializeField] SBParent sbParent;
    [SerializeField] GameObject topUI;
    AudioSource audioSource;
    Animator anim;
    AudioClip currentClip;
    bool isPlaying = false;
    SBTriggerFilterer calledBalloon;
    string audioLength;
    private void Awake()
    {
        Setup();
        image.material.SetTexture("_MainTex", null);
    }

    public void TurnPanelOn(SBTriggerFilterer trigger, AudioClip clip, Texture2D pic)
    {
        topUI.SetActive(false);
        sbParent.PauseSBsAfterThisSB(trigger.transform);
        calledBalloon = trigger;
        sightline.enabled = false;
        ChangePicture(pic);
        currentClip = clip;
        audioSource.enabled = true;
        anim.SetTrigger("Show");
    }

    public void TurnPanelOff()
    {
        topUI.SetActive(true);
        sbParent.UnPauseSBsAfterThisSB(calledBalloon.transform);
        audioSource.Stop();
        audioSource.enabled = false;
        isPlaying = false;
        anim.SetTrigger("Hide");
        sightline.enabled = true;
        calledBalloon.OnPanelFinished();
    }

    public void StartClip()
    {
        audioSource.clip = currentClip;
        if (audioSource.clip == null) return;
        audioSource.time = 0;
        audioSource.Play();
        audioLength = " / " + TimeFloat2String(currentClip.length);
        isPlaying = true;
    }

    public void ResumeClip()
    {
        audioSource.UnPause();
    }

    public void PauseClip()
    {
        audioSource.Pause();
    }

    private void Update()
    {
        if (isPlaying)
        {
            if(audioSource.time >= currentClip.length)
            {
                TurnPanelOff();
                return;
            }
            time.text = "<b>" + TimeFloat2String(audioSource.time) + audioLength + "</b>";
        }
    }


    void ChangePicture(Texture2D pic)
    {
        image.material.SetTexture("_MainTex", pic);
        image.texture = pic;
        
        if (pic.width > pic.height)
        {
            if (pic.height * mainCanvas.rect.width / pic.width <= mainCanvas.rect.height)
            {
                image.rectTransform.sizeDelta = new Vector2(mainCanvas.rect.width, pic.height * mainCanvas.rect.width / pic.width);
                return;
            }
        }   
        image.rectTransform.sizeDelta = new Vector2(pic.width * mainCanvas.rect.height / pic.height, mainCanvas.rect.height);
    }

    void SetSingleTon()
    {
        if(tapPanel == null)
        {
            tapPanel = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Setup()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        SetSingleTon();
    }

    string TimeFloat2String(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
