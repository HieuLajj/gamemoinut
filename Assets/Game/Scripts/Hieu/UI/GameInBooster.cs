using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class GameInBooster : MonoBehaviour
{
    public static Action ActionBooster;
    public static string NameBooster;
    public static int UsedBooster;
    public TextMeshProUGUI textBooster;
    private int loseticketmagic;

    public TextMeshProUGUI textTicketMagic;
    public TextMeshProUGUI textTicketMagic2;
    public GameObject button2x;
    public GameObject button1x;
    public Image anhBooster;

    public GameObject nhan1;
    public GameObject nhan2;

    public GameObject coinMain;
    //public RectTransform pannelMain;
    private void OnEnable()
    {
        coinMain.SetActive(false);
        LevelController.EventEndGame += DisActiveFunction;
        Timer.instance.Pause();
        loseticketmagic = Mathf.Clamp(UsedBooster + 1, 1, 2);
        textTicketMagic.text = $"X{loseticketmagic}";
        textTicketMagic2.text = $"X{loseticketmagic}";
        if (UsedBooster >= 1)
        {
            button2x.SetActive(false);
            button1x.SetActive(true);
        }
        else
        {
            button2x.SetActive(true);
            button1x.SetActive(false);
        }
      //  textBooster.text = LocalizeManager.GetText(NameBooster);
      //  SoundManager.Instance.PlaySound(AudioClipType.SFX_UI_POPUP);
    }

    public void OnCreateAnimationDotween()
    {
        coinMain.SetActive(true);
    }

    private void OnDisable()
    {
        LevelController.EventEndGame -= DisActiveFunction;
        Timer.instance.Resume();
    }

    private void ActionBoosterFunction()
    {
        gameObject.SetActive(false);
        //DailyQuestManager.RaiseDailyQuestValue(DailyQuestType.USE_X_BOOSTERS, 1);
        ActionBooster?.Invoke();
    }
    public void ViewAds()
    {    
       //SuGame.Get<SuAds>().ShowRewardVideo(ActionBoosterFunction, null, UltilitiesHieu.ActionShowAdsString(NameBooster));
    }
    public void UseTicket()
    {
        // if (ItemManager.GetItemValue(ItemName.TICKET_MAGIC) < loseticketmagic)
        // {
        //     coinMain.SetActive(false);
        //     MagicTicket.DisableAction += ActiveCoinMain;
        //     MagicTicket.checkoption = 1;
        //     UIEvents.Instance.ShowMagicTicket();
        //     return;
        // }
        //ItemManager.RaiseItem(ItemName.TICKET_MAGIC, -loseticketmagic);
        ActionBoosterFunction();
    }
    private void DisActiveFunction()
    {
        gameObject.SetActive(false);
    }
    private void ActiveCoinMain()
    {
        coinMain.SetActive(true);
    }
   
}
