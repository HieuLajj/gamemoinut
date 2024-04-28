using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    private static UIEvents instance;
    public static UIEvents Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIEvents>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private void Awake() {
        if (instance == null)
        {
            instance = this;      
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDefaultUI(){
        if(CanvasManagerGamePlay.Instance == null)return;
     
        CanvasManagerGamePlay.Instance.DefaultUI.gameObject.SetActive(true);
    }

    public void ShowWinUI(){
        // SuGame.Get<SuAds>().RequestAdsOnUserAction();
        GameMonitor.Instance.flagDifficultSupport++;
        HideMasterPass();
        LevelController.Instance.paritcleSystemWin.Play();
        ActiveWinUI();     
    }
    private void ActiveWinUI()
    {
        CanvasManagerGamePlay.Instance.WinUI.gameObject.SetActive(true);
    }

    public void ShowMasterPass()
    {
        CanvasManagerGamePlay.Instance.MassterPass.gameObject.SetActive(true);
    }
    public void HideMasterPass()
    {
        CanvasManagerGamePlay.Instance.MassterPass.gameObject.SetActive(false);
    }

    public void ShowEffectBlur()
    {
        CanvasManagerGamePlay.Instance.EffectBlur.gameObject.SetActive(true);
    }

    public void ShowEffectWinBlur()
    {
        CanvasManagerGamePlay.Instance.EffectBlurWin.gameObject.SetActive(true);
    }

    public void ShowRewardUI()
    {
        CanvasManagerGamePlay.Instance.RewardUI.gameObject.SetActive(true);
    }

    public void ShowBoosterUI()
    {
        CanvasManagerGamePlay.Instance.GameBoosterUI.gameObject.SetActive(true);
    }

    public void ShowMagicTicket()
    {
        CanvasManagerGamePlay.Instance.MagicTicketUI.gameObject.SetActive(true);
    }
}
