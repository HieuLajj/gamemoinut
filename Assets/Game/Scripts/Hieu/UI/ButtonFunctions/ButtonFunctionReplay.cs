using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctionReplay : MonoBehaviour
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
    private Coroutine coroutineDelayReplay;
    private bool checkCoroutineDelay;

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
        if (checkCoroutineDelay)
        {
            StopCoroutine(coroutineDelayReplay);
        }
    }
    public void ActiveFunctions()
    {
        GameInBooster.ActionBooster = Active;
        CanvasManagerGamePlay.Instance.GameBoosterUI.nhan1.SetActive(true);
        CanvasManagerGamePlay.Instance.GameBoosterUI.nhan2.SetActive(false);
        CanvasManagerGamePlay.Instance.GameBoosterUI.anhBooster.sprite = imageNhan.sprite;
        GameInBooster.NameBooster = "replay";
        GameInBooster.UsedBooster = usedInt;
        UIEvents.Instance.ShowBoosterUI();
    }
    private void Active()
    {
        coroutineDelayReplay = StartCoroutine(awaitActive());
    }

    private IEnumerator awaitActive()
    {
        checkCoroutineDelay = true;
        yield return new WaitForSeconds(0.75f);
        LevelController.Instance.ResetLevel();
        usedInt++;
        checkCoroutineDelay = false;
    }

    private void SetupUsedInt()
    {
        usedInt = 0;
    }
    private void DisActiveFunction()
    {
        if (checkCoroutineDelay)
        {
            StopCoroutine(coroutineDelayReplay);
        }
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
