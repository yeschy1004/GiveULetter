using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector3 rotation;
    private void Awake()
    {
        rotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.z = -player.eulerAngles.y;
        transform.eulerAngles = rotation;
    }
}
