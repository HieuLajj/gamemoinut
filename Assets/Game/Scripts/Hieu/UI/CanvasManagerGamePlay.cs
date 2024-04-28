using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CanvasManagerGamePlay : MonoBehaviour
{
    private static CanvasManagerGamePlay instance;
    public static CanvasManagerGamePlay Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CanvasManagerGamePlay>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public void Home(){
        ControllerHieu.Instance.nailLayerController.ClearLayer();
        ControllerHieu.Instance.rootlevel?.ClearRoot(() =>
        {
            SceneManager.LoadScene("Level");
        });
    }
    public TextLevel textLevel;
    public Transform DefaultUI;
    public Transform WinUI;
    public Transform MassterPass;
    public Transform IngameUI;
    public Transform RewardUI;
    public GameInBooster GameBoosterUI;
    public Transform MagicTicketUI;

    public EffectBlur EffectBlur;
    public EffectBlur EffectBlurWin;

    public void ShowPopUpSetting()
    {
        //PopupManager.ShowPopup(PopupName.POPUP_SETTING);
    }
    [Button()]
    public void ShowPopUpRate()
    {
        //PopupManager.ShowPopup(PopupName.POPUP_RATE);
    }
}
