using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultChallengeUI : MonoBehaviour
{
    public GamePlayChallenge gamePlayChallenge;
    public GameObject coinMain;
    private GameObject MagicTicketUIObject;
    public Canvas canvasMain;

    private void Start()
    {
        MagicTicket.DisableAction = null;
        if (MagicTicketUIObject == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Hieu\\UI\\Magictickets");

            if (prefab != null)
            {
                MagicTicketUIObject = Instantiate(prefab, Vector3.zero, Quaternion.identity,transform);
                RectTransform rectTransformMagicTicket = MagicTicketUIObject.GetComponent<RectTransform>();
                rectTransformMagicTicket.offsetMin = Vector2.zero; 
                rectTransformMagicTicket.offsetMax = Vector2.zero; 
            }
            else
            {
                Debug.LogError("Không thể tải Prefab từ thư mục Resources.");
            }
        }
        MagicTicketUIObject.SetActive(false);
    }

    private void OnEnable()
    {
       // SoundManager.Instance.PlaySound(AudioClipType.SFX_UI_POPUP);
        coinMain.SetActive(false);
    }
    public void CompletedAnimation()
    {
        coinMain.SetActive(true);
    }
    public void Replay()
    {
        gamePlayChallenge.StartGame();
    }
    public void ViewAds()
    {
        // SuGame.Get<SuAds>().ShowRewardVideo(AddTime, null, UltilitiesHieu.ActionShowAdsString("outofmove"));
        // SuAnalytics.LogEventLevelEnd(LevelController.Instance.LevelIDInt, "", false);
    }

    public void UseCoin()
    {
        // if (ItemManager.GetItemValue(ItemName.COIN) < 40)
        // {
        //     coinMain.SetActive(false);
        //     MagicTicket.DisableAction += ActiveCoinMain;
        //     MagicTicket.checkoption = 3;
        //     MagicTicketUIObject.SetActive(true);
        //     return;
        // }
        //ItemManager.RaiseItem(ItemName.COIN, -40);
        AddTime();
    }
    public void AddTime()
    {
        Timer.instance.Run(60);
        gameObject.SetActive(false);
    }
    public void Home()
    {
     //   SuSceneManager.LoadScene(SceneName.scn_home,null);
    }
    private void ActiveCoinMain()
    {
        coinMain.SetActive(true);
    }
}
