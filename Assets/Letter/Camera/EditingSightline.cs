using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EditingSightline : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text text;
    [SerializeField] SnackBar snackBar;
    [SerializeField] MainPanelUI mainUI;

    SBEditingObject onSightlineSB;
    // Update is called once per frame
    void Update()
    {
        if (SBCreator.creator.GetIsSettingSB()||SBEditor.editor.isEditing)
            return;
        SBEditingObject currentLookingObject = CheckForSBOnSightline();
        onSightlineSB = currentLookingObject;
        CallUI(onSightlineSB);
        SendObject2Editor(CheckForClickedSB());
    }
    void CallUI(SBEditingObject sb)
    {
        if (sb == null)
            mainUI.LookingAway();
        else
            mainUI.LookingAtSB(sb.transform.GetSiblingIndex());
    }

    void SendObject2Editor(SBEditingObject sb)
    {
        if(sb != null)
        {
            SBEditor.editor.StartEditingSB(sb);
        }
    }
    SBEditingObject CheckForSBOnSightline()
    {
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {
            return hitInfo.transform.GetComponentInParent<SBEditingObject>();
        }
        return null;
    }

    SBEditingObject CheckForClickedSB()
    {
        if (Input.GetMouseButtonDown(0) && !(EventSystem.current.IsPointerOverGameObject() ||
     EventSystem.current.currentSelectedGameObject != null))
        {
            
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                SBEditingObject hitSB = hitInfo.transform.GetComponentInParent<SBEditingObject>();
                return hitSB;
            }
        }
        return null;
    }

    public void DeleteSB()
    {
        if(onSightlineSB != null)
        {
            snackBar.DeleteObject(onSightlineSB.gameObject);
        }
    }
}

