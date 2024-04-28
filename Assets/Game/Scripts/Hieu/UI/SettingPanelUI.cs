using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanelUI : MonoBehaviour
{
   public static int SoundCheck = 0;
   public static int musicCheck = 0;
   public static int MusicCheck
    {
        set {
            musicCheck = value;   
        }
        get
        {
            return musicCheck;
        }
    }
   public void OpenRate()
   {
        gameObject.SetActive(false);
        CanvasGameIn1.Instance.RatePanel.gameObject.SetActive(true);
   }

   
}
