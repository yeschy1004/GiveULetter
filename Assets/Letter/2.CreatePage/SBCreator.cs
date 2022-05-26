using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBCreator : MonoBehaviour
{
    public static SBCreator creator;

    [SerializeField] GameObject SBEditingObjectPrefab;
    
    [SerializeField] Transform player;
    [SerializeField] float distance;
    [SerializeField] ButtonController setSBButton;
    [SerializeField] ButtonController addSBButton;
    Transform currentMovingObject;
    SBCollideDetector currentSBCollideDetect;
    bool isSettingSB = false;
    int maxSB = 10;
    Transform SBParent;
    private void Start()
    {
        if (creator == null)
        {
            creator = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SBParent = SBConverter.converter.transform;
        SBConverter.converter.TurnOnChildren();
    }

    private void Update()
    {
        if (!isSettingSB) return;
        currentMovingObject.position = player.forward * distance;
        FacePlayer(currentMovingObject);
        if (!currentSBCollideDetect.isPlaceable) setSBButton.DeactivateButton();
        else setSBButton.ActivateButton();
    }

    public void AddSB()
    {
        currentMovingObject = Instantiate(SBEditingObjectPrefab, player.forward * distance, Quaternion.identity, SBParent).transform;
        currentSBCollideDetect = currentMovingObject.GetComponentInChildren<SBCollideDetector>();
        currentSBCollideDetect.SetToPlaceableState();
        isSettingSB = true;
        currentSBCollideDetect.BeingPlaced();
    }

    public void FacePlayer(Transform sb)
    {
        Vector3 lookPoint = sb.position - player.transform.position;
        lookPoint += sb.position;
        sb.LookAt(lookPoint);
    }

    public void SetSB()
    {
        isSettingSB = false;
        currentMovingObject = null;
        currentSBCollideDetect.Set();
        currentSBCollideDetect = null;
        setSBButton.DeactivateButton();

        if(SBParent.childCount > maxSB)
        {
            addSBButton.DeactivateButton();
        }
    }

    public bool GetIsSettingSB()
    {
        return isSettingSB;
    }
}
