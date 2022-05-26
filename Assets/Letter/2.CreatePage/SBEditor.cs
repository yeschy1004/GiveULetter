using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBEditor : MonoBehaviour
{
    public static SBEditor editor;
    [SerializeField] EditorUIController editorPanelUI;
    [SerializeField] Recorder recorder;
    [SerializeField] RecorderUIController recordUI;
    /*
    [SerializeField] float radius = 10f;
    [SerializeField] float polar = 5f;
    [SerializeField] float azimuthal = 5f;
    */
    
    [SerializeField] new CameraManager camera;

    int maxSB = 10;
    SBEditingObject currentEditingBalloon;
    public bool isEditing = false;
    int pictureMaxSize = 512;
    Vector3 originalSBPos;
    Picture newPicture;
    Recording newRecord;

    private void Awake()
    {
        if (editor == null)
        {
            newPicture = new Picture();
            newRecord = new Recording();
            editor = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartEditingSB(SBEditingObject sb)
    {
        camera.PauseCamera();

        currentEditingBalloon = sb;
        newPicture.CopyPic(sb.GetPicture());
        newRecord.CopyRecord(sb.GetRecording());

        SBConverter.converter.TurnOffChildren();

        editorPanelUI.OpenEditorUI(newPicture, sb.transform.GetSiblingIndex());
        editorPanelUI.CheckIfSaveable();

        recordUI.SetStartState(newRecord.audio);    

        isEditing = true;
    }

    public void CancelEditing()
    {
        currentEditingBalloon = null;

        newPicture.Reset();
        newRecord.Reset();

        editorPanelUI.CloseEditorUI();

        camera.UnPauseCamera();

        isEditing = false;
    }


    #region SBMovement Setter - obsolete
    /*
     * 
    public void SetSBPos(Vector3 pos)
    {
        SBEditingObject newSB = Instantiate(SBPrefab, pos, Quaternion.identity).GetComponent<SBEditingObject>();
        newSB.orderNumber = ++numberOfSB;
        SetCurrentEditingEB(newSB);

    }
    void PrepareSphericalVar()
    {
        polar *= Mathf.Deg2Rad;
        azimuthal *= Mathf.Deg2Rad;
    }
    public void MoveLeft()
    {
        currentBalloon.currSpherePos.polar += polar;
        currentBalloon.transform.position = SphericalCoordinates.Spherical2Cartesian(currentBalloon.currSpherePos);
        currentBalloon.FacePlayer();
    }

    public void MoveRight()
    {
        currentBalloon.currSpherePos.polar -= polar;
        currentBalloon.transform.position = SphericalCoordinates.Spherical2Cartesian(currentBalloon.currSpherePos);
        currentBalloon.FacePlayer();
    }

    public void MoveUp()
    {
        currentBalloon.currSpherePos.azimuthal -= azimuthal;
        currentBalloon.transform.position = SphericalCoordinates.Spherical2Cartesian(currentBalloon.currSpherePos);
        currentBalloon.FacePlayer();

    }

    public void MoveDown()
    {
        currentBalloon.currSpherePos.azimuthal += azimuthal;
        currentBalloon.transform.position = SphericalCoordinates.Spherical2Cartesian(currentBalloon.currSpherePos);
        currentBalloon.FacePlayer();

    }
    */
    #endregion

    #region SB property Setter
    public void SaveSBSettings()
    {
        newRecord.CopyRecord(recorder.SaveRecording());
        currentEditingBalloon.SetPicture(newPicture);
        if (newRecord.audio != null)
        {
            currentEditingBalloon.SetRecord(newRecord);
        }
        currentEditingBalloon = null;

        newPicture.Reset();
        newRecord.Reset();

        editorPanelUI.CloseEditorUI();

        

        camera.UnPauseCamera();

        isEditing = false;
    }

    public void SetPicture()
    {
        Debug.Log("SettingPicture");
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery(
            (path) =>
            {
                if (path != null)
                {
                    newPicture.path = path;
                    newPicture.texture = NativeGallery.LoadImageAtPath(path, pictureMaxSize);
                    if (newPicture.texture == null || path.Contains("?"))
                    {
                        Debug.Log("problem");
                        return;
                    }
                    else
                    {
                        editorPanelUI.SetPreviewPicture(newPicture.texture);
                        editorPanelUI.CheckIfSaveable();

                        Debug.Log("no problem");
                    }
                }
            }, "사진을 골라주세요", "image/*");
    }

    public void SetVoiceClip(Recording voice)
    {
        newRecord.CopyRecord(voice);
        editorPanelUI.CheckIfSaveable();
    }


    #endregion
    public bool IsSBSavable()
    {
        return newRecord.audio != null && !newPicture.IsEmpty();
    }

    public Recording GetCurrentRecording()
    {
        return newRecord;
    }
    #region TestingPurpose
    public void PrintCurrBubble()
    {
        currentEditingBalloon.GetComponent<SBTriggerFilterer>().PrintInfo();
    }
    #endregion
}
