using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMonitor : MonoBehaviour
{
    public DateTime start_match_utc;
    public int TimeTotalSpendBeforePause;
    private int _flagDifficultSupport;
    public int flagDifficultSupport
    {
        set
        {
            _flagDifficultSupport = value;
            PlayerPrefs.SetInt(PlayerPrefs_FlagDifficultSupport,_flagDifficultSupport);
        }
        get { return _flagDifficultSupport; }
    }
    private readonly string PlayerPrefs_FlagDifficultSupport = "PlayerPrefs_FlagDifficultSupport";
    private static GameMonitor instance;
    public static GameMonitor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameMonitor>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private Coroutine monitorNailCoroutine;
    private bool checkRunNailCoroutine;
    private void Awake()
    {
        flagDifficultSupport = PlayerPrefs.GetInt(PlayerPrefs_FlagDifficultSupport);
        Slot_Item.EventDisActiveNail += StartMonitor;
    }
    public void StartMonitor()
    {  
        if (checkRunNailCoroutine == true) return;       
        monitorNailCoroutine = StartCoroutine(CallFunctionEveryTenSeconds());
        checkRunNailCoroutine = true;
    }

    private void OnDestroy()
    {
        Slot_Item.EventDisActiveNail -= StartMonitor;
    }

    public void StopMonitor()
    {       
        if (checkRunNailCoroutine == false) return;  
        StopCoroutine(monitorNailCoroutine);
        checkRunNailCoroutine = false;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            if (start_match_utc != null)
            {
                TimeTotalSpendBeforePause += totalTime();
            }
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Invoke("OnApplicationFocus2", 0.5f);
        }

    }
    private void OnApplicationFocus2()
    {
        if (TimeTotalSpendBeforePause != 0)
        {
            start_match_utc = DateTime.Now;
        }
    }

    public int totalTime()
    {
        DateTime end_match_utc = DateTime.Now;
        TimeSpan timeSpan = end_match_utc.Subtract(start_match_utc);
        return (int)timeSpan.TotalSeconds;
    }

    public int TimeMatchAll()
    {
        return totalTime() + TimeTotalSpendBeforePause;
    }
    private IEnumerator CallFunctionEveryTenSeconds()
    {

        yield return new WaitForSeconds(10f);

        if (checkRunNailCoroutine)
        {
            YourFunctionToCall();
        }
    }


    private void YourFunctionToCall()
    {

    
        foreach (Slot_Item slot in ControllerHieu.Instance.rootlevel.litsslot_mydictionary.Values)
        {
            if (slot.CheckRunNotLock() && slot.hasLockAds ==false)
            {       
                checkRunNailCoroutine = false;
                StartMonitor();
                return;
            }
        }
       
        checkRunNailCoroutine = false;
       // Notification.Instance.CallNotificaiton(LocalizeManager.GetText("out_of_move"),Color.red);
    }

   
}
