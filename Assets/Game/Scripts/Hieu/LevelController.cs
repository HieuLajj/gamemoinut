using Sirenix.OdinInspector;

using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class LevelController : MonoBehaviour
{
    public static Action EventStartGame;
    public static Action EventEndGame;
    public static Action EventCompleteGame;
    public List<IUIEffectItem> ListUIEffectControll = new List<IUIEffectItem>();
    public delegate void CheckActionUser();
    public event CheckActionUser checkActionUser;

    public ParticleSystem paritcleSystemWin;
    private static LevelController instance;
    public static LevelController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelController>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public Transform MainLevelSetupCreateMap;
    private Coroutine checkLose;
    public int NumberBoardDestroy;

    public bool LevelDifficule;
    public int totalWood;

    public int lee;
    public int LevelIDInt
    {
        get
        {          
            return 1;
            // if (GameManager.dataSave.level <= 0)
            // {
            //     return 1;
            // }
            //return GameManager.dataSave.level;
            //return 180;
            //return 178;
        }
        set
        {
            //GameManager.dataSave.level = value;
            //GameManager.SaveData();
           
           // CanvasManagerGamePlay.Instance.textLevel.textLevel.text = "Level " + (GameManager.dataSave.level);
        }
    }

    

    private void Awake()
    {
        //levelInt = 1;
        //PlayerPrefs.GetInt("Playinglevel");
        // SoundManager.Instance.StopMusic();
        // SoundManager.Instance.PlayMusic(AudioClipType.SFX_BGM5);
        //CanvasManagerGamePlay.Instance.textLevel.textLevel.text = "Level " + (Mathf.Clamp(GameManager.dataSave.level,1,1000));
      //  SuSceneManager.OnBeginLoadScene += BeginChangeScene;
        
    }

    // private void OnEnable()
    // {
    //     PopupManager.OnPopupShow += PopupShow;
    //     PopupManager.OnPopupClose += PopupClose;
    // }

    // private void OnDisable()
    // {
    //     PopupManager.OnPopupShow -= PopupShow;
    //     PopupManager.OnPopupClose -= PopupClose;
    // }
    // private void PopupShow(PopupName popupName)
    // {
    //     if (popupName == PopupName.POPUP_RATE)
    //     {
    //         Timer.instance.Pause();
    //     }
    // }

    // private void PopupClose(PopupName popupName)
    // {
    //     if (popupName == PopupName.POPUP_RATE)
    //     {
    //         Timer.instance.Resume();
    //     }
    // }
    private void Start()
    {
      //  Timer.instance.EndTime = EndTime;
        // POPUP_SETTING.OnClickReplayAction = () =>
        // {
        //     ReplayLevel();
        // };
    }
    private void EndTime()
    {
        // CheckFailureDetail(() =>
        // {
        //     GameMonitor.Instance.StopMonitor();
        //     DefaultUI.typeDefault = 0;
        //     UIEvents.Instance.ShowDefaultUI();
        //     DailyQuestManager.ResetDailyQuestValue(DailyQuestType.WIN_X_GAMES_IN_A_ROW);
        //     GameMonitor.Instance.flagDifficultSupport = -1;
        // });
    }

    // private void BeginChangeScene(SceneName sceneName)
    // {
    //     GameMonitor.Instance.StopMonitor();
    //     ControllerHieu.Instance.nailLayerController.ClearLayer();
    //     ControllerHieu.Instance.rootlevel?.ClearRoot(() => { });
    //     SuSceneManager.OnBeginLoadScene -= BeginChangeScene;
    // }
   
    public void StartGame()
    {
        StartLevel_LoadLevel();
       //CanvasManagerGamePlay.Instance.IngameUI.gameObject.SetActive(true);
      //  ControllerHieu.Instance.background_ui.Notdacbiet();
    }


    public void CleanMap()
    {
        ControllPlayGame.Instance.targetNail = null;
        for(int i=0; i< ListUIEffectControll.Count; i++)
        {
            ListUIEffectControll[i].ResetPool();
        }
        ListUIEffectControll.Clear();
    }

    public void ResetLevel()
    {   
        CleanMap();
        ControllerHieu.Instance.nailLayerController.ClearLayer();
        ControllerHieu.Instance.rootlevel?.ClearRoot(() => {
        StartLevel_LoadLevel();
        }); 
    }
    public void ReplayLevel()
    {
        LevelDifficule = false;
        CleanMap();
        ControllerHieu.Instance.nailLayerController.ClearLayer();
        ControllerHieu.Instance.rootlevel?.ClearRoot(() => {
            StartLevel_LoadLevel();
        });
    }

    [Button()]
    public void NextLevel()
    {
        CleanMap();    
        ControllerHieu.Instance.nailLayerController.ClearLayer();
        ControllerHieu.Instance.rootlevel.ClearRoot(() =>
        {      
            StartLevel_LoadLevel();
        });
    }

    public void StartLevel_LoadLevel()
    {
        EventStartGame?.Invoke();
        LoadDataBase.Instance.LoadLevelGame(LevelIDInt);
    }


    public void ActiveActionUser()
    {
        checkActionUser?.Invoke();
    }
    public void CheckAfterKillBoard()
    {    
        if (ControllerHieu.Instance.rootlevel.listboard.Count == NumberBoardDestroy)
        {
            if (checkLose != null)
            {
                StopCoroutine(checkLose);
            }     
            EventEndGame?.Invoke();
            GameMonitor.Instance.StopMonitor();
            if (!LevelDifficule)
            {
                //SoundManager.Instance.PlaySound(AudioClipType.SFX_GAME_WIN);
                CanvasManagerGamePlay.Instance.EffectBlur.ActionAwaitEffect = StartDifficlueLevel;
                UIEvents.Instance.ShowEffectBlur();           
                return;
            }

            LevelIDInt++;
            LevelDifficule = false;
           
            if (MagicTicket.checkactive == 2)
            {
                MagicTicket.DisableAction += () =>
                {
                    //SoundManager.Instance.PlaySound(AudioClipType.SFX_GAME_WIN);
                    CanvasManagerGamePlay.Instance.EffectBlur.ActionAwaitEffect = AffterPlayerComplete;
                    UIEvents.Instance.ShowEffectBlur();
                };
            }
            else {
                //SoundManager.Instance.PlaySound(AudioClipType.SFX_GAME_WIN);
                CanvasManagerGamePlay.Instance.EffectBlur.ActionAwaitEffect = AffterPlayerComplete;
                UIEvents.Instance.ShowEffectBlur();
            }        
        }
    }
    private void StartDifficlueLevel()
    {
        LevelDifficule = true;
        CleanMap();
        ControllerHieu.Instance.nailLayerController.ClearLayer();
        ControllerHieu.Instance.rootlevel?.ClearRoot(() =>
        {
            StartLevel_LoadLevel();
        });
    }


    private void AffterPlayerComplete()
    {    
        EventCompleteGame?.Invoke();
        Timer.instance.Pause();
        // SuAnalytics.LogEventLevelEnd(LevelIDInt, "", true);
        // DailyQuestManager.RaiseDailyQuestValue(DailyQuestType.WIN_X_GAMES, 1);
        // DailyQuestManager.RaiseDailyQuestValue(DailyQuestType.WIN_X_GAMES_IN_A_ROW, 1);
        // ItemManager.RaiseItem(ItemName.LUCKY_SPIN_PROCESS, 1);
     //   SuGame.Get<SuAds>().ShowInterstitial(() =>
      //  {
            UIEvents.Instance.ShowWinUI();
            if (LevelIDInt >= 5)
            {
                WinUI.checkActiveOther = true;
                Invoke("ShowMasterPass", 1f);
            }
            else
            {
                WinUI.checkActiveOther = false;
            }
        //}, UltilitiesHieu.ActionShowAdsString("complete"));
     
    }
    private void ShowMasterPass()
    {
        UIEvents.Instance.ShowMasterPass();
    }


    public void CheckFailureDetail(Action loseAction)
    {
        foreach (Board_Item board_Item in ControllerHieu.Instance.rootlevel.listboard)
        {
            if (!board_Item.checkDestroy)
            {
                foreach (Slot_board_Item slot_Board_Item in board_Item.listslot)
                {
                    if (slot_Board_Item.hingeJointInSlot.enabled)
                    {
                        loseAction?.Invoke();                  
                    }
                }
                if (checkLose != null)
                {
                    checkLose = StartCoroutine(CheckLose2(loseAction));
                }
            }
        }     
    }
    IEnumerator CheckLose2(Action loseAction)
    {
        yield return new WaitForSeconds(3);
        CheckFailureDetail2(loseAction);
    }
    private void CheckFailureDetail2(Action loseAction) {

        loseAction?.Invoke();
    }
}