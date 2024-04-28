using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctionHammer : MonoBehaviour

{
    public Button HammerButton;
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
        LevelController.EventStartGame += Setup;
        LevelController.EventEndGame += DisActiveFunction;
        LevelController.EventStartGame += SetupUsedInt;
    }

    private void OnDisable()
    {
        LevelController.EventStartGame -= DisActiveFunction;
        LevelController.EventStartGame -= Setup;
        LevelController.EventEndGame -= DisActiveFunction;
        LevelController.EventStartGame -= SetupUsedInt;
    }

    public void Setup()
    {
        Slot_Item.EventDisActiveNailOther += ActiveFunction;
    }
    private void OnDestroy()
    {
        Slot_Item.EventDisActiveNailOther -= ActiveFunction;
    }
    private void SetupUsedInt()
    {
        usedInt = 0;
    }

    public void ActiveFunctions()
    {
        GameInBooster.ActionBooster = Active;
        CanvasManagerGamePlay.Instance.GameBoosterUI.nhan1.SetActive(true);
        CanvasManagerGamePlay.Instance.GameBoosterUI.nhan2.SetActive(false);
        CanvasManagerGamePlay.Instance.GameBoosterUI.anhBooster.sprite = imageNhan.sprite;
        GameInBooster.NameBooster = "hammerStr";
        GameInBooster.UsedBooster = usedInt;
        UIEvents.Instance.ShowBoosterUI();
    }
    private void Active()
    {
        Hammer.Instance.Active();
        usedInt++;
    }
    private void DisActiveFunction()
    {
        HammerButton.interactable = false;
        imageBackground.sprite = sprite[1];
        colortarget.a = 0.5f;
        imageNhan.color = colortarget;
    }

    private void ActiveFunction()
    {
        Slot_Item.EventDisActiveNailOther -= ActiveFunction;
        HammerButton.interactable = true;
        imageBackground.sprite = sprite[0];
        colortarget.a = 1f;
        imageNhan.color = colortarget;
    }
}
