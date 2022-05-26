using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour, ICameraRotation
{
    [SerializeField] float sensX = 100f;
    [SerializeField] float sensY = 100f;

    [SerializeField] private bool paused;
    float rotationX = 0f;
    float rotationY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) return;
        Rotate();
    }

    void Rotate()
    {
        if (Input.GetMouseButton(0))
        {
            rotationX += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(-rotationY, rotationX, 0));
        }
    }

    public void ResetBase()
    {
        transform.rotation = Quaternion.identity;
        rotationX = 0;
        rotationY = 0;
    }

    public void PauseRotation()
    {
        paused = true;
    }
    public void UnPauseRotation()
    {
        paused = false;
    }
}
