using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Sirenix.OdinInspector;
[System.Serializable]
public class ColorDataArray
{
    public ColorHieu[] colors;
}
[System.Serializable]
public struct BaseHole
{
    public Pos pos;

    public int id;

}
public class GamePlayChallenge : MonoBehaviour
{
    // public ItemRewardModule itemRewardModuleChallenge;
    public GameObject WinUI;
    public LevelHolderChallenge GamePlayMain;
    public Action EventsChallenge;
    public GameObject PrefabNailChallenge;
    private static GamePlayChallenge instance;
    public List<int> holeTypeIdsRandom;
    public TutorialChallenge tutorialChallenge;

    public NailChallenge TargetNailChallenge;
    public int NumberNotDestroy;
    public GameObject DefaultGameObject;
    public GameObject prefabsHole;
    
    public Camera mainCamera;
    private readonly string playerprefs_tutorialchallenge = "playerprefs_tutorialchallenge"; 
    public static GamePlayChallenge Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GamePlayChallenge>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    float screenHeight ; 
    float screenWidth ;
    private void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
       
        mainCamera.orthographicSize = (float)(screenHeight / screenWidth)*8.435f;
        // SoundManager.Instance.StopMusic();
        // SoundManager.Instance.PlayMusic(AudioClipType.SFX_BGM5);
        WinUI.SetActive(false);
      //  DailyQuestManager.RaiseDailyQuestValue(DailyQuestType.PLAY_DAILY_CHALLENGE, 1);
        StartGame();
        Timer.instance.EndTime = EndTime;
        // POPUP_SETTING.OnClickReplayAction = () =>
        // {          
        //     StartGame();
        // };
    }

    private void EndTime()
    {
        DefaultGameObject.SetActive(true);
    }
    [Button()]
    public void StartGame()
    {
        if (TargetNailChallenge != null)
        {
            TargetNailChallenge.ResetImage();
            TargetNailChallenge = null;
        }
       
        Timer.instance.Stop();
        if (GamePlayMain.transform.childCount == 0)
        {
             string titlelevel = $"Hieu\\LevelChallenge\\LevelHolderNew1";
           
            TextAsset textAsset = Resources.Load<TextAsset>(titlelevel);
            if (textAsset != null)
            {
               
                string jsonLevel = textAsset.text;
                string[] strings = jsonLevel.Split(new string[] { "%%" }, StringSplitOptions.RemoveEmptyEntries);

                ColorDataArray data = JsonUtility.FromJson<ColorDataArray>(strings[0]);
                Color[] colorArray = new Color[data.colors.Length];
                for (int i = 0; i < data.colors.Length; i++)
                {
                    colorArray[i] = new Color(data.colors[i].r, data.colors[i].g, data.colors[i].b, data.colors[i].a);
                }
                GamePlayMain.colorMain = colorArray;

                string[] stringsmain = strings[1].Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string texthole in stringsmain)
                {
                    BaseHole baseHole = JsonUtility.FromJson<BaseHole>(texthole);
                    GameObject g = Instantiate(prefabsHole, new Vector3(baseHole.pos.x, baseHole.pos.y, baseHole.pos.z), Quaternion.identity,GamePlayMain.transform);                 
                    ChallengeHole challengeHole = g.GetComponent<ChallengeHole>();
                    GamePlayMain.ChallengeHole.Add(challengeHole);
                   
                    Color colorhole = colorArray[baseHole.id];
                    challengeHole.currentColor = colorhole;
                    challengeHole.holeTypeId = baseHole.id;
                    challengeHole.holeSpr.color = colorhole;
                }
                for (int i = 0; i < GamePlayMain.ChallengeHole.Count; i++)
                {
                    GamePlayMain.ChallengeHole[i].checkholehidescrew = false;
                }

                int randomhole = UnityEngine.Random.Range(0, GamePlayMain.ChallengeHole.Count);

                ChallengeHole challengeHoles = GamePlayMain.ChallengeHole[randomhole];
                challengeHoles.checkholehidescrew = true;

            }
            else
            {
                Debug.LogError("Không tìm thấy file text có tên myTextFile trong thư mục Resources.");
            }
        }
        DefaultGameObject.SetActive(false);

        if (GamePlayMain.transform.childCount != 0)
        {      
            GamePlayMain.transform.parent = transform;
            NumberNotDestroy = GamePlayMain.ChallengeHole.Count - 1;

            ShuffleHoleTypeIds(GamePlayMain.ChallengeHole);

            for (int i = 0; i < GamePlayMain.ChallengeHole.Count; i++)
            {
                GamePlayMain.ChallengeHole[i].Setup(i);
            }
            Timer.instance.SetTextTime(300);
            Timer.instance.Run(301);
            if (PlayerPrefs.GetInt(playerprefs_tutorialchallenge)==0)
            {
                PlayerPrefs.SetInt(playerprefs_tutorialchallenge, 1);
                tutorialChallenge.gameObject.SetActive(true);
            }         
        }
        else
        {
            Debug.LogError("Không tìm thấy file text có tên myTextFile trong thư mục Resources.");
        }

    }



    void ShuffleHoleTypeIds(List<ChallengeHole> ChallengeHoles)
    {
        holeTypeIdsRandom = ChallengeHoles.Select(ch => ch.holeTypeId).ToList();
        System.Random rng = new System.Random();
        holeTypeIdsRandom = holeTypeIdsRandom.OrderBy(x => rng.Next()).ToList();
    }

    public void CheckWin()
    {
        if (NumberNotDestroy == 0)
        {
            WinUI.SetActive(true);
        }
    }
    public void ShowPopUpSetting()
    {
        //PopupManager.ShowPopup(PopupName.POPUP_SETTING);
    }
   
  
}
