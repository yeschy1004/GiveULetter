using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIEventHandler : MonoBehaviour
{
    public static UIEventHandler eventHandler;

    public GameObject mainPanel;
    public GameObject mailboxPanel;
    public GameObject exitPanel;
    public GameObject letterPanel;
    public GameObject sendPanel;
    public GameObject sendCheckingPanel;

    public GameObject downloadSuccessPanel;
    public Text downloadStateTxt;
    public Text inputCodeTxt;


    public GameObject mailbox_LetterScrollview;
    public GameObject mailbox_LetterEmptyObj;

    public Text mailbox_letterTime;
    public Text mailbox_letterDate;

    public string curDate;
    public string curTime;
    public static bool isExist_letter=false;

    public InputField sendPanel_SenderNameInputField;
    public InputField sendPanel_ReceiverNameInputField;
    public Text sendPanel_letterTitleTxt;


    enum PageState {Main, Mailbox, Exit, Kakao, Letter, Send, SendCheck};

    PageState pageState = PageState.Main;

    // Start is called before the first frame update


    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (SBConverter.converter != null)
            {
                Debug.Log("jump");
                JumpToSendPanel();
            }
            else
            {
                pageState = PageState.Main;
                Onclick_turnOnMainPanel();

                downloadSuccessPanel.SetActive(false);
            }
            
        }

    }


    //서버에서 있는 키값인지 확인해야 합니다.
    //서버에서 확인한 뒤에 해당 값을 저장하는 일을 해야할 듯
    public void Onclick_turnOnDownloadSuccessPanel()
    {

        if (inputCodeTxt.text.Equals("-MS-PYVNOxHLPJMzVSsL"))
        {
            downloadStateTxt.text = "편지를 성공적으로 내려받았습니다.";
            isExist_letter = true;
        }
        else
        {
            downloadStateTxt.text = "없는 편지 번호입니다.";
        }
        downloadSuccessPanel.SetActive(true);

    }


    public void Onclick_turnOffDownloadFailPanel()
    {
        
        downloadSuccessPanel.SetActive(false);

    }

    #region 씬 전환 관련
    public void GoToScene(int i)
    {
        SceneManager.LoadScene(i);
        if(i > 0)
        {
            pageState = PageState.Letter;
            Set_ScreenOrientation(pageState);
        }
    }
    #endregion

    #region Onclick 패널
    //Panel 관리
    public void Onclick_turnOnMainPanel()
    {
        Debug.Log("Turn on Main panel");
        pageState = PageState.Main;
        Set_ScreenOrientation(pageState);

        mainPanel.SetActive(true);
        mailboxPanel.SetActive(false);
        letterPanel.SetActive(false);
        exitPanel.SetActive(false);
        sendPanel.SetActive(false);
        sendCheckingPanel.SetActive(false);

    }

    public void Onclick_turnOnLetterPanel()
    {

        Debug.Log("Onclick_turnOnLetterPanel");
        pageState = PageState.Letter;
        Set_ScreenOrientation(pageState);

        mainPanel.SetActive(false);
        mailboxPanel.SetActive(false);
        letterPanel.SetActive(true);
        exitPanel.SetActive(false);
        sendPanel.SetActive(false);
        sendCheckingPanel.SetActive(false);
    }

    

    public void Onclick_turnOnMailboxPanel()
    {
        Debug.Log("Onclick_turnOnMailboxPanel");
        pageState = PageState.Mailbox;
        Set_ScreenOrientation(pageState);

        mainPanel.SetActive(false);
        mailboxPanel.SetActive(true);
        letterPanel.SetActive(false);
        exitPanel.SetActive(false);
        sendPanel.SetActive(false);
        sendCheckingPanel.SetActive(false);

        if (isExist_letter)
        {
            mailbox_LetterEmptyObj.SetActive(false);
            mailbox_LetterScrollview.SetActive(true);
        }
        else
        {
            mailbox_LetterEmptyObj.SetActive(true);
            mailbox_LetterScrollview.SetActive(false);
        }

    }

    public void Onclick_turnOnSendPanel()
    {
        Debug.Log("Onclick_turnOnSendPanel");
        pageState = PageState.Send;
        Set_ScreenOrientation(pageState);


        InicializeSendeReceiverNameTxt();
        sendPanel.SetActive(true);
    }



    //편지보기(어플 재시작)
    public void Onclick_ReOpenLetter()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);
            intent.Call<AndroidJavaObject>("setFlags", 0x20000000);//Intent.FLAG_ACTIVITY_SINGLE_TOP

            AndroidJavaClass pendingIntent = new AndroidJavaClass("android.app.PendingIntent");
            AndroidJavaObject contentIntent = pendingIntent.CallStatic<AndroidJavaObject>("getActivity", currentActivity, 0, intent, 0x8000000); //PendingIntent.FLAG_UPDATE_CURRENT = 134217728 [0x8000000]
            AndroidJavaObject alarmManager = currentActivity.Call<AndroidJavaObject>("getSystemService", "alarm");
            AndroidJavaClass system = new AndroidJavaClass("java.lang.System");
            long currentTime = system.CallStatic<long>("currentTimeMillis");
            alarmManager.Call("set", 1, currentTime + 1000, contentIntent); // android.app.AlarmManager.RTC = 1 [0x1]

            Debug.LogError("alarm_manager set time " + currentTime + 1000);
            currentActivity.Call("finish");

            AndroidJavaClass process = new AndroidJavaClass("android.os.Process");
            int pid = process.CallStatic<int>("myPid");
            process.CallStatic("killProcess", pid);
        }
    }

    public void Onclick_turnOnSendCheckPanel()
    {
        pageState = PageState.SendCheck;
        Set_ScreenOrientation(pageState);

        ChangeSenderReceiverName();


        sendCheckingPanel.SetActive(true);
    }


    public void Onclick_turnOnExitPanel()
    {
        Debug.Log("Turn on Exit panel");
        pageState = PageState.Exit;
        Set_ScreenOrientation(pageState);

        exitPanel.SetActive(true);
    }


    public void Onclick_turnOffSendPanel()
    {
        pageState = PageState.Letter;
        Set_ScreenOrientation(pageState);

        sendPanel.SetActive(false);
    }

    public void Onclick_turnOffSendCheckPanel()
    {
        pageState = PageState.Send;
        Set_ScreenOrientation(pageState);


        sendCheckingPanel.SetActive(false);
    }


    public void Onclick_turnOffExitPanel()
    {
        pageState = PageState.Main;

        exitPanel.SetActive(false);
    }
    #endregion

    public void Onclick_codeImportBtn()
    {
        
        DebugLog("Onclick", "Json 파일 다운로드 버튼");

        curTime = System.DateTime.Now.ToString("HH:mm");
        curDate = System.DateTime.Now.ToString("yyyy.MM.dd");

        mailbox_letterDate.text = curDate;
        mailbox_letterTime.text = curTime;

        Onclick_turnOnDownloadSuccessPanel();


        //서버 연동은 하지 않음
        //   GameObject.Find("SystemObject").GetComponent<ImportLetter>().Get_LetterJsonInfo();
    }


    #region 종료, 백버튼 관련
    //Main, Mailbox, Letter, Exit, Send, SendCheck
    void Onclick_BackBtn()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
               if(pageState == PageState.Main)
                {
                    Onclick_turnOnExitPanel();
                }
                else if(pageState == PageState.Mailbox)
                {
                    Onclick_turnOnMainPanel();
                }
               else if(pageState == PageState.Letter)
                {
                    //아무일도 일어나지 않음
                }
                else if (pageState == PageState.Exit)
                {
                    Onclick_turnOnMainPanel();
                }
                else if (pageState == PageState.Send)
                {
                    Onclick_turnOffSendPanel();
                }
                else if (pageState == PageState.SendCheck)
                {
                    //아무일도 일어나지 않음(전송 완료됨)
                }
            }
        }
    }

    public void Onclick_ExitPanel_Yes()
    {
        //프로그램 종료
        Application.Quit();
    }

    //앱 재실행
   

    #endregion

    public void JumpToSendPanel()
    {
        Onclick_turnOnSendPanel();
    }


    public void Onclick_Mainpanel_CompanyWebBrowseBtn()
    {
        GameObject.Find("SystemObject").GetComponent<BrowseCompanyWebSite>().BrowseCompanyWebsite();

    }

    public void Onclick_SharingKakaotalk()
    {
        //서버로 편지 전송하는 코드, 카카오톡 보낸거 확인해야 하는데 지금은 그냥 보냄
        GameObject.Find("SystemObject").GetComponent<ExportLetter>().UploadLetterToServer();

        DebugLog("Onclick", "카카오톡 보내기 버튼");
    }


    #region 화면 회전, 비율 설정
    void Set_ScreenOrientation(PageState ps)
    {
        //enum PageState {Main, Mailbox, Exit, Kakao, Letter, Send, SendCheck}; 

        if ((int)ps <= 3)
            Screen.orientation = ScreenOrientation.Portrait;
        else
            Screen.orientation = ScreenOrientation.Landscape;
    }


    void InicializeSendeReceiverNameTxt()
    {
        /*
        GameObject.Find("SystemObject").GetComponent<ExportLetter>().Set_SenderName(FromToGetSetSystem.GetFrom());
        GameObject.Find("SystemObject").GetComponent<ExportLetter>().Set_ReceiverName(FromToGetSetSystem.GetTo());
        */

        Debug.Log(FromToGetSetSystem.GetFrom());
        Debug.Log(FromToGetSetSystem.GetTo());
        sendPanel_SenderNameInputField.text = FromToGetSetSystem.GetFrom();
        sendPanel_ReceiverNameInputField.text = FromToGetSetSystem.GetTo();

        sendPanel_letterTitleTxt.text = "To " + FromToGetSetSystem.GetTo() + ", From " + FromToGetSetSystem.GetFrom();

        //sendPanel_letterTitleTxt.text = "To " + GameObject.Find("SystemObject").GetComponent<ExportLetter>().Get_ReceiverName() + ", From " + GameObject.Find("SystemObject").GetComponent<ExportLetter>().Get_SenderName();

        Debug.Log("sender Name: " + sendPanel_SenderNameInputField.text);
    }
    #endregion



    //씬 재생(삭제해도 됨)
    public void Onclick_PlayLetter()
    {
        //특정한 해당 콘텐츠 재생

        DebugLog("Onclick", "콘텐츠 재생");
    }


    void ChangeSenderReceiverName()
    {
        GameObject.Find("SystemObject").GetComponent<ExportLetter>().Set_SenderName(sendPanel_SenderNameInputField.text);
        GameObject.Find("SystemObject").GetComponent<ExportLetter>().Set_ReceiverName(sendPanel_ReceiverNameInputField.text);
    }

    void ResolutionFix(){
        // 가로 세로 비율
        float targetWidthAspect = 16.0f;
        float targetHeightAspect = 9.0f;

        Camera.main.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHeightAspect;

        float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
        float widthadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

        // 시작지점을 0으로 만듬
        if (heightRatio > widthRatio)
            widthRatio = 0.0f;
        else
            heightRatio = 0.0f;

        Camera.main.rect = new Rect(
            Camera.main.rect.x + Mathf.Abs(widthadd),
            Camera.main.rect.x + Mathf.Abs(heightadd),
            Camera.main.rect.width + (widthadd * 2),
            Camera.main.rect.height + (heightadd * 2));
    }



    // Update is called once per frame
    void Update()
    {
       // ResolutionFix();
        Onclick_BackBtn();

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
    }


    void DebugLog(string systemName, string discription)
    {
        Debug.Log("[" + systemName + "] " + discription);
    }
}
