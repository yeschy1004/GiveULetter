using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadSystem : MonoBehaviour
{
    public static bool contentIsPlaying = false;
    public static int numberOfSBs = 10;
    public bool isTemp;
    public Texture2D []tempTextures;
    public AudioClip []tempAudios;
    public bool isTest;
    public string folderPath;

    [SerializeField] AudioClip[] guideSounds;
    [SerializeField] AudioClip[] activatedSounds;
    [SerializeField] int themeNumber;
    [SerializeField] Transform sbParent;
    [SerializeField] GameObject tapPanel;
    [SerializeField] GameObject endPanel;

    int maxnumberOfSB = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (!isTemp)
        {
            if (isTest && SBConverter.converter != null)
            {
                LoadTest();
            }
            else if (!isTest)
            {
                LoadLetter();
            }
        }
        else
        {
            SetSB(sbParent.GetChild(0).GetComponent<SoundBalloon>(), tempAudios[0], sbParent.GetChild(1).gameObject, tempTextures[0], new Vector3(0, 0, 2), 1);
            SetSB(sbParent.GetChild(1).GetComponent<SoundBalloon>(), tempAudios[1], endPanel, tempTextures[1], new Vector3(-1.6714316606521607f, 0.3445935845375061f, -1.0428674221038819f), 2);
            FromToGetSetSystem.SetFrom("창업");
            FromToGetSetSystem.SetTo("예비");
            numberOfSBs = 2;
        }
        contentIsPlaying = true;
        sbParent.GetChild(0).gameObject.SetActive(true);
        PlayPageUIController.mainUI.StartContent();
    }

    void LoadLetter()
    {
        string json = File.ReadAllText(folderPath);
        LetterSaveObject letter = JsonUtility.FromJson<LetterSaveObject>(json);
        numberOfSBs = letter.sbs.Length;

        bool isSBAmountMax = numberOfSBs >= maxnumberOfSB;

        for (int i = 0; i < numberOfSBs; i++)
        {
            SoundBalloon sb = sbParent.GetChild(i).GetComponent<SoundBalloon>();
            SBSaveObject sbObject = letter.sbs[i];
            GameObject nextObject;

            if (i + 1 == numberOfSBs)
            {
                nextObject = endPanel;
            }
            else
            {
                nextObject = sbParent.GetChild(i + 1).gameObject;
            }
            //TODO: 오디오, 사진 받아오기 -> 하연이 코드
            // SetSB(sb, sbEdit.GetRecording().audio, nextObject, sbEdit.GetPicture().texture, sbObject.position, sbObject.orderNumber);
        }

        for (int i = numberOfSBs; i < maxnumberOfSB; i++)
        {
            sbParent.GetChild(i).gameObject.SetActive(false);
        }        
    }
    
    void LoadTest()
    {
        bool isSBAmountMax;
        Transform converter = SBConverter.converter.transform;

        numberOfSBs = converter.childCount;
        isSBAmountMax = numberOfSBs >= maxnumberOfSB;

        for(int i = 0; i<converter.childCount; i++)
        {
            SoundBalloon sb = sbParent.GetChild(i).GetComponent<SoundBalloon>();
            SBEditingObject sbEdit = converter.GetChild(i).GetComponent<SBEditingObject>();
            GameObject nextObject;

            if(i + 1 == converter.childCount)
            {
                nextObject = endPanel;
            }
            else
            {
                nextObject = sbParent.GetChild(i + 1).gameObject;
            }
            SetSB(sb, sbEdit.GetRecording().audio, nextObject, sbEdit.GetPicture().texture, sbEdit.transform.position, i + 1);

            
            
        }

        for(int i = numberOfSBs; i <maxnumberOfSB; i++)
        {
            sbParent.GetChild(i).gameObject.SetActive(false);
        }

        converter.GetComponent<SBConverter>().TurnOffChildren();
    }

    void SetSB(SoundBalloon sb, AudioClip clip, GameObject nextObject, Texture2D picture, Vector3 position, int orderNumber)
    {
        sb.SetPicture(picture);
        sb.SetPos(position);
        sb.orderNumber = orderNumber;

        SBActionManager action = sb.GetComponent<SBActionManager>();

        action.SetAction(SBTriggerFilterer.TriggerTypes.Initial, SBActionManager.ActionTypes.GrayScaleAction,
            new SBGrayScaleAction(sb, false));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Initial, SBActionManager.ActionTypes.SoundAction,
            new SBSoundAction(sb, guideSounds[themeNumber], true, true));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Initial, SBActionManager.ActionTypes.PulseAction,
            new SBPulseAction(sb, true));

        action.SetAction(SBTriggerFilterer.TriggerTypes.Look, SBActionManager.ActionTypes.GrayScaleAction,
            new SBGrayScaleAction(sb, true));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Look, SBActionManager.ActionTypes.SoundAction,
            new SBSoundAction(sb, activatedSounds[themeNumber], true, true));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Look, SBActionManager.ActionTypes.PulseAction,
            new SBPulseAction(sb, false));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Look, SBActionManager.ActionTypes.ProgressBarAction,
            new SBProgressAction(sb, true));

        action.SetAction(SBTriggerFilterer.TriggerTypes.NotLooking, SBActionManager.ActionTypes.SoundAction,
            new SBSoundAction(sb, null));
        action.SetAction(SBTriggerFilterer.TriggerTypes.NotLooking, SBActionManager.ActionTypes.ProgressBarAction,
            new SBProgressAction(sb, false));


        action.SetAction(SBTriggerFilterer.TriggerTypes.Tap, SBActionManager.ActionTypes.PulseAction,
            new SBPulseAction(sb, false));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Tap, SBActionManager.ActionTypes.PanelAction,
           new SBTapPanelAction(sb, clip, sb.GetPicture()));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Tap, SBActionManager.ActionTypes.ProgressBarAction,
     new SBProgressAction(sb, false));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Tap, SBActionManager.ActionTypes.SoundAction,
     new SBSoundAction(sb, null));

        action.SetAction(SBTriggerFilterer.TriggerTypes.PanelFinished, SBActionManager.ActionTypes.SwitchAction,
                new SBSwitchAction(nextObject, true));

        sb.gameObject.SetActive(false);
    }
    /*
    void InitializeSB(SoundBalloon sb, GameObject nextObject)
    {
        SBActionManager action = sb.GetComponent<SBActionManager>();

        sb.orderNumber = orderNumber++;
        action.SetAction(SBTriggerFilterer.TriggerTypes.Initial, SBActionManager.ActionTypes.GrayScaleAction,
            new SBGrayScaleAction(sb, false));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Initial, SBActionManager.ActionTypes.SoundAction,
            new SBSoundAction(sb, guideSounds[themeNumber], true, true));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Initial, SBActionManager.ActionTypes.PulseAction,
            new SBPulseAction(sb, true));
        
        action.SetAction(SBTriggerFilterer.TriggerTypes.Look, SBActionManager.ActionTypes.GrayScaleAction,
            new SBGrayScaleAction(sb, true));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Look, SBActionManager.ActionTypes.SoundAction,
            new SBSoundAction(sb, activatedSounds[themeNumber], true, true));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Look, SBActionManager.ActionTypes.PulseAction,
            new SBPulseAction(sb, false));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Look, SBActionManager.ActionTypes.ProgressBarAction,
            new SBProgressAction(sb, true));
        
        action.SetAction(SBTriggerFilterer.TriggerTypes.NotLooking, SBActionManager.ActionTypes.SoundAction,
            new SBSoundAction(sb, null));
        action.SetAction(SBTriggerFilterer.TriggerTypes.NotLooking, SBActionManager.ActionTypes.ProgressBarAction,
            new SBProgressAction(sb, false));

        action.SetAction(SBTriggerFilterer.TriggerTypes.Tap, SBActionManager.ActionTypes.PanelAction,
           new SBTapPanelAction(sb,testAudio1, sb.GetPicture()));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Tap, SBActionManager.ActionTypes.ProgressBarAction,
     new SBProgressAction(sb, false));
        action.SetAction(SBTriggerFilterer.TriggerTypes.Tap, SBActionManager.ActionTypes.SoundAction,
     new SBSoundAction(sb, null));

        action.SetAction(SBTriggerFilterer.TriggerTypes.PanelFinished, SBActionManager.ActionTypes.SwitchAction,
            new SBSwitchAction(nextObject, true));

        sb.gameObject.SetActive(false);
    }*/
}
