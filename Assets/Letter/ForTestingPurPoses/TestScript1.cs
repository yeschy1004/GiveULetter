using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
    [SerializeField] Texture2D baseImage;

    [SerializeField] float time;
    [SerializeField] UnityEngine.UI.Image panel;
    private void Start()
    {
        /*
        GetComponent<AudioSource>().time = time;
        GetComponent<AudioSource>().Play();
        */
        panel.material.SetTexture("_MainTex", baseImage);
    }

    void EffectGrayScaleTest()
    {
        //rend.material.SetFloat("_EffectAmount", 1);
    }
    
}
