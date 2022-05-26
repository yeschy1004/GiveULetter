using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackBar : MonoBehaviour
{
    GameObject holdingObject;
    [SerializeField] UnityEngine.UI.Text text;
    string message = " 서랍이 방금 삭제 되었습니다.";
    float timer = 0;
    float snackbarLastTime = 6f;

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > snackbarLastTime)
        {
            PermanentDelete();
            gameObject.SetActive(false);
        }
    }
    public void DeleteObject(GameObject sb)
    {
        text.text = MainPanelUI.numberInKorean[sb.transform.GetSiblingIndex()] + message;
        if (holdingObject != null && holdingObject != sb)
        {
            Destroy(holdingObject);
        }

        holdingObject = sb;
        holdingObject.SetActive(false);

        MainPanelUI.mainUIController.CheckIfSavable();
    }

    public void PermanentDelete()
    {
        if (holdingObject != null)
            Destroy(holdingObject);

        MainPanelUI.mainUIController.CheckIfSavable();
    }

    public void RestoreObject()
    {
        if(holdingObject != null)
            holdingObject.SetActive(true);
        holdingObject = null;

        MainPanelUI.mainUIController.CheckIfSavable();
    }
}
