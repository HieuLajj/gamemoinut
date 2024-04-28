using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateCheckEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {     
        // if (LevelController.Instance.LevelIDInt > RemoteConfigManager.General_Config.rate_config.level_show)
        // {
        //     return;
        // }
        LevelController.EventCompleteGame += CheckRate;
    }
    private void CheckRate()
    {
        // if (LevelController.Instance.LevelIDInt == RemoteConfigManager.General_Config.rate_config.level_show)
        // {
        //     //PopupManager.ShowPopup(PopupName.POPUP_RATE);
        //     LevelController.EventCompleteGame -= CheckRate;
        // }
    }
}
