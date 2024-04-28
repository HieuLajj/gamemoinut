using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

public class Slot_Item : MonoBehaviour, TInterface<Slot_Item>
{
    public static Action EventActiveNail;
    public static Action EventDisActiveNail;
    public static Action EventDisActiveNailOther;
    public static int flag = 1;
    //pool
    private ObjectPool<Slot_Item> _pool;

    public bool hasNail;
    public bool hasLock;
    public bool hasLockAds;
    public Ad_Item Aditem;
    public Nail_Item nail_item;

    public Collider2D mainCheckCollider;

    public SpriteRenderer spriteRenderer;
    private bool savepre_hasNail;
    private Ad_Item savepre_AdItem;
    private Nail_Item savepre_NailItem;


    public void RegisterEventBack()
    {      
        BackFeature.Instance.EventBackFeature += ActiveFeatureback;
        SaveInformation();
    }

    public void UnRegisterEventBack()
    {
        BackFeature.Instance.EventBackFeature -= ActiveFeatureback;
    }
    public void SaveInformation()
    {
        savepre_hasNail = hasNail;
        savepre_AdItem = Aditem;
        savepre_NailItem = nail_item;
    }
    public void ActiveFeatureback()
    {     
        hasNail = savepre_hasNail;
        Aditem = savepre_AdItem;
        nail_item = savepre_NailItem;
    }

    public void ActiveWhenDown()
    {

        if (flag == 1)
        {        
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == true)
            {
               
                return;
            }
            else
            {             
                flag = 2;
            }
        }
        if (hasLock == true) {
          //  Locked.CreateLocked(transform.position);
          
            return;
        }
        if (NailPullFeature.Instance.check && nail_item!=null)
        {
            hasNail = false;
            nail_item.Furture_Destroy();
            nail_item = null;
            NailPullFeature.Instance.NotSetupFeature();
         
            return;
        }

#if UNITY_EDITOR
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == true)
        {
            flag = 1;

            return;
        }
#else
        // if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == true)
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
       {
            flag = 1;
         
            return;
       }
#endif

        LevelController.Instance.ActiveActionUser();

        if(Aditem != null)
        {
            //Aditem.animator.SetTrigger("play");
            //Aditem = null;
            CanvasManagerGamePlay.Instance.GameBoosterUI.nhan1.SetActive(false);
            CanvasManagerGamePlay.Instance.GameBoosterUI.nhan2.SetActive(true);
            GameInBooster.ActionBooster = BoosterAd;
            GameInBooster.NameBooster = "holeStr";
            GameInBooster.UsedBooster = 0;
            UIEvents.Instance.ShowBoosterUI();
            return;
        }
        

        if (ControllPlayGame.Instance.targetNail == nail_item)
        {
            if(ControllPlayGame.Instance.targetNail != null)
            {
                ControllPlayGame.Instance.targetNail.ResetImageNail();

                EventDisActiveNail?.Invoke();
                ControllPlayGame.Instance.targetNail = null;
            }
          
            return;
        }

        if (ControllPlayGame.Instance.targetNail != null && hasNail == false)
        {
          
            ControllPlayGame.Instance.targetNail.KinematicBoardParent();
            StartCoroutine(Checkboardinslot());
        }
        else
        {
          
            if (nail_item != null && hasNail == true)
            {
                if (ControllPlayGame.Instance.targetNail != null)
                {
                    ControllPlayGame.Instance.targetNail.ResetImageNail();               
                }   
                ControllPlayGame.Instance.targetNail = nail_item;           
                //thuc hien kinamatic cha cua nail
                nail_item.ActiveImageNail();
                EventActiveNail?.Invoke();

            }
        }
    }
    private void BoosterAd()
    {
        Aditem.animator.SetTrigger("play");
        Aditem = null;
    }
    public bool CheckRunNotLock()
    {
        if(hasLock == false && nail_item==null && Aditem == null)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(mainCheckCollider.transform.position, new Vector2(0.075f, 0.075f), 0);
            int check = 0;
            //cham board la +1 cham slotbot la -1 khi nao bang 0 thi cho qua
            foreach (Collider2D collider in colliders)
            {

                if (collider.CompareTag("Board"))
                {
                    check += collider.gameObject.GetInstanceID();
                }
                if (collider.gameObject.CompareTag("HigleJoint2d"))
                {
                    check -= collider.transform.parent.gameObject.GetInstanceID();
                }
            }
            if (check == 0)
            {
                return true;
            }
        }
        return false;
    }
   
    IEnumerator Checkboardinslot()
    {
        yield return new WaitForSeconds(0.0f);
        if (ControllPlayGame.Instance.targetNail != null)
        {
        
            Vector2 size = mainCheckCollider.bounds.size;
            int check = 0;
            //new Vector2(0.075f,0.075f)
            Collider2D[] colliders = Physics2D.OverlapBoxAll(mainCheckCollider.transform.position, new Vector2(0.15f, 0.15f), 0);     
            //cham board la +1 cham slotbot la -1 khi nao bang 0 thi cho qua
            foreach (Collider2D collider in colliders)
            {

                if (collider.CompareTag("Board"))
                {
                    check+= collider.gameObject.GetInstanceID();        
                }
               //0.02
                if (collider.gameObject.CompareTag("HigleJoint2d")&&Math.Abs(Vector3.Distance(gameObject.transform.position, collider.transform.position))<=0.06F)
                {               
                    check-= collider.transform.parent.gameObject.GetInstanceID();
                }
            }

            if (check != 0)
            {
                
                ControllPlayGame.Instance.targetNail.testKinematicBoardParent();           
            }
            else
            {
                //luu cac trang thai cua board lien quan den chiec dinh nay
                BackFeature.Instance.UnsubscribeAll();


                //check theo kieu moi
                if (ControllerHieu.Instance.nailLayerController.FindnewTypeNail == true)
                {
                    ControllerHieu.Instance.nailLayerController.EventNailRegistedNewType?.Invoke();
                    Collider2D[] collidersnail = Physics2D.OverlapCircleAll(ControllPlayGame.Instance.targetNail.preslot_item.transform.position, 6f);
                    Array.Sort(collidersnail, (a, b) => {
                        float distanceA = Vector2.Distance(a.transform.position, ControllPlayGame.Instance.targetNail.preslot_item.transform.position);
                        float distanceB = Vector2.Distance(b.transform.position, ControllPlayGame.Instance.targetNail.preslot_item.transform.position);
                        return distanceA.CompareTo(distanceB);
                    });
                    foreach (Collider2D collider in collidersnail)
                    {
                        if (collider.CompareTag("Nail"))
                        {
                            Nail_Item nailitem = collider.gameObject.GetComponent<Nail_Item>();
                            nailitem.RegisterNailTypeLayer();
                            LoadDataBase.Instance.CreatePhysic2dforboard_layersub_2(nailitem);

                        }
                    }
                    ControllPlayGame.Instance.targetNail.UnRegisterNailTypeLayer();
                }

                ControllPlayGame.Instance.targetNail.RegisterEventBackFeature();
                ControllPlayGame.Instance.targetNail.slot_item.RegisterEventBack();
                RegisterEventBack();
                foreach(Board_Item board_Item in LoadDataBase.Instance.listsBoardAwaitDeath)
                {
                    board_Item.RegisterEventBack();
                }



                ControllPlayGame.Instance.targetNail.ColiderNail.isTrigger = true;
                ControllPlayGame.Instance.targetNail.transform.position = transform.position;
                ControllPlayGame.Instance.targetNail.ResetImageNailWithParticle();
                ControllPlayGame.Instance.targetNail.ResetDisactiveListHingeJoint();
                ControllPlayGame.Instance.targetNail.slot_item.ResetNail();

                EventDisActiveNail?.Invoke();
                EventDisActiveNailOther?.Invoke();

                SetUpNail(ControllPlayGame.Instance.targetNail);
                //if(!Controller.Instance.nailLayerController.FindnewTypeNail)
                {
                    ControllPlayGame.Instance.targetNail.CheckOverlapBoxBoard(this, ControllPlayGame.Instance.targetNail);
                }
                //else
                //{
                //    if (ControllPlayGame.Instance.pre_targetNail != ControllPlayGame.Instance.targetNail)
                //    {
                //        ControllPlayGame.Instance.targetNail.CheckOverlapBoxBoardNotLayer(this, ControllPlayGame.Instance.targetNail);
                //    }
                //    else
                //    {
                //        ControllPlayGame.Instance.targetNail.CheckOverlapBoxBoard(this, ControllPlayGame.Instance.targetNail);
                //    }
            
                //}               
                ControllPlayGame.Instance.targetNail = null;
            }
        }
    }
    float CalculateOverlapPercentage(Collider2D colliderA, Collider2D colliderB)
    {
        Bounds boundsA = colliderA.bounds;
        Bounds boundsB = colliderB.bounds;

        float intersectionWidth = Mathf.Min(boundsA.max.x, boundsB.max.x) - Mathf.Max(boundsA.min.x, boundsB.min.x);
        float intersectionHeight = Mathf.Min(boundsA.max.y, boundsB.max.y) - Mathf.Max(boundsA.min.y, boundsB.min.y);

        float intersectionArea = Mathf.Max(0, intersectionWidth) * Mathf.Max(0, intersectionHeight);
        float totalArea = boundsA.size.x * boundsA.size.y + boundsB.size.x * boundsB.size.y;

        float overlapPercentage = intersectionArea / totalArea * 100f;
      
        return overlapPercentage;
    }

    public void ResetNail()
    {
        hasNail = false;

        //hasLock = false;
        //Aditem = null;

        nail_item = null;
    }

    public void SetUpNail(Nail_Item nail_Item2)
    {
      
        hasNail = true;
        nail_item = nail_Item2;
        nail_Item2.transform.parent = transform;
        nail_Item2.preslot_item = nail_Item2.slot_item;
        nail_Item2.slot_item = this;
    }


    public void SetPool(ObjectPool<Slot_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Slot_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {
        nail_item = null;
        Aditem = null;
    }

    public void StartCreate()
    {
       // if (DataManagerHieu.Instance.CurrentBoard != null)
        {
          //  spriteRenderer.sprite = DataManagerHieu.Instance.SpriteCurrentHole;
        }
    }

}
