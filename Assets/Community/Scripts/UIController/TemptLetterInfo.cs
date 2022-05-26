using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.UIElements;

public class TemptLetterInfo : MonoBehaviour
{

    public Text senderName;
    public Text receiverName;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

    public string Get_SenderNameTxt()
    {
        return senderName.text;
    }

    public string Get_ReceiverNameTxt()
    {
        return receiverName.text;
    }

    public void Onclick_PictureAudioInfoDownload()
    {
        DebugLog("Onclick", "이미지, 음악 다운로드 버튼");

        GameObject.Find("SystemObject").GetComponent<ImportLetter>().Get_LetterAudioPictureInfo();
    }

    
    
    //마지막에 보내기에서 눌러야 하는 것
    //중요합니다!!!!

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DebugLog(string systemName, string discription)
    {
        Debug.Log("[" + systemName + "] " + discription);
    }
}
