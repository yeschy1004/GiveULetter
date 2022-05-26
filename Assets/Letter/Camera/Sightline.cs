using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sightline : MonoBehaviour
{
    [SerializeField] float maxSightline = 100f;

    SBTriggerFilterer currentlyInteractingSB = null;
    LineRenderer lineRenderer;
    Vector3 lineEndPosition;

    // Start is called before the first frame update
    void Start()
    {
        SetLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfNewSB(CheckHitSB()))
        {
            SendLookTrigger();
        }
        SendTapTrigger(CheckForClickedSB());
        
    }

    SBTriggerFilterer CheckHitSB()
    {
        lineEndPosition = transform.forward * maxSightline;

        RaycastHit hit;
        SBTriggerFilterer interactingSB;
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxSightline)
            &&(interactingSB  = hit.transform.gameObject.GetComponentInParent<SBTriggerFilterer>()))
        {
            lineEndPosition = hit.point;
            return interactingSB;
        }
        return null;
    }

    bool SendTapTrigger(SBTriggerFilterer tappedSB)
    {
        if (tappedSB != null)
        {
            tappedSB.OnTap();
            return true;
        }
        return false;
    }
    SBTriggerFilterer CheckForClickedSB()
    {
        if (Input.GetMouseButtonDown(0)&& !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                SBTriggerFilterer hitSB = hitInfo.transform.GetComponentInParent<SBTriggerFilterer>();
                return hitSB;
            }
            else
            {
                PlayPageUIController.mainUI.ShowTopUI();
            }
        }
        return null;
    }
    bool CheckIfNewSB(SBTriggerFilterer newSB)
    {
        if (newSB == currentlyInteractingSB)
            return false;
        if(currentlyInteractingSB != null)
        {
            currentlyInteractingSB.NotLooking();
        }
        currentlyInteractingSB = newSB;
        return true;
    }

    void SendLookTrigger()
    {
        if (currentlyInteractingSB == null)
            return;
        currentlyInteractingSB.OnLook();
    }

    #region lineRendererSettingsForTesting
    void SetLineRenderer()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void SetLineState()
    {
        Color lineColor = Color.white;//lineHitSomething ? Color.green : Color.red;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.SetPosition(0, transform.position - new Vector3(0, 0.5f, 0));
        lineRenderer.SetPosition(1, lineEndPosition);
    }

    #endregion
}
