using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Board_Item : MonoBehaviour, TInterface<Board_Item>
{
    public List<Nail_Item> listnail;
    public Material materialBoard;
   
    public Transform[] positionAnchor;
    //pool
    private ObjectPool<Board_Item> _pool;
    public List<Slot_board_Item> listslot;
    public SpriteRenderer spritemain;
    public Rigidbody2D rb;
    public bool checkDestroy;
    public Board boardinfomation;
    public Vector3 rotationPre;
    private Vector3 savepre_Position;
    private Quaternion savepre_Rotation;
    private bool savepre_checkDestroy;
   
    private void Awake()
    {      
        rb = transform.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Hammer.Instance.ActionHammer += Shake;
    }

    private void OnDisable()
    {
        Hammer.Instance.ActionHammer -= Shake;
    }
    private void Shake()
    {  
          spritemain.transform.DOShakeRotation(duration: 1f, strength: new Vector3(0f, 0f, 2f), vibrato: 8, randomness: 90f);
    }
    public int GetLayer()
    {
        return spritemain.sortingLayerID;
    }

    public void AddSlotforBoard(Slot_board_Item slot_board)
    {   
        if(slot_board != null)
        {
            slot_board.mask.frontSortingOrder = spritemain.sortingOrder;
            slot_board.mask.backSortingOrder = spritemain.sortingOrder-1;
        }
        listslot.Add(slot_board);
    }

    public void SetupRb()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void NotRb()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = 0; 
    }

    public void SetPool(ObjectPool<Board_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Board_Item IGetComponentHieu()
    {
        return this;
    }
    public void ResetAfterRelease()
    {
        NotRb();
    }

    public void StartCreate()
    {
        HingeJoint2D[] hingeJoints = gameObject.GetComponents<HingeJoint2D>();

        foreach (HingeJoint2D hingeJoint in hingeJoints)
        {
            Destroy(hingeJoint);
        }

        foreach (Slot_board_Item slot_board_item in listslot)
        {
            slot_board_item.ResetPool();
        }
        listslot.Clear();
        listnail.Clear();
        NotRb();
        checkDestroy = false;
    }

    public void WhenTriggerandBoardCheck()
    {
        //Controller.Instance.rootlevel.listboard.Remove(this);
        //ResetPool();
        if (checkDestroy) return;
        checkDestroy = true;
        LevelController.Instance.NumberBoardDestroy++;

        //rb.isKinematic = true;
        //rb.angularVelocity = 0;
        //rb.velocity = Vector2.zero;

        LevelController.Instance.CheckAfterKillBoard();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 31){
            Key_Item key_Item = other.transform.GetComponent<Key_Item>();

            if(key_Item != null)
            {
                key_Item.FindLock();
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Board")){

    //        AudioController.Instance.PlayClip("vacham");
    //    }
    //    else if (collision.gameObject.CompareTag("Nail"))
    //    {
    //        AudioController.Instance.PlayClip("vachamdinh");
    //    }     
    //}

    public Slot_board_Item FindOtherSlotBoard(Slot_board_Item a){
    
        int Index = 0;

        for(int i = 0; i < listslot.Count; i++)
        {
            if(listslot[Index] == a)
            {
                break;
            }
        }       
        return listslot[Index];
    }

    public virtual void DetermineCenterPoint(Slot_board_Item a){

    }
    public bool CompareVectors()
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.x - boardinfomation.rot.x) <=0.1f && Mathf.Abs(transform.rotation.eulerAngles.y - boardinfomation.rot.y) <= 0.1f && Mathf.Abs(transform.rotation.eulerAngles.z - boardinfomation.rot.z) <= 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

     public virtual void AutoRotate(Slot_board_Item slotboardItem, Slot_board_Item findanySlotBoardIteminboard, Slot_Item slotItem)
     {
       // Slot_board_Item parentt = FindOtherSlotBoard(slotboardItem);
        Vector2 dirboard = findanySlotBoardIteminboard.transform.position - slotItem.transform.position;
        Vector2 dir3 = findanySlotBoardIteminboard.transform.position - slotboardItem.transform.position;
        findanySlotBoardIteminboard.transform.SetParent(this.transform.parent);
        this.transform.SetParent(findanySlotBoardIteminboard.transform);

        Vector3 axis = Vector3.Cross(dir3, dirboard);
        float angle = Vector3.Angle(dir3, dirboard);
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        findanySlotBoardIteminboard.transform.rotation = rotation * findanySlotBoardIteminboard.transform.rotation;

        this.transform.SetParent(findanySlotBoardIteminboard.transform.parent);
        findanySlotBoardIteminboard.transform.SetParent(transform);
     }

    public virtual bool Define_intersection(Vector3 positionM)
    {
        return true;
    }
    public void RegisterEventBack()
    {
        BackFeature.Instance.EventBackFeature += ActiveFeatureback;
        BackFeature.Instance.EventBackFeatureBoard += RegisDestroyAfterBack;
        SaveInformation();
    }

    public void UnRegisterEventBack()
    {
        BackFeature.Instance.EventBackFeature -= ActiveFeatureback;
        BackFeature.Instance.EventBackFeatureBoard -= RegisDestroyAfterBack;
    }

    public void RegisDestroyAfterBack()
    {
        if (checkDestroy)
        {
            gameObject.SetActive(false);
        }
    }
    public void ActiveFeatureback()
    {
        rb.isKinematic = true;
        foreach (Slot_board_Item slotboardItem in listslot)
        {
            slotboardItem.ActiveSaveInformation();
        }
        transform.position = savepre_Position;
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        transform.rotation = savepre_Rotation;
        rb.isKinematic = false;
       
        if (checkDestroy && savepre_checkDestroy == false)
        {    
            LevelController.Instance.NumberBoardDestroy--;
        }
        checkDestroy = savepre_checkDestroy;
        UnRegisterEventBack();
    }
    public void SaveInformation()
    {
        foreach(Slot_board_Item slotboardItem in listslot)
        {
            slotboardItem.SaveInformation();
        }
        savepre_Position = transform.position;
        savepre_Rotation = transform.rotation;
        savepre_checkDestroy = checkDestroy;
    }

    public bool CheckHigle()
    {
        foreach(Slot_board_Item slotlit in listslot)
        {
            if(slotlit.hingeJointInSlot.enabled == true)
            {
                return true;
            }
        }
        return false;
    }
    [Button()]
    public void ThunderDestroy(int m)
    {
        Material materialThunder = ControllerHieu.Instance.GetMaterinalThunder();
        materialThunder.SetTexture("_SubTexture", materialBoard.GetTexture("_MainSubText"));
        materialBoard = materialThunder;
        spritemain.material = materialBoard;
        LightController.Instance.CallThunder(transform.position,m);
        //SoundManager.Instance.PlaySound(AudioClipType.SFX_LIGHTNING_STRIKE);
        StartCoroutine(Vanish(m));
    }


    private IEnumerator Vanish(int m)
    {
        float elapsedTime = 0f;
        while(elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            float lerpedDissolve = Mathf.Lerp(1, 0f, (elapsedTime / 2));    
            materialBoard.SetFloat("_DissoleAmount", lerpedDissolve);
            yield return null;
        }
        gameObject.SetActive(false);
        LevelController.Instance.NumberBoardDestroy++;
        savepre_checkDestroy = true;
        checkDestroy = true;
        for(int i=0; i<listnail.Count; i++) {    
            LoadDataBase.Instance.CreatePhysic2dforboard_layersub_3(listnail[i]);
        }
        LightController.Instance.DisActiveThunder(m);
    }

    public bool checkActive()
    {
        foreach (Slot_board_Item slot_Board_Item in listslot)
        {
            if (slot_Board_Item.hingeJointInSlot.enabled)
            {
                return true;
            }
        } 
        return false;
    }
}
