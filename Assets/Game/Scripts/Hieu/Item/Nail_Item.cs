using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SocialPlatforms.Impl;
using System;
using Sirenix.OdinInspector;
using DG.Tweening;
public class Nail_Item : MonoBehaviour, TInterface<Nail_Item>
{
    public SpriteRenderer Outline;
    //pool
    private ObjectPool<Nail_Item> _pool;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteCrewFoot;
  
    public Nail nail;
    public Slot_Item slot_item;
    public Slot_Item preslot_item;
    public CircleCollider2D ColiderNail;
    public List<Slot_board_Item> listSlotBoardItem;
    private Board_Item boardItemBeforeandAfter;
    public string keyLayer;
    public string keyLayerPre;
    private Vector3 savepre_Postion;
    private Transform savepre_Parent;
    private Slot_Item savepre_slotItem;
    private Slot_Item savepre_preslotItem;
    private List<Slot_board_Item> savepre_ListHingeJoin = new List<Slot_board_Item>();
    private Coroutine _EffectNailCoroutine;
    public Vector3 startScale;
    private void Awake()
    {
        ColiderNail = GetComponent<CircleCollider2D>();
    }


    public void CheckLayerString(string hhhh)
    {

    }

    public void ActiveMask()
    {
        foreach (Slot_board_Item item in listSlotBoardItem)
        {
            item.mask.transform.parent.gameObject.SetActive(true);
        }
    }
    public void DisActiveMask()
    {
        foreach (Slot_board_Item item in listSlotBoardItem)
        {
            item.mask.transform.parent.gameObject.SetActive(false);
        }      
    }

    public void ResetDisactiveListHingeJoint()
    {
        for (int i = 0; i < listSlotBoardItem.Count; i++)
        {
            if (listSlotBoardItem[i].hingeJointInSlot != null)
            {
                listSlotBoardItem[i].SetHingeJointInSlotBool(false);
                Board_Item boardItem = listSlotBoardItem[i].hingeJointInSlot.transform.GetComponent<Board_Item>();
                if (boardItem.CheckHigle() == false)
                {
                    LoadDataBase.Instance.listsBoardAwaitDeath.Add(boardItem);
                }
            }
        }
        listSlotBoardItem.Clear();
    }

    public void RegisterEventBackFeature()
    {
        
        RegisterEventBack();
        for (int i = 0; i < listSlotBoardItem.Count; i++)
        {
            Board_Item boardItem = listSlotBoardItem[i].hingeJointInSlot.GetComponent<Board_Item>();
            boardItem.RegisterEventBack();
        }
    }
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
        savepre_Postion = transform.position;
        savepre_ListHingeJoin.Clear();    
        savepre_ListHingeJoin.AddRange(listSlotBoardItem);       
        savepre_Parent = transform.parent;
        savepre_slotItem = slot_item;
        savepre_preslotItem = preslot_item;
    }
    public void ActiveFeatureback()
    {
       
        gameObject.layer = 6;
        transform.position = savepre_Postion;
        ResetDisactiveListHingeJoint();
        listSlotBoardItem.Clear();
        listSlotBoardItem.AddRange(savepre_ListHingeJoin);
        ControllPlayGame.Instance.targetNail = this;
        this.ActiveImageNail();
        UnRegisterEventBack();
        transform.parent = savepre_Parent;
        slot_item = savepre_slotItem;
        preslot_item = savepre_preslotItem;
        StartCoroutine(SetLayerObject());       
    }

    IEnumerator SetLayerObject()
    {
        yield return new WaitForSeconds(.25f);
        LoadDataBase.Instance.CreatePhysic2dforboard_layersub_2(this);
    }

    [Button()]
    public void Furture_Destroy()
    {
        ResetDisactiveListHingeJoint();
        gameObject.SetActive(false);
    }



    public void CheckOverlapBoxBoard(Slot_Item slotItem, Nail_Item nailtargetbefore)
    {
        StartCoroutine(checkover(slotItem, nailtargetbefore));
    }
    IEnumerator checkover(Slot_Item slotItem, Nail_Item nailtargetbefore)
    {
        yield return new WaitForSeconds(0f);
        try
        {
            Bounds boundnail = ColiderNail.bounds;
            Vector2 size = boundnail.size;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);
            List<int> layerboard = new List<int>();    
            foreach (Collider2D collider in colliders)
            {          
                if (collider.gameObject.CompareTag("HigleJoint2d"))
                {                 
                    CircleCollider2D circleCollider = collider as CircleCollider2D;
                    float distanceCicle = Vector3.Distance(transform.position, collider.gameObject.transform.position);
                    if(distanceCicle < circleCollider.radius && distanceCicle < ColiderNail.radius) { 
                        Board_Item boarditem = collider.transform.parent.GetComponent<Board_Item>();
                        Slot_board_Item slotboardItem = collider.GetComponent<Slot_board_Item>();

                        if(boarditem == null)
                        {
                        }
                        else
                        {
                            Slot_board_Item findanySlotBoardIteminboard = CheckHingleInBoard(boarditem);                     
                            if (findanySlotBoardIteminboard == null)
                            {
                                Vector3 positionchange = slotItem.transform.position - slotboardItem.transform.position;
                                boarditem.transform.position += positionchange;
                                listSlotBoardItem.Add(slotboardItem);
                               // slotboardItem.hingeJointInSlot.enabled = true;
                                slotboardItem.SetHingeJointInSlotBool(true);
                                layerboard.Add(collider.transform.parent.gameObject.layer - 7);
                            }
                            else
                            {

                                listSlotBoardItem.Add(slotboardItem);
                                // slotboardItem.hingeJointInSlot.enabled = true;
                                slotboardItem.SetHingeJointInSlotBool(true);
                                layerboard.Add(collider.transform.parent.gameObject.layer - 7);
                                boarditem.AutoRotate(slotboardItem, findanySlotBoardIteminboard, slotItem);
                            }
                        }

                    }
                }
            }
            gameObject.layer = ControllerHieu.Instance.nailLayerController.InputNumber(layerboard,this,0);
            ColiderNail.isTrigger = false;
            nailtargetbefore.testKinematicBoardParent();
        }
        catch (Exception e)
        {      
            Debug.LogException(e);
            LevelController.Instance.ResetLevel();         
        }
    }


    public void CheckOverlapBoxBoardNotLayer(Slot_Item slotItem, Nail_Item nailtargetbefore)
    {
        StartCoroutine(checkoverNotlayer(slotItem, nailtargetbefore));
    }
    IEnumerator checkoverNotlayer(Slot_Item slotItem, Nail_Item nailtargetbefore)
    {
        yield return new WaitForSeconds(0f);
        try
        {
            Bounds boundnail = ColiderNail.bounds;
            Vector2 size = boundnail.size;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0);
            List<int> layerboard = new List<int>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("HigleJoint2d"))
                {
                    CircleCollider2D circleCollider = collider as CircleCollider2D;
                    float distanceCicle = Vector3.Distance(transform.position, collider.gameObject.transform.position);
                    if (distanceCicle < circleCollider.radius && distanceCicle < ColiderNail.radius)
                    {
                        Board_Item boarditem = collider.transform.parent.GetComponent<Board_Item>();
                        Slot_board_Item slotboardItem = collider.GetComponent<Slot_board_Item>();

                        if (boarditem == null)
                        {
                        }
                        else
                        {
                            Slot_board_Item findanySlotBoardIteminboard = CheckHingleInBoard(boarditem);
                            if (findanySlotBoardIteminboard == null)
                            {
                                Vector3 positionchange = slotItem.transform.position - slotboardItem.transform.position;
                                boarditem.transform.position += positionchange;
                                listSlotBoardItem.Add(slotboardItem);
                                //slotboardItem.hingeJointInSlot.enabled = true;
                                slotboardItem.SetHingeJointInSlotBool(true);
                                layerboard.Add(collider.transform.parent.gameObject.layer - 7);
                            }
                            else
                            {

                                listSlotBoardItem.Add(slotboardItem);
                                //slotboardItem.hingeJointInSlot.enabled = true;
                                slotboardItem.SetHingeJointInSlotBool(true);
                                layerboard.Add(collider.transform.parent.gameObject.layer - 7);
                                boarditem.AutoRotate(slotboardItem, findanySlotBoardIteminboard, slotItem);
                            }
                        }

                    }
                }
            }
            gameObject.layer = 6;
            ControllerHieu.Instance.nailLayerController.CheckRootNail(this);
            ColiderNail.isTrigger = false;
            nailtargetbefore.testKinematicBoardParent();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            LevelController.Instance.ResetLevel();
        }
    }




    //kiem tra co higlejoint nao trong board con hoat dong khong
    public Slot_board_Item CheckHingleInBoard(Board_Item board_item)
    {        
            for (int i = 0; i < board_item.listslot.Count; i++)
            {
                if (board_item.listslot[i].hingeJointInSlot.enabled)
                {
                    return board_item.listslot[i];
                }
            }
            return null;      
    }

    float CalculateOverlapPercentage(Collider2D colliderA, Collider2D colliderB)
    {
        Bounds boundsA = colliderA.bounds;
        Bounds boundsB = colliderB.bounds;

        float intersectionWidth = Mathf.Min(boundsA.max.x, boundsB.max.x) - Mathf.Max(boundsA.min.x, boundsB.min.x);
        float intersectionHeight = Mathf.Min(boundsA.max.y, boundsB.max.y) - Mathf.Max(boundsA.min.y, boundsB.min.y);

        float intersectionArea = Mathf.Max(0, intersectionWidth) * Mathf.Max(0, intersectionHeight);
        float areaA = boundsA.size.x * boundsA.size.y;

        float overlapPercentage = intersectionArea / areaA * 100f;
        return overlapPercentage;
    }
    public void SetPool(ObjectPool<Nail_Item> pool) {
        _pool = pool;
    }

    public void ResetPool() {
        _pool.Release(this);
    }

    public Nail_Item IGetComponentHieu() {
        return this;
    }

    public void ResetAfterRelease()
    {
        listSlotBoardItem.Clear();
        ColiderNail.isTrigger = false;  
        if (ControllerHieu.Instance.nailLayerController.FindnewTypeNail)
        {
            ControllerHieu.Instance.nailLayerController.EventNailRegistedNewType -= NailTypeLayer_Fuc;
        }
        Outline.gameObject.SetActive(false);
        spriteRenderer.transform.localPosition = new Vector3(0, 0, 0);
        spriteRenderer.transform.rotation = Quaternion.identity;
        spriteRenderer.transform.localScale = startScale;
      
        keyLayer = "";    
        keyLayerPre = "";
    }

    public void StartCreate()
    {
      
        startScale = spriteRenderer.transform.localScale;
     //   if (DataManagerHieu.Instance.CurrentCrew != null)
        {
            // spriteRenderer.sprite = DataManagerHieu.Instance.CurrentCrew;
            // spriteCrewFoot.color = DataManagerHieu.Instance.ColorCurrentCrew;
            // Outline.sprite = DataManagerHieu.Instance.CurrentCrew;
        }
       // spriteRenderer.transform.localScale = startScale;
    } 

    public void ActiveImageNail()
    {
        ActiveMask();
        ActiveNailEffect();
       // spriteRenderer.transform.localPosition = new Vector3(0,0.2f,0);
    }
    public void ResetImageNailWithParticle()
    {
   
        ParticleNailItem particleNailItem = ParticleNailSpawner.Instance._pool.Get();
        particleNailItem.transform.position = transform.position;
        ResetImageNail();
      
    }
   

    // dùng khi bắt đầu lựa chọn đinh, đổi board cha sang kinematic để tránh tạo tương tác vật lý không mong muốn
    public void KinematicBoardParent()
    {
        if (boardItemBeforeandAfter != null)
        {
            boardItemBeforeandAfter.rb.isKinematic = true;
            return;
        }
        if (listSlotBoardItem.Count == 0) return;
        HingeJoint2D hingle = listSlotBoardItem[0].hingeJointInSlot;
        if(hingle == null) return;
        boardItemBeforeandAfter = hingle.transform.GetComponent<Board_Item>();
        boardItemBeforeandAfter.rb.isKinematic = true;
    }

    public void testKinematicBoardParent()
    {
        if (boardItemBeforeandAfter != null)
        {
            
            boardItemBeforeandAfter.rb.isKinematic = false;
            boardItemBeforeandAfter = null;
            return;
        }
    }


    private bool checkInteractBoard;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Board"))
        {
            checkInteractBoard = true;
        }
    }   


    public void RegisterNailTypeLayer()
    {
        checkInteractBoard = false;
        ControllerHieu.Instance.nailLayerController.EventNailRegistedNewType += NailTypeLayer_Fuc;
    }

    private void NailTypeLayer_Fuc()
    {
        if (checkInteractBoard == true)
        {

        }
        else
        {
            if(gameObject.layer != 31)
            {
                gameObject.layer = 6;
            }
            ControllerHieu.Instance.nailLayerController.CheckRootNail(this);
            ControllerHieu.Instance.nailLayerController.EventNailRegistedNewType -= NailTypeLayer_Fuc;
        }
    }

    public void UnRegisterNailTypeLayer()
    {
        ControllerHieu.Instance.nailLayerController.EventNailRegistedNewType -= NailTypeLayer_Fuc;
    }
    [Button]
    public void ActiveNailEffect()
    {
        // if (GameManager.Instance._dataSave.isVibrate)
        // {
        //     Vibration.Vibrate();
        // }
       // SoundManager.Instance.PlaySound(AudioClipType.SFX_WOODIMPACT_1);
        if (_EffectNailCoroutine != null)
        {
            StopCoroutine(_EffectNailCoroutine);
            _EffectNailCoroutine = null;
        }
        spriteRenderer.transform.localPosition = Vector3.zero;     
        _EffectNailCoroutine = StartCoroutine(RotateAndMoveCoroutine());
    }
    IEnumerator RotateAndMoveCoroutine()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = spriteRenderer.transform.rotation;       
        Vector3 startLocalPosition = spriteRenderer.transform.localPosition;

        while (elapsedTime < 0.25f)
        {
            spriteRenderer.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0f, 0, -180), elapsedTime / 0.25f);
            spriteRenderer.transform.localScale = Vector3.Lerp(startScale, new Vector3(0.4f,0.4f,1), elapsedTime / 0.25f);
            spriteRenderer.transform.localPosition = Vector3.Lerp(startLocalPosition, new Vector3(0, 0.45f, 0), elapsedTime / 0.25f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.transform.rotation = Quaternion.identity;
        spriteRenderer.transform.localPosition = new Vector3(0, 0.45f, 0);
    }
    public void ResetImageNail()
    {
        // if (GameManager.Instance._dataSave.isVibrate)
        // {
        //     Vibration.Vibrate();
        // }
        //SoundManager.Instance.PlaySound(AudioClipType.SFX_WOODIMPACT_2);
        
        if (_EffectNailCoroutine != null)
        {
            StopCoroutine(_EffectNailCoroutine);
            _EffectNailCoroutine = null;
        }
        ForceBoard();
        _EffectNailCoroutine = StartCoroutine(ResetRotateAndMoveCoroutine());
    }
    private void ForceBoard()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D coliderObject in objetos)
        {            
            if (coliderObject.gameObject.CompareTag("Board"))
            {
                Board_Item boardItem = coliderObject.GetComponent<Board_Item>();
                if (boardItem.checkActive() == false)
                {
                    Rigidbody2D rb2D = coliderObject.GetComponent<Rigidbody2D>();
                    if (rb2D != null)
                    {
                        
                        rb2D.AddForce(rb2D.velocity.normalized * 5);
                    }
                }         
            }
        }
    }
    IEnumerator ResetRotateAndMoveCoroutine()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = spriteRenderer.transform.rotation;
        Vector3 resetStartScale = spriteRenderer.transform.localScale;
        Vector3 resetStartPosition = spriteRenderer.transform.localPosition;
  
        while (elapsedTime < 0.25f)
        {
            spriteRenderer.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0f, 0, 180), elapsedTime / 0.25f);
            spriteRenderer.transform.localScale = Vector3.Lerp(resetStartScale, startScale, elapsedTime / 0.25f);
            spriteRenderer.transform.localPosition = Vector3.Lerp(resetStartPosition, Vector3.zero, elapsedTime / 0.25f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.transform.localPosition = new Vector3(0, 0, 0);
        spriteRenderer.transform.localScale = startScale;
        spriteRenderer.transform.rotation = Quaternion.identity;
        DisActiveMask();
    }

}
