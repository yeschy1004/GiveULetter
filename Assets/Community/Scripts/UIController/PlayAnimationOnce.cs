using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnce : MonoBehaviour
{
    public Animation once_anim;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        once_anim.wrapMode = WrapMode.Once;
    }
}
