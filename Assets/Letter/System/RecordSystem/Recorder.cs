using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    Recording recording = new Recording();
    AudioSource audioSource;
    bool isRecording;
    bool isPlaying;
    int numberOfRecords = 0;
    string defaultMessage = "버튼을 눌러 녹음을 시작하세요";
    public static readonly int maxRecordTime = 120;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnDisable()
    {
        audioSource.Stop();
        StopRecording();
        recording.Reset();
    }
    
    public void StartRecording()
    {
        recording.audio = Microphone.Start(null, true, maxRecordTime, 44100);
        //Microphone.devices[0] or null
        isRecording = true;
    }
    
    public void StopRecording()
    {
        isRecording = false;
        if(recording.audio != null)
            recording.audio = SavWav.TrimSilence(recording.audio, 0);
        SBEditor.editor.SetVoiceClip(recording);
    }

    public void PlayRecording()
    {
        isPlaying = true;
        audioSource.clip = recording.audio;
        audioSource.Play();
    }

    public void PauseCheckingRecording()
    {
        audioSource.Pause();
    }

    public void ResumeCheckRecording()
    {
        audioSource.Play();
    }

    public Recording SaveRecording()
    {
        if(recording.audio == null)
        {
            return null;
        }
        string filename = "Recording" + numberOfRecords.ToString() + ".wav";
        recording.audio.name = filename;
        SavWav.Save(filename, recording.audio);
        recording.path = System.IO.Path.Combine(SaveSystem.tempFolderPath, filename);
        numberOfRecords++;
        return recording;
    }

    public bool GetIsRecording()
    {
        return isRecording;
    }
}
