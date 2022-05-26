using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBCollideDetector : MonoBehaviour
{
    public bool isPlaceable = true;

    bool isBeingPlaced = false;
    Color placeableColor = Color.green;
    Color notPlaceableColor = Color.red;
    Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isBeingPlaced) return;
        SetToUnPlaceableState();
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isBeingPlaced) return;
        SetToPlaceableState();
    }

    public void SetToPlaceableState()
    {
        SetColor(placeableColor);
        isPlaceable = true;
    }

    public void SetToUnPlaceableState()
    {
        SetColor(notPlaceableColor);
        isPlaceable = false;
    }

    public void SetColor(Color color)
    {
        mat.SetColor("_Color", color);
    }

    public void ReturnColor()
    {
        mat.SetColor("_Color", Color.white);
    }

    public void BeingPlaced()
    {
        isBeingPlaced = true;
    }

    public void Set()
    {
        isBeingPlaced = false;
        ReturnColor();
    }
}
