using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecorderUIController : MonoBehaviour
{
    enum RecordingState { defaultState, isRecording, recordStop, checkRecord, pauseCheckingRecord, numberOfStates};
    [Tooltip("default, isRecording, recordStop, checkRecord, pauseCheckingRecord")]
    [SerializeField] GameObject[] buttonStates = new GameObject[(int)RecordingState.numberOfStates];
    [SerializeField] GameObject[] msgStates = new GameObject[(int)RecordingState.numberOfStates];

    [SerializeField] Text currentRecordTime;

    [SerializeField] Color recordingTimeColor;
    [SerializeField] Color stopRecordingColor;
    [SerializeField] Color checkingRecordTimeColor;

    [SerializeField] ButtonController reRecordButton;
    [SerializeField] ButtonController sbSaveButton;

    RecordingState currentState;
    float timer = 0;
    float recordedTime = 0;
    bool timerIsRolling = false;
    Recorder recorder;

    private void Awake()
    {
        recorder = GetComponent<Recorder>();
    }

    public void SetDefaultState()
    {
        reRecordButton.DeactivateButton();
        timer = 0;
        recordedTime = 0;
        TurnOnButton(RecordingState.defaultState);
        currentRecordTime.text = "";
        currentState = RecordingState.defaultState;
    }

    public void SetStartState(AudioClip clip)
    {
        if(clip== null)
        {
            SetDefaultState();
        }
        else
        {
            timer = 0;
            StopRecording();
            recordedTime = clip.length;
            SetTime(clip.length);
        }
    }

    public void StartRecording()
    {
        TurnOnButton(RecordingState.isRecording);
        timer = 0;
        timerIsRolling = true;
        currentRecordTime.color = recordingTimeColor;
        currentState = RecordingState.isRecording;
    }

    public void StopRecording()
    {
        reRecordButton.ActivateButton();
        TurnOnButton(RecordingState.recordStop);
        timerIsRolling = false;
        currentRecordTime.color = stopRecordingColor;
        recordedTime = timer;
        currentState = RecordingState.recordStop;
    }

    public void CheckRecording()
    {
        TurnOnButton(RecordingState.checkRecord);
        timer = 0;
        timerIsRolling = true;
        currentRecordTime.color = checkingRecordTimeColor;
        reRecordButton.DeactivateButton();
        currentState = RecordingState.checkRecord;
    }

    public void PauseCheckingRecording()
    {
        TurnOnButton(RecordingState.pauseCheckingRecord);
        timerIsRolling = false;
        reRecordButton.ActivateButton();
        currentState = RecordingState.pauseCheckingRecord;
    }

    public void ResumeRecording()
    {
        TurnOnButton(RecordingState.checkRecord);
        timerIsRolling = true;
        reRecordButton.DeactivateButton();
        currentState = RecordingState.checkRecord;
    }

    public void FinishListeningToRecording()
    {
        TurnOnButton(RecordingState.recordStop);
        timerIsRolling = false;
        timer = 0;
        currentRecordTime.color = stopRecordingColor;
        SetTime(recordedTime);
        reRecordButton.ActivateButton();
        currentState = RecordingState.recordStop;
    }
    public void SetTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        currentRecordTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void SetTime(float time, float baseTime)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int baseminutes = Mathf.FloorToInt(baseTime / 60F);
        int baseseconds = Mathf.FloorToInt(baseTime - baseminutes * 60);

        currentRecordTime.text = string.Format("{0:00}:{1:00} <color=white>/ {2:00}:{3:00}</color>", minutes, seconds, baseminutes, baseseconds);

    }
    public void RerecordSetup()
    {
        SetDefaultState();
        sbSaveButton.DeactivateButton();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (timerIsRolling)
        {
            float baseTime;
            if(currentState == RecordingState.isRecording)
            {
                baseTime = Recorder.maxRecordTime;
                if(timer > baseTime)
                {
                    StopRecording();
                    recorder.StopRecording();
                }
            }
            else
            {
                baseTime = recordedTime;
            }
            timer += Time.deltaTime;
            SetTime(timer, baseTime);
            if (recordedTime > 0f && timer > recordedTime)
            {
                FinishListeningToRecording();
            }
        }
    }

    void TurnOnButton(RecordingState state)
    {
        for(int i = 0; i<(int)RecordingState.numberOfStates; i++)
        {
            if(i == (int)state)
            {
                buttonStates[i].SetActive(true);
                msgStates[i].SetActive(true);
            }
            else
            {
                buttonStates[i].SetActive(false);
                if(msgStates[i] != msgStates[(int)state])
                    msgStates[i].SetActive(false);
            }
        }
    }
}
