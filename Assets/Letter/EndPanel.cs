using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] Sightline sightline;

    private void OnEnable()
    {
        sightline.enabled = false;
    }
    private void OnDisable()
    {
        sightline.enabled = true;
    }
}
