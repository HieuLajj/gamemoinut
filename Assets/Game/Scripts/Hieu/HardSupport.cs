using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using Spine.Unity;
using Sirenix.OdinInspector;
using static DG.DemiLib.DeToggleColors;


public class HardSupport : MonoBehaviour
{

    private static HardSupport instance;
    public static HardSupport Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HardSupport>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private Coroutine hardSupportCoroutine;
    public GameObject CanvastHardSupport;
    public GameObject CanvasHardSupportMain;
    public Image imageTime;
    public Image imageThunder; 
    private int timescale = 0;
    private int thunderscale = 0;

    public bool BoolThunder;
    public bool BoolTime;
    public Image imageBackground;
    private UnityEngine.Color colorimageBackground;
    public SkeletonGraphic skeletonAnimationAlert;
    private RectTransform rectCanvasHardSupportMain;
    private readonly string playerpref_checkfirsthard= "playerpref_checkfirsthard";
    public bool Checkfirst;
    public WarningImage WarningImage;

    private void Start()
    {
       
        colorimageBackground = imageBackground.color; 
        float scaleSkeleton = (1.053f * Screen.width) / 1080;
        skeletonAnimationAlert.transform.localScale = new Vector3(scaleSkeleton, scaleSkeleton, 1);
        rectCanvasHardSupportMain = CanvasHardSupportMain.GetComponent<RectTransform>();
        Checkfirst = PlayerPrefs.GetInt(playerpref_checkfirsthard) ==1?false:true;
    }

    [Button()]
    public void MoveIn()
    {
        skeletonAnimationAlert.gameObject.SetActive(true);
        skeletonAnimationAlert.AnimationState.SetAnimation(0, "move in", false).Complete+=AnimationComplete;
        Invoke("Setup2", 2f);
    }

    [Button()]
    public void MoveOut()
    {       
        skeletonAnimationAlert.AnimationState.SetAnimation(0, "move out", false).Complete += falseAnimationComplete;
    }


    void AnimationComplete(Spine.TrackEntry trackEntry)
    {    
        skeletonAnimationAlert.AnimationState.SetAnimation(0, "idle", true);
    }

    void falseAnimationComplete(Spine.TrackEntry trackEntry)
    {
       
        skeletonAnimationAlert.gameObject.SetActive(false);     
    }
   
    [Button()]
    public void Setup()
    {
       
        rectCanvasHardSupportMain.anchoredPosition = new Vector2(12.2628f, -1650f);
        colorimageBackground.a = 0.195f;
        imageBackground.color = colorimageBackground;
        colorimageBackground.a = 0.785f;
        imageBackground.DOColor(colorimageBackground, 0.75f);
        BoolThunder = true;
        BoolTime = true;
        Timer.instance.Pause();      
        timescale = 0;
        thunderscale = 0;
        imageTime.gameObject.SetActive(false);
        imageThunder.gameObject.SetActive(false);
        CanvastHardSupport.SetActive(true);
        CanvasHardSupportMain.SetActive(false);
        // SoundManager.Instance.StopMusic();
        // SoundManager.Instance.PlayMusic(AudioClipType.ALERT_BOMB);
        MoveIn();
        Invoke("MoveOut", 1.75f);
    }

   

    private void Setup2()
    {
        // SoundManager.Instance.StopMusic();
        // SoundManager.Instance.PlayMusic(AudioClipType.SFX_BGM5);
        rectCanvasHardSupportMain.DOAnchorPos(new Vector2(12.2628f, -43f), 0.8f).SetEase(Ease.OutBack);

        if (LevelController.Instance.LevelIDInt < 5)
        {
            StartCoroutine(IEOnDisableActive());
        }
        else
        {
            if (GameMonitor.Instance.flagDifficultSupport >= 5)
            {
                SetIncreaseTime();
                SetThunder();
            }
            else if (GameMonitor.Instance.flagDifficultSupport == 1 || Checkfirst)
            {
                SetIncreaseTime();
                if (Checkfirst)
                {
                    PlayerPrefs.SetInt(playerpref_checkfirsthard, 1);
                    Checkfirst = false;
                }
            }
            hardSupportCoroutine = StartCoroutine(DisActiveSupport());
        }
    }

    private IEnumerator IEOnDisableActive()
    {
        if(timescale <= 0)
        {
           Timer.instance.Resume();
        }
        yield return new WaitForSeconds(0.3f);
        if (timescale > 0)
        {
            Timer.instance.IncreaseTime();
        }
        if (thunderscale > 0)
        {
            if (timescale > 0)
            {
                Invoke("CallThunder", 1.75f);
            }
            else
            {
                CallThunder();
            }
        }
        colorimageBackground.a = 0;
        WarningImage.StopWarning();
        imageBackground.DOColor(colorimageBackground, 0.25f).OnComplete(DisActiveCanvas);
    }

    private void DisActiveCanvas(){
        CanvastHardSupport.SetActive(false);
    }

    private IEnumerator DisActiveSupport()
    {
        //yield return new WaitForSeconds(1f);
        CanvasHardSupportMain.SetActive(true);
        yield return new WaitForSeconds(4f);
        CanvastHardSupport.SetActive(false);
        CanvasHardSupportMain.SetActive(false);
        OnDisableActive();
        hardSupportCoroutine = null;
    }

    private void OnDisableActive()
    {
        if (timescale > 0)
        {
            Timer.instance.IncreaseTime();
        }
        else
        {
            Timer.instance.Resume();
        }
        if (thunderscale > 0)
        {
           if(timescale > 0)
           {
                Invoke("CallThunder", 1.75f);
            }
            else
            {
                Invoke("CallThunder", 1);            
            }
        }
    }
    private void CallThunder()
    {
        LightController.Instance.ActiveGlobalVolumn();
        StartCoroutine(ThunderActive());
    }

    public int GetTimeScale()
    {
        if(timescale >= 3)
        {
            return 39;
        }
        if(timescale >= 2)
        {
            return 29;
        }
        if(timescale >= 1)
        {
            return 9;
        }
        return 0;
    }

    private void SetIncreaseTime()
    {
        imageTime.gameObject.SetActive(true);

        timescale++;
    }
    private void SetThunder()
    {
        imageThunder.gameObject.SetActive(true);
        thunderscale++;
    }

    private IEnumerator ThunderActive() {

        Board_Item board1 = null;
        for (int i = 0; i < thunderscale; i++)
        {
            Board_Item board_Item;
            do
            {
                int randomIndex = Random.Range(0, ControllerHieu.Instance.rootlevel.listboard.Count);
                board_Item = ControllerHieu.Instance.rootlevel.listboard[randomIndex];
            } while (board_Item == board1);

            board1 = board_Item;
            board1.ThunderDestroy(i);
            yield return new WaitForSeconds(0.75f);
        }
    }

    public void AddTime()
    {
        if (BoolTime == false) return;
        // if (ItemManager.GetItemValue(ItemName.TICKET_SILVER) < 1)
        // {
        //     StopCoroutine(hardSupportCoroutine);
        //     gameObject.SetActive(false);
        //     MagicTicket.DisableAction += () =>
        //     {
        //         gameObject.SetActive(true);
        //     };
        //     MagicTicket.checkoption = 2;
        //     UIEvents.Instance.ShowMagicTicket();
        //     return;
        // }
        //ItemManager.RaiseItem(ItemName.TICKET_SILVER, -1);
        timescale+=2;
        BoolTime = false;
    }

    public void AddThunder()
    {
        if (BoolThunder == false) return;
        // if (ItemManager.GetItemValue(ItemName.TICKET_SILVER) < 1)
        // {
        //     StopCoroutine(hardSupportCoroutine);
        //     gameObject.SetActive(false);
        //     MagicTicket.DisableAction += () =>
        //     {
        //         gameObject.SetActive(true);
        //     };
        //     MagicTicket.checkoption = 2;
        //     UIEvents.Instance.ShowMagicTicket();
        //     return;
        // }
        // ItemManager.RaiseItem(ItemName.TICKET_SILVER, -1);
        BoolThunder = false;        
        thunderscale++;
    }

    public void Back()
    {
        CanvastHardSupport.SetActive(false);
        StopCoroutine(hardSupportCoroutine);
        OnDisableActive();
    }
}
