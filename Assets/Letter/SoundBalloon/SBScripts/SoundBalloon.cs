using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBalloon : MonoBehaviour
{
    public int orderNumber;
    [SerializeField] int editorScene;
    [SerializeField] Vector3 playerPos = Vector3.zero;
    [SerializeField] Texture2D TestPic;

    Texture2D myPic;
    Material mat;
    SBTriggerFilterer trigger;
    SBActionManager action;
    // Start is called before the first frame update
    private void OnEnable()
    {
        mat = GetComponentInChildren<Renderer>().material;
        trigger = GetComponent<SBTriggerFilterer>();
        action = GetComponent<SBActionManager>();
        Setup();
        playerPos = GameObject.Find("Player").transform.position;
    }
    void Setup()
    {
        FacePlayer();
    }
    #region Setting Functions
    public void SetPicture(Texture2D text)
    {
        myPic = text;
        if (text.height < text.width)
        {
            transform.localScale = new Vector3(transform.localScale.x, text.height * transform.localScale.x / text.width, 1f);
        }
        else
        {
            transform.localScale = new Vector3(text.width * transform.localScale.y / text.height, transform.localScale.y, 1f);
        }
        mat.SetTexture("_MainTex", text);
    }
    public Texture2D GetPicture()
    {
        return myPic;
    }
    public void FacePlayer()
    {
        Vector3 lookPoint = transform.position - playerPos;
        lookPoint += transform.position;
        transform.LookAt(lookPoint);
    }
    #endregion

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
        FacePlayer();
    }
}
