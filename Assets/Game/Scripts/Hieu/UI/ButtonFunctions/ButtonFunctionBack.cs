using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctionBack : MonoBehaviour
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
        LevelController.EventStartGame += DisActiveFunction;
        Slot_Item.EventDisActiveNailOther += ActiveFunction;
        LevelController.EventEndGame += DisActiveFunction;
        LevelController.EventStartGame += SetupUsedInt;
    }
    private void OnDisable()
    {
        LevelController.EventStartGame -= DisActiveFunction;
        Slot_Item.EventDisActiveNailOther -= ActiveFunction;
        LevelController.EventEndGame -= DisActiveFunction;
        LevelController.EventStartGame -= SetupUsedInt;
    }


    private void SetupUsedInt()
    {
        usedInt = 0;
    }
    public void Active()
    {
        BackFeature.Instance.Active();
        DisActiveFunction();
        usedInt++;
    }
    public void ActiveFunctions()
    {
        CanvasManagerGamePlay.Instance.GameBoosterUI.nhan1.SetActive(true);
        CanvasManagerGamePlay.Instance.GameBoosterUI.nhan2.SetActive(false);
        CanvasManagerGamePlay.Instance.GameBoosterUI.anhBooster.sprite = imageNhan.sprite;
        GameInBooster.ActionBooster = Active;
        GameInBooster.NameBooster = "undoStr";
        GameInBooster.UsedBooster = usedInt;
        UIEvents.Instance.ShowBoosterUI();
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
