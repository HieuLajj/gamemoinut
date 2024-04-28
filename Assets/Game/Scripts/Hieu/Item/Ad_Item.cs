using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Ad_Item : MonoBehaviour, TInterface<Ad_Item>
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    //pool
    private ObjectPool<Ad_Item> _pool;
    public void SetPool(ObjectPool<Ad_Item> pool)
    {
        _pool = pool;
    }
    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Ad_Item IGetComponentHieu()
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
        ControllerHieu.Instance.rootlevel.listad.Remove(this);
    }
}
