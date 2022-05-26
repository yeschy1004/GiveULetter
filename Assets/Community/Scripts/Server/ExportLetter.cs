using System.IO;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Storage;

public class ExportLetter : MonoBehaviour
{
   //임의로 지정한 녹음파일과 사진파일 path들, 해당 path만 알면 알아서 파일이름이랑 path로 서버 전송할 예정
   //따라서 각 파일 path를 get해오는 함수를 해당 변수에 연결해주면 됨
    string[] audioNames = { "C:/Users/Hayoun/AppData/LocalLow/DefaultCompany/GiveULetter(New)/savefiles/temp/rara.wav", "C:/Users/Hayoun/AppData/LocalLow/DefaultCompany/GiveULetter(New)/savefiles/temp/riri.mp3", "C:/Users/Hayoun/AppData/LocalLow/DefaultCompany/GiveULetter(New)/savefiles/temp/roro.mp3" };
    string[] pictureNames = { "C:/Users/Hayoun/AppData/LocalLow/DefaultCompany/GiveULetter(New)/savefiles/temp/lala.png", "C:/Users/Hayoun/AppData/LocalLow/DefaultCompany/GiveULetter(New)/savefiles/temp/lili.png", "C:/Users/Hayoun/AppData/LocalLow/DefaultCompany/GiveULetter(New)/savefiles/temp/lolo.jpg" };


    string tempFolderPath;

    string letterKey;
    string sendedDate;

    string senderNameTxt;
    string receiverNameTxt;
    string senderReceiverNameSendedDate;

    string localJsonFileName;


    // Start is called before the first frame update
    void Start()
    {
        //초기 보내는 파일이름 setting
        Set_LocalJsonFileName("temp.json");

        tempFolderPath = Application.persistentDataPath + "/savefiles/temp/";
    }

    #region 수신/발신 정보 UI에서 Get
    // get 함수들
    public string Get_SenderName()
    {
        return senderNameTxt;
    }

    public string Get_LetterKey()
    {
        return letterKey;
    }

    public void Set_SenderName(string name)
    {
        senderNameTxt = name;
    }

    public void Set_ReceiverName(string name)
    {
        receiverNameTxt = name;
    }

    public string Get_ReceiverName()
    {
        return receiverNameTxt;
    }

    public string Get_SendedDate()
    {
        return sendedDate;
    }
    #endregion


    #region json파일 이름 Set (지워도 됨)
    public void Set_LocalJsonFileName(string name)
    {
        localJsonFileName = name;
    }
    #endregion


    public void UploadLetterToServer()
    {
        UploadLetterToServerDB(); //파일 정보를 DB 서버로 전송
        UploadJsonToServerStorage(); //json 파일을 Storage 서버로 전송
        UploadFolderToServerStorage(audioNames, "Audios"); //녹음 파일을 Storage 서버로 전송
        UploadFolderToServerStorage(pictureNames, "Pictures"); //사진 파일을 Storage 서버로 전송
    }


    void DeleteTempFolder()
    {
        Directory.Delete(tempFolderPath, true);
    }

    // DB

    void UploadLetterToServerDB() //콘텐츠 관련 데이터를 Firebase Database로 저장
    {
        DatabaseReference databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        letterKey = databaseRef.Child("GiveLetter").Child("ltrId").Push().Key;
        databaseRef.Child("GiveLetter").Child(letterKey).Child("ltrId").SetValueAsync(letterKey);

        sendedDate = System.DateTime.Now.ToString("yyyy.MM.dd HH:mm");

        // 수신,발신인 이름
        senderNameTxt = GameObject.Find("TemptObject").GetComponent<TemptLetterInfo>().Get_SenderNameTxt();
        receiverNameTxt = GameObject.Find("TemptObject").GetComponent<TemptLetterInfo>().Get_ReceiverNameTxt();


        senderReceiverNameSendedDate = senderNameTxt + "?" + receiverNameTxt + "?" + sendedDate;

        databaseRef.Child("GiveLetter").Child(letterKey).Child("ltrSenderReceiverNameSendedDate").SetValueAsync(senderReceiverNameSendedDate);


        //json, picture, audio 파일 경로 저장
        databaseRef.Child("GiveLetter").Child(letterKey).Child("ltrStoragePath").SetValueAsync("GiveLetter/" + letterKey);


    }

    void UploadJsonToServerStorage() // 콘텐츠 데이터 중 Json 파일을 Firebase Storage에 저장
    {
        string jsonFileName = senderReceiverNameSendedDate + ".json";

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference storageRef = storage.GetReferenceFromUrl(GameObject.Find("SystemObject").GetComponent<ServerConnect>().GetServerStorageUrl(1));
        StorageReference fileRef = storageRef.Child("GiveLetter/" + letterKey + "/" + jsonFileName); // firebase storage상 위치상 저장할 위치 및 이름(titleText.text)

        fileRef.PutFileAsync(tempFolderPath + localJsonFileName).ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
            }
            else
            {
                Firebase.Storage.StorageMetadata metadata = task.Result;

                Debug.Log("[Server-Storage] JsonFile Uploaded: " + "GiveLetter/" + letterKey + "/" + jsonFileName);

            }
        });
    }

    void UploadFolderToServerStorage(string[] filePath, string folderName)
    {
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference storageRef = storage.GetReferenceFromUrl(GameObject.Find("SystemObject").GetComponent<ServerConnect>().GetServerStorageUrl(1));
        string fileName;

        for (int i = 0; i < filePath.Length; i++)
        {
            fileName = Path.GetFileName(filePath[i]);
            StorageReference fileRef = storageRef.Child("GiveLetter/" + letterKey + "/" + folderName + "/" + fileName); // firebase storage상 위치상 저장할 위치, 이름

            fileRef.PutFileAsync(filePath[i]).ContinueWith((Task<StorageMetadata> task) =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                }
                else
                {
                    Firebase.Storage.StorageMetadata metadata = task.Result;
                    Debug.Log(task.Result.ToString());
                    Debug.Log("[Server-Storage] JsonFile Uploaded: " + "GiveLetter/" + letterKey + "/" + folderName + "/" + fileName);

                }
            });
        }
    }

}
