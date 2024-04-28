using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackController : MonoBehaviour
{

    public Transform Source;
    public Transform target1;
    public Transform target2;
    public Transform[] items;

    public int percentInt;
    public Image imageToFill;
    private bool check = false;
    private Coroutine CoroutineAnimation;

    private int FillAmount;
    private int FillAmountAfter;
    private int FillAmountSmall;


    public int MaxFillAmount;
    public TextMeshProUGUI textPercent;
    private Tween fillTween;

    public Image ImageBackground;
    private Color targetColor;

    public TextMeshProUGUI TextLevel;
    public TextMeshProUGUI TotalWood;
    private int battlePassLevelAfter;
    private int battlePassLevel;
    private bool checkNextLevel = false;
    public RectTransform magicPassBox;
    private int LevelLast;
    private void Awake()
    {
        targetColor = ImageBackground.color;
    }
    private void OnEnable()
    {
        checkNextLevel = false;
        // battlePassLevel = ItemManager.GetItemValue(ItemName.BATTLE_PASS_LEVEL);
        // battlePassLevelAfter = battlePassLevel + 1;
        // if(battlePassLevelAfter > 20)
        // {
        //     battlePassLevel = 0;
        // }
        // TextLevel.text = battlePassLevel.ToString();

        // try
        // {
        //     MaxFillAmount = BattlePassManager.BattlePassTicketPerLevel(battlePassLevel);
        // }
        // catch
        // {
        //     MaxFillAmount = 200;
        // }
        // percentInt =  ItemManager.GetItemValue(ItemName.BATTLE_PASS_TICKET);
        // textPercent.text = $"{percentInt}/{MaxFillAmount}";  
        // targetColor.a = 0f;
        // ImageBackground.color = targetColor;
        // targetColor.a = 0.5f;
        // check = false;
        // FillAmount = LevelController.Instance.totalWood;
        // TotalWood.text = LevelController.Instance.totalWood + "";
        // FillAmountAfter = percentInt + FillAmount;
        // FillAmountSmall = (int)(FillAmount / items.Length);
        // magicPassBox.anchoredPosition = new Vector2(150,757.2795f);
        // ImageBackground.DOColor(targetColor, 1f);
        // magicPassBox.DOAnchorPos(new Vector2(-441.846f, 757.2795f), 0.6f).SetEase(Ease.OutBack).OnComplete(ActiveAnimation);
        // LevelLast = battlePassLevel;      
    }
    private void ActiveAnimation()
    {
        imageToFill.fillAmount = (float)percentInt / MaxFillAmount;
        Test();         
    }

    [Button()]
    public void Test()
    {
        foreach(var item in items)
        {
            item.transform.position = Source.position;
        }
        CoroutineAnimation = StartCoroutine(StartAnimationPack());
    }
    IEnumerator StartAnimationPack()
    {
        int flag = 0;
        foreach (var item in items)
        {
            yield return new WaitForSeconds(0.2f);
            item.DOMove(target1.transform.position, 0.5f).OnComplete(
                () =>
                {
                    item.DOMove(target2.transform.position, 0.3f).OnComplete(
                    () =>
                    {
                        //SoundManager.Instance.PlaySound(AudioClipType.SFX_WOODIMPACT_1);
                        flag++;
                        if (flag == items.Length)
                        {
                            StartFillAmountFinish();
                        }
                        else
                        {
                            StartFillAmount();
                        }
                    }    
                    );
                }  
            );
        }
    }

    private void StartFillAmount()
    { 
        percentInt += FillAmountSmall;
        target2.DOScale(1.03f, 0.12f).SetEase(Ease.InOutSine).OnComplete(OneScale);
        if(percentInt >= MaxFillAmount)
        {
            checkNextLevel = true;
            FillAmountAfter -= percentInt;
            percentInt -= MaxFillAmount;
            //cap nhap maxFillMount
            // try
            // {
            //     MaxFillAmount = BattlePassManager.BattlePassTicketPerLevel(battlePassLevelAfter);
                
            // }
            // catch
            // {
            //     MaxFillAmount = 200;
            // }
            TextLevel.text = battlePassLevelAfter+"";
            fillTween?.Kill();
            imageToFill.fillAmount = (float)percentInt / MaxFillAmount;
            LevelLast = battlePassLevelAfter;
        }
        else
        {
            fillTween = imageToFill.DOFillAmount((float)percentInt/MaxFillAmount, 0.3f);
        }
        textPercent.text = $"{percentInt}/{MaxFillAmount}";

    }

    private void OneScale(){
        target2.localScale = Vector3.one;
    }

    private void StartFillAmountFinish()
    {
    //     percentInt = FillAmountAfter;    
    //     target2.DOScale(1.03f, 0.12f).SetEase(Ease.InOutSine).OnComplete(OneScale);
    //     if (percentInt >= MaxFillAmount)
    //     {
    //         checkNextLevel = true;
    //         percentInt -= MaxFillAmount;
    //         try
    //         {
    //             MaxFillAmount = BattlePassManager.BattlePassTicketPerLevel(battlePassLevelAfter);
    //         }
    //         catch
    //         {
    //             MaxFillAmount = 200;
    //         }
    //         TextLevel.text = battlePassLevelAfter + "";
    //         fillTween?.Kill();
    //         imageToFill.fillAmount = (float)percentInt / MaxFillAmount;
    //         LevelLast = battlePassLevelAfter;
    //     }
    //     else
    //     {
    //         fillTween = imageToFill.DOFillAmount((float)percentInt / MaxFillAmount, 0.3f);
    //     }
    //     textPercent.text = $"{percentInt}/{MaxFillAmount}";
    //    // ItemManager.SetItemValue(ItemName.BATTLE_PASS_TICKET, percentInt);
    //     //ItemManager.SetItemValue(ItemName.BATTLE_PASS_LEVEL, LevelLast);
    //     Invoke("CallUIWin", 1F);
    //     WinUI.checkActiveOther = false;
    }
    private void CallUIWin()
    {
        // if (checkNextLevel)
        // {
        //     PopupManager.ShowPopup(PopupName.POPUP_BATTLE_PASS, 1);
        // }
        gameObject.SetActive(false);      
    }
}
