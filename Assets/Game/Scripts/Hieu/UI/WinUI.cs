using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WinUI : MonoBehaviour
{
    public static bool checkActiveOther;
    public Button buttonNext;
    public TextMeshProUGUI textTimeWin;
    private void OnEnable()
    {
        int timemath = GameMonitor.Instance.TimeMatchAll();
        int number = timemath / 60;
        int number2 = timemath % 60;
       // textTimeWin.text =  LocalizeManager.GetText("time_played",Timer.GetNumberWithZeroFormat(number) + ":" + Timer.GetNumberWithZeroFormat(number2));
        buttonNext.gameObject.SetActive(false);
        Invoke("ActiveButtonNext", 1f);
    }
    private void ActiveButtonNext()
    {
        buttonNext.gameObject.SetActive(true);
    }
    public void ReplayBtn(){
        ControllerHieu.Instance.nailLayerController.ClearLayer();
        ControllerHieu.Instance.rootlevel?.ClearRoot(() =>
        {
            LevelController.Instance.StartLevel_LoadLevel();
            gameObject.SetActive(false);
        });
    }

    public void NextLevel(){
        if (checkActiveOther) return;
      
        if (LevelController.Instance.LevelIDInt>=4)
        {
            UIEvents.Instance.ShowRewardUI();
            gameObject.SetActive(false);
            return;
        }
        CanvasManagerGamePlay.Instance.EffectBlurWin.ActionAwaitEffect = NextLevelContinue;
        UIEvents.Instance.ShowEffectWinBlur();
    }
    private void NextLevelContinue()
    {
        LevelController.Instance.NextLevel();
        gameObject.SetActive(false);
    }
}
