using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Storage;

using System.Threading.Tasks;
using System.Threading;

using UnityEngine.UI;

public class ImportLetter : MonoBehaviour
{
    public Text testText;
    public Text codeInputTxt;

    static string saveFolderPath;

    string letterKey;

    public static string tempt = null;

    public static string senderReceiverNameSendedDate;
    static string senderName;
    static string receiverName;
    static string sendedDate;
    static string letterTitle;
    static string receivedDate;

    
    //해당 리스트를 json에서 이미 읽어서 할당했다고 가정함
    string []audioLists = { "rara.wav", "riri.mp3", "roro.mp3" };
    string []pictureLists = {"lala.png", "lili.png", "lolo.jpg" };


    // Start is called before the first frame update
    void Start()
    {
        saveFolderPath = Application.persistentDataPath + "/savefiles/";

    }

    #region 수신/발신 정보를 입력창(key값) or 서버에서 Get
    public string Get_LetterKey()
    {
        return letterKey;
    }

    public string Get_LetterTitle()
    {
        return letterTitle;
    }

    public string Get_SendedDate()
    {
        return sendedDate;
    }

    public string Get_ReceivedDate()
    {
        return receivedDate;
    }
    #endregion

    #region Storage에서 녹음, 사진파일들 Get
    public void Get_LetterAudioPictureInfo()
    {
        Get_allFiles("Audios", audioLists);
        Get_allFiles("Pictures", pictureLists);
    }

    //나중에 동영상 파일 등으로도 확장할 수 있어서 함수를 분리함
    public void Get_allFiles(string folderName, string []list)
    {
        Debug.Log("list cnt: " + list.Length);

        for(int i=0; i<list.Length; i++)
        {
            Get_FolderFiles(folderName, list[i]);
            
        }
    }

    //녹음파일, 사진 이름 받은거 읽어서 서버에서 다운로드
    void Get_FolderFiles(string folderName, string fileName) //Audios, Pictures
    {
        saveFolderPath = Application.persistentDataPath + "/savefiles/";
        saveFolderPath += letterKey + "/" + folderName + "/"; //다운로드할 최종 경로

        string saveFilePath = saveFolderPath;

        if (!Directory.Exists(saveFolderPath)) //폴더 없으면 디렉토리 생성
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference storageRef = storage.GetReferenceFromUrl("gs://soundspace-cf041.appspot.com/");


        StorageReference fileRef = storageRef.Child("GiveLetter").Child(letterKey).Child(folderName).Child(fileName);

        saveFilePath += fileName;

        // Debug.Log("Create File Path: " + saveFilePath);
        // Debug.Log("Create Folder Path: " + saveFolderPath);

        Task task = fileRef.GetFileAsync(saveFilePath,
            new StorageProgress<DownloadState>((DownloadState state) => {
                Debug.Log(string.Format("Progress: {0} of {1} bytes transferred.",
                    state.BytesTransferred,
                    state.TotalByteCount));
            }), CancellationToken.None);

        task.ContinueWith(resultTask =>
        {
            if (!resultTask.IsFaulted && !resultTask.IsCanceled)
            {
                Debug.Log("Json Download finished.");
            }
            else
            {
                Debug.Log("Json Download failed: " + task.Status.ToString());
            }
        });

        saveFolderPath = Application.persistentDataPath + "/savefiles/";

    }

    #endregion

    #region Storage에서 Json파일 Get
    //storage로 부터 json 파일 다운로드
    //여러번할 때 초기화도 생각하셈
    void Get_JsonFile()
    {
        saveFolderPath += letterKey + "/";
        string saveFilePath = saveFolderPath;
        if (Directory.Exists(saveFolderPath)) //폴더 있으면
        {
            //다운로드 할 필요 없음, 이미 다운로드 되어있는 표시 해주기 
            Debug.Log("Folder Exists: " + saveFolderPath);
        }
        else //폴더 없으면
        {

            Directory.CreateDirectory(saveFolderPath);
            
            FirebaseStorage storage = FirebaseStorage.DefaultInstance;
            StorageReference storageRef = storage.GetReferenceFromUrl("gs://soundspace-cf041.appspot.com/");

            /*
            Debug.Log("1. sender name: " + senderName);
            Debug.Log("2. receiver name: " + receiverName);
            Debug.Log("3. date: " + sendedDate);
            Debug.Log("4. name date: " + senderReceiverNameSendedDate);
            */

            Debug.Log("파일 제목표기: " + letterTitle);
            Debug.Log("보낸날짜: " + sendedDate);

            StorageReference fileRef = storageRef.Child("GiveLetter").Child(letterKey).Child(senderReceiverNameSendedDate + ".json");


            saveFilePath += "letter.json"; //무조건 letter.json 으로

            Debug.Log("Create File Path: " + saveFilePath);
            Debug.Log("Create Folder Path: " + saveFolderPath);

            Task task = fileRef.GetFileAsync(saveFilePath,
                new StorageProgress<DownloadState>((DownloadState state) => {
                    Debug.Log(string.Format("Progress: {0} of {1} bytes transferred.",
                        state.BytesTransferred,
                        state.TotalByteCount));
                }), CancellationToken.None);

            task.ContinueWith(resultTask =>
            {
                
                if (!resultTask.IsFaulted && !resultTask.IsCanceled)
                {
                    Debug.Log("Json Download finished.");
                }
                else
                {
                    Debug.Log("Json Download failed: " + task.Status.ToString());
                }
            });

        }

        saveFolderPath = Application.persistentDataPath + "/savefiles/";
    }
    #endregion


    //이하는 몰라도 됨

    public void Get_LetterJsonInfo()
    {
        letterKey = codeInputTxt.text;

        Get_LetterNameDateInfo("ltrSenderReceiverNameSendedDate");

        saveFolderPath = Application.persistentDataPath + "/savefiles/";


    }

    void Get_LetterNameDateInfo(string element)
    {
        FirebaseDatabase.DefaultInstance.GetReference("GiveLetter").Child(letterKey).Child(element).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Firebase Database-Single [" + element + "] failed");

            }
            else if (task.IsCompleted)
            {
                char splitStr = '?';

                senderReceiverNameSendedDate = task.Result.Value.ToString();

                string[] tempStr = senderReceiverNameSendedDate.Split(splitStr);

                senderName = tempStr[0];
                receiverName = tempStr[1];
                sendedDate = tempStr[2];
                letterTitle = Set_LetterTitle(senderName, receiverName);
                receivedDate = System.DateTime.Now.ToString("yyyy.MM.dd HH:mm");

                Debug.Log("Firebase Database-Single [" + element + "] OK: " + senderName + ", " + receiverName + ", " + sendedDate + ", " + receivedDate);

                //jsonFile 먼저 다운로드
                Get_JsonFile();

                //txt파일에 기록


                //GameObject.Find("SystemObject").GetComponent<MailboxController>().Save_LetterInfoTxtFile(letterTitle, letterKey, sendedDate, receivedDate);
            }
        });
    }


    string Set_LetterTitle(string senderN, string receiverN)
    {
        return "To " + receiverN + ", From " + senderN;
    }


}