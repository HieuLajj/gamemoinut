using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonFunctionNailPull : MonoBehaviour
{
    public Button BackButton;
    public Image imageBackground;
    public Image imageNhan;
    public Sprite[] sprite;
    private Color colortarget;
    private int usedInt;
    private void Awake()
    {
        colortarget = imageNhan.color;
    }
    private void OnEnable()
    {
        LevelController.EventEndGame += DisActiveFunction;
        LevelController.EventStartGame += SetupUsedInt;
        LevelController.EventStartGame += ActiveFunction;
    }

    private void OnDisable()
    {
        LevelController.EventEndGame -= DisActiveFunction;
        LevelController.EventStartGame -= SetupUsedInt;
        LevelController.EventStartGame -= ActiveFunction;
    }
    public void ActiveFunctions()
    {
        GameInBooster.ActionBooster = Active;
        CanvasManagerGamePlay.Instance.GameBoosterUI.nhan1.SetActive(true);
        CanvasManagerGamePlay.Instance.GameBoosterUI.nhan2.SetActive(false);
        CanvasManagerGamePlay.Instance.GameBoosterUI.anhBooster.sprite = imageNhan.sprite;
        GameInBooster.NameBooster = "screwStr";
        GameInBooster.UsedBooster = usedInt;
        UIEvents.Instance.ShowBoosterUI();
    }
    private void Active()
    {
        NailPullFeature.Instance.SetupFeature();
        usedInt++;
    }
    private void SetupUsedInt()
    {
        usedInt = 0;
    }
    private void DisActiveFunction()
    {
        BackButton.interactable = false;
        imageBackground.sprite = sprite[1];
        colortarget.a = 0.5f;
        imageNhan.color = colortarget;
    }

    private void ActiveFunction()
    {
        BackButton.interactable = true;
        imageBackground.sprite = sprite[0];
        colortarget.a = 1f;
        imageNhan.color = colortarget;
    }
}
