using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using Spine.Unity;
using Unity.VisualScripting;

public class RewardUIInGame : MonoBehaviour
{
    //-1264.357
    //-1588
    public Image giftImage;
    public Sprite spriteMagicTicket;
    private int indexPercent;
    public int IndexPercent
    {
        get
        {
            return indexPercent;
        }
        set
        {
            textPercent.text = $"{value}/{maxPercent}";
            if (value >= 3)
            {
                ContinueRectTransform.anchoredPosition = new Vector2(0, -1588);
            }
            else
            {
                ContinueRectTransform.anchoredPosition = new Vector2(0, -1264.357f);
            }
            CountText(value, indexPercent);
            indexPercent = value;
            PlayerPrefs.SetInt("PlayerPrefsStringKeyCompleteIndex", value);
        }
    }
    private int maxPercent;
    
    public TextMeshProUGUI textPercent;
    public GameObject ContinueButton;
    private RectTransform ContinueRectTransform;
    public Image imageChests;
    public Image imageChestsBorder;
    public Transform giftReward;
    public SkeletonGraphic skeletonAnimationGift;
    public GameObject PopupReward;
    private RectTransform imageRewardRectTransform;
    private readonly string playerPrefs_checkfirstchests = "playerPrefs_checkfirstchests";
    private void Awake()
    {
        ContinueRectTransform = ContinueButton.GetComponent<RectTransform>();
        indexPercent = PlayerPrefs.GetInt("PlayerPrefsStringKeyCompleteIndex");
        imageRewardRectTransform = giftReward.GetComponent<RectTransform>();
        if (PlayerPrefs.GetInt(playerPrefs_checkfirstchests) == 0)
        {
            maxPercent = 3;
        }
        else
        {
            maxPercent = 5;
        }
    }

    private void OnEnable()
    {
        if (IndexPercent >= maxPercent)
        {
          
            indexPercent = 0;
        }
        IndexPercent += 1;
        PopupReward.SetActive(true);
        skeletonAnimationGift.gameObject.SetActive(false);  
        imageChestsBorder.gameObject.SetActive(true);
        giftReward.gameObject.SetActive(false);
        ContinueButton.SetActive(false);
      
        skeletonAnimationGift.AnimationState.Complete += AnimationComplete;

    }

    void AnimationComplete(Spine.TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Equals("open"))
        {
            skeletonAnimationGift.AnimationState.SetAnimation(0, "idle_2", true);

        }
    }

    private void OnDisable()
    {
        skeletonAnimationGift.AnimationState.Complete -= AnimationComplete;
    }

    private void CountText(int newValue, int oldValue)
    {
        imageChests.fillAmount = (float)oldValue / maxPercent; 
        imageChests.DOFillAmount((float)newValue / maxPercent, 0.9f).OnComplete(OpenChests);
    }
    private void OpenChests()
    {
    //    if (IndexPercent >= maxPercent)
    //    {
    //         if (maxPercent == 3)
    //         {
    //             PlayerPrefs.SetInt(playerPrefs_checkfirstchests, 1);
    //             maxPercent = 5;
    //         }
    //        // Sprite spritereward = SkinManager.UnlockRandomSkin();
    //         if (spritereward!=null)
    //         {
    //             giftImage.sprite = spritereward;
    //         }
    //         else
    //         {
    //             giftImage.sprite = spriteMagicTicket;
    //             ItemManager.RaiseItem(ItemName.TICKET_MAGIC, 1);
    //         }
    //         indexPercent = 0;
    //         StartCoroutine(ActionAnimation());
    //         return;
    //    }
       ContinueButton.SetActive(true);
    }

    IEnumerator ActionAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        PopupReward.SetActive(false);
        skeletonAnimationGift.gameObject.SetActive(true);
        skeletonAnimationGift.AnimationState.SetAnimation(0, "open", false);
       
        imageChestsBorder.gameObject.SetActive(false);
        //yield return new WaitForSeconds(2.6f);
        //skeletonAnimationGift.AnimationState.SetAnimation(0, "idle_2", true);
        yield return new WaitForSeconds(1.6f);   
        giftReward.gameObject.SetActive(true);
        imageRewardRectTransform.anchoredPosition = new Vector2(-25, -960);
        imageRewardRectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        imageRewardRectTransform.DOScale(new Vector3(1.8f,1.8f,1.8f), 0.5f);
        imageRewardRectTransform.DOAnchorPos(new Vector2(-25, -600), 1).OnComplete(ActiveContinueButton);
        //   ItemManager.RaiseItem(ItemName.TICKET_MAGIC, 1);    
    }

    private void ActiveContinueButton(){
        ContinueButton.SetActive(true);
    }

   public void NextLevel()
   {
        CanvasManagerGamePlay.Instance.EffectBlur.ActionAwaitEffect = NextLevelSub;
        UIEvents.Instance.ShowEffectBlur();
       
   }
    private void NextLevelSub()
    {
        LevelController.Instance.NextLevel();
        gameObject.SetActive(false);
    }
}
