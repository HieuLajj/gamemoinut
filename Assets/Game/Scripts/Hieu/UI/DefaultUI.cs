using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DefaultUI : MonoBehaviour
{
    // public void Show(){
    //     UIEvents.Instance.ShowDefaultUI();
    // }
    public Button ButtonAds;
    public Button ButtonCoin;
    public static int typeDefault;

    private void OnEnable()
    {
        if(typeDefault == 0)
        {
            ButtonAds.interactable = true;
            ButtonCoin.interactable = true;
        }
        else
        {
            ButtonAds.interactable = false;
            ButtonCoin.interactable = false;
        }
        //SoundManager.Instance.PlaySound(AudioClipType.SFX_UI_POPUP);
        CoinsMain.SetActive(false);
        
    }

    public GameObject CoinsMain;

    public void ReplayBtn(){
        LevelController.Instance.ReplayLevel();
        gameObject.SetActive(false);
    }

    public void NextLevel(){
        LevelController.Instance.NextLevel();
        gameObject.SetActive(false);
    }

    public void ViewAds()
    {
       // SuGame.Get<SuAds>().ShowRewardVideo(AddTime,null, UltilitiesHieu.ActionShowAdsString("outofmove"));
        //SuAnalytics.LogEventLevelEnd(LevelController.Instance.LevelIDInt, "", false);
    }
    public void OnCreateAnimationDotween()
    {
        CoinsMain.SetActive(true);
    }
    public void UseCoin()
    {
        // if (ItemManager.GetItemValue(ItemName.COIN) < 40)
        // {
        //     CoinsMain.SetActive(false);
        //     MagicTicket.checkoption = 3;
        //     MagicTicket.DisableAction += ActiveCoinMain;
        //     UIEvents.Instance.ShowMagicTicket();          
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
    private void ActiveCoinMain()
    {
        CoinsMain.SetActive(true);
    }
    public void Home()
    {
       // SuSceneManager.LoadScene(SceneName.scn_home, null);
    }
}
