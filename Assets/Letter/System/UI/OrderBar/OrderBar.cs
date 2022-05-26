using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderBar : MonoBehaviour
{
    public static OrderBar orderBar;
    readonly public static string[] numberInKorean = { "첫 번째", "두 번째", "세 번째", "네 번째", "다섯 번째", "여섯 번째", "일곱 번째", "여덟 번째", "아홉 번째", "열 번째", };
    [SerializeField] Text letterProgress;
    [SerializeField] Text tapMessage;
    [SerializeField] Image filler;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        SetSingleTon();
    }

    private void SetSingleTon()
    {
        if(orderBar == null)
        {
            orderBar = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowBar(int number)
    {
        int numberOfSBs = LoadSystem.numberOfSBs;
        letterProgress.text = number + "/" + numberOfSBs;
        filler.fillAmount = (float)number / numberOfSBs;
        tapMessage.text = numberInKorean[number - 1] + " 사진을 탭하여 들어보세요!";
        anim.SetTrigger("Show");
        anim.ResetTrigger("Hide");
    }
    public void HideBar()
    {
        anim.SetTrigger("Hide");
        anim.ResetTrigger("Show");
    }
}
