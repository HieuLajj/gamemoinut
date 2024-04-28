using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicTicket : MonoBehaviour
{
    public static Action DisableAction;
    public static int checkoption;
    public GameObject MagicOption;
    public GameObject SliverOption;
    public GameObject CoinOption;
    public static int checkactive = 0;
    private void OnEnable()
    {
       // SoundManager.Instance.PlaySound(AudioClipType.SFX_UI_POPUP);
        checkactive = 2;
        Timer.instance.Pause();
        if(checkoption == 1)
        {
            MagicOption.SetActive(true);
            SliverOption.SetActive(false);
            CoinOption.SetActive(false);
        }
        else if(checkoption == 2)
        {
            MagicOption.SetActive(false);
            SliverOption.SetActive(true);
            CoinOption.SetActive(false);
        }
        else
        {
            MagicOption.SetActive(false);
            SliverOption.SetActive(false);
            CoinOption.SetActive(true);
        }
    }
    private void OnDisable()
    {
        checkactive = 0;
        DisableAction?.Invoke();
        DisableAction = null;
    }
}
