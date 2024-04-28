using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Slot_board_Item : MonoBehaviour, TInterface<Slot_board_Item>
{
    private ObjectPool<Slot_board_Item> _pool;
    public SpriteMask mask;
    public HingeJoint2D hingeJointInSlot;
    public SpriteRenderer spriteBorder;
    private bool savepre_hingeJointInSlot;

    public void SetHingeJointInSlotBool(bool setbool)
    {
        hingeJointInSlot.enabled  = setbool;
        if (!setbool)
        {       
            mask.transform.parent.gameObject.SetActive(true);
        }
    }
    public void SetMaskInSlotBool()
    {
       
         mask.transform.parent.gameObject.SetActive(false);
        
    }
    public void SetPool(ObjectPool<Slot_board_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Slot_board_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {       
        hingeJointInSlot = null;
    }

    public void StartCreate()
    {
        mask.transform.parent.gameObject.SetActive(true);
    }

    public void SaveInformation()
    {
        savepre_hingeJointInSlot = hingeJointInSlot.enabled;
    }

    public void ActiveSaveInformation()
    {
        //hingeJointInSlot.enabled = savepre_hingeJointInSlot;
        SetHingeJointInSlotBool(savepre_hingeJointInSlot);
    }
}
