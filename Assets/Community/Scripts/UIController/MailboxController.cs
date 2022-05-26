using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.IO;

public class MailboxController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //Save_LetterInfoTxt("제목", "키값", "보낸날짜", "받은날짜");
    }

    void Save_LetterInfoTxt(string letterTitle, string letterKey, string sendedDate, string receivedDate)
    {
        string saveFilePath = Application.persistentDataPath + "/savefiles/letterInfo.txt";

        string[] txtLines;
        string[] newLines = { "1", letterTitle, letterKey, sendedDate, receivedDate };
        int readNum = 0;

        string value;

        Debug.Log("텍스트 파일 함수 작동.");


        if (!File.Exists(saveFilePath))
        {
            var file = File.CreateText(saveFilePath);
            StreamWriter writer = new StreamWriter(@saveFilePath);
            writer.WriteLine("0\n");
            writer.Close();
            file.Close();
            Debug.Log("savefiles 폴더 내 텍스트 파일이 새로 생성되었습니다");
        }
        else
        {
            Debug.Log("savefiles 폴더 내 텍스트 있음");

            StreamWriter writer = new StreamWriter(@saveFilePath);
            StreamReader reader = new StreamReader(@saveFilePath);
            value = reader.ReadToEnd();
            readNum = Int32.Parse((value[0].ToString()));

            writer.Flush();

            writer.Close();
        }
    }
    
    void Delete_LetterInfoTxtFile()
    {

    }
        //+파일 폴더 자체를 삭제하는 함수
    void Delete_LetterInfoFolder()
    {

    }


    //텍스트 파일을 읽어서 리스트를 생성하는 함수
        /*
         * 텍스트파일 저장 형식:
         * 1) 파일 이름(발신/수신자 이름들어간거)
         * 2) 키값
         * 3) 보낸 날짜
         * 4) 받은 날짜
         */

    void Get_InternalFileList(){
        string InternalPath = Application.persistentDataPath + "/savefiles/";

        if (System.IO.Directory.Exists(InternalPath))
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(InternalPath);
            foreach (var item in di.GetFiles())
            {
                Debug.Log(item.Name);
            }
        }
        else
        {
            Debug.Log("저장된 항목이 없음");
        }
    }


    private void InitMailBox()
    {
        int yValue = 0;

        for(int i=0; i<10; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
