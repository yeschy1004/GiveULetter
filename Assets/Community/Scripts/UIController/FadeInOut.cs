using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class FadeInOut : MonoBehaviour
{
    public float FadeTime = 2f; // Fade효과 재생시간
    Image fadeImg;

    float start;
    float end;
    float time = 0f;
    bool isPlaying = false;

    void Awake()
    {
        fadeImg = GetComponent<Image>();
        
        InStartFadeAnim();
    }

    #region Fade out
    public void OutStartFadeAnim(){
        //중복재생방지
        if (isPlaying == true){
            return;
        }

        start = 1f;
        end = 0f;

        //코루틴 실행
        StartCoroutine("fadeoutplay");  
    }
    IEnumerator fadeoutplay()
    {
        isPlaying = true;
        Color fadecolor = fadeImg.color;

        time = 0f;
        fadecolor.a = Mathf.Lerp(start, end, time);

        while (fadecolor.a > 0f)
        {
            time += Time.deltaTime / FadeTime;
            fadecolor.a = Mathf.Lerp(start, end, time);
            fadeImg.color = fadecolor;
            yield return null;
        }

        isPlaying = false;

    }
    #endregion

    #region Fade in
    public void InStartFadeAnim(){
        //중복재생방지
        if (isPlaying == true) {
            return;
        }

        start = 0f;
        end = 1f;

        StartCoroutine("fadeIntanim");
    }

    IEnumerator fadeIntanim(){
        isPlaying = true;
        Color fadecolor = fadeImg.color;
        time = 0f;
        fadecolor.a = Mathf.Lerp(start, end, time);

        while (fadecolor.a < 1f){
            time += Time.deltaTime / FadeTime;
            fadecolor.a = Mathf.Lerp(start, end, time);
            fadeImg.color = fadecolor;
            yield return null;
        }

        isPlaying = false;

    }
    #endregion

}
