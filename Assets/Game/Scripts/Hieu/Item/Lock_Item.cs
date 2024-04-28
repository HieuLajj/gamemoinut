using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Lock_Item : MonoBehaviour, TInterface<Lock_Item>
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    //pool
    private ObjectPool<Lock_Item> _pool;
    public void SetPool(ObjectPool<Lock_Item> pool)
    {
        _pool = pool;
    }
    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Lock_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {
        spriteRenderer.transform.localPosition = Vector3.zero;
        Color currentColor = spriteRenderer.color;
        currentColor.a = 1f;
        spriteRenderer.color = currentColor;
    }

    public void StartCreate()
    {

    }

    public void ResetPool2()
    {
        ResetPool();
        //Controller.Instance.rootlevel.litslock.Remove(this);
        ControllerHieu.Instance.rootlevel.litslock_mydictionary.Remove(transform.position.ToString());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 31)
        {
           
            Slot_Item slot_Item = this.transform.parent.transform.GetComponent<Slot_Item>();
            if (slot_Item == null) return;
            slot_Item.hasLock = false;
            animator.SetTrigger("play");
        }
    }
}
