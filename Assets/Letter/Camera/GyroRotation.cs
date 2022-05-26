using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotation : MonoBehaviour, ICameraRotation
{
    [SerializeField] float rotSmoothness = 0.32f;

    private Quaternion cameraBaseOrientation;
    private Quaternion gyroBaseOrientation;
    private readonly Quaternion correctionRot = Quaternion.Euler(90f, 0f, 0f);

    private bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (pause) return;
        Rotate();
    }

    void Setup()
    {
        Input.gyro.enabled = true;
        cameraBaseOrientation = transform.rotation;
        ResetBase();
    }

    void Rotate()
    {
        Quaternion offsetRotation = gyroBaseOrientation * Input.gyro.attitude;
        offsetRotation = GyroToUnity(offsetRotation);
        Vector3 finalRotation = Quaternion.Slerp(transform.rotation, offsetRotation * cameraBaseOrientation, rotSmoothness).eulerAngles;
        finalRotation.z = 0;
        transform.rotation = Quaternion.Euler(finalRotation);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    
    public void ResetBase()
    {
        gyroBaseOrientation = Quaternion.Inverse(Input.gyro.attitude);
    }
    public void PauseRotation()
    {
        pause = true;
    }
    public void UnPauseRotation()
    {
        pause = false;
    }
}
