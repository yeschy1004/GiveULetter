using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPage_UI : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject mailboxPanel;
    public GameObject exitPanel;

    public GameObject downloadSuccessPanel;
    public Text downloadStateTxt;
    public Text inputCodeTxt;

    public GameObject mailbox_LetterScrollview;
    public GameObject mailbox_LetterEmptyObj;


    #region


    #endregion



    // Start is called before the first frame update
    void Start()
    {
        //초기화 시 MainPanel이 가장 위에
    }

    void InitializePanel()
    {
        mainPanel.SetActive(true);
        mailboxPanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
