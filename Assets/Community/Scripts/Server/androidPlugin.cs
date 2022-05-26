using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class androidPlugin : MonoBehaviour
{

    public void Onclick_FeedTemplate()
    {
        AndroidJavaObject kotlin = new AndroidJavaObject("android.SilverliningStudio.GiveULetter.KakaoPlugin");
        kotlin.Call("FeedTemplate");
    }
}
