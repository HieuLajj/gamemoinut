using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bg_Item : MonoBehaviour, TInterface<Bg_Item>
{
    //pool
    public SpriteRenderer spriteRenderer;
    private ObjectPool<Bg_Item> _pool;
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
        transform.DOShakePosition(duration: 0.5f, strength: new Vector3(0.05f, 0.05f, 0f), vibrato: 8, randomness: 90f, fadeOut: false);
    }
    public void SetPool(ObjectPool<Bg_Item> pool)
    {
        _pool = pool;
    }
    public void ResetPool()
    {
        _pool.Release(this);
    }
    public Bg_Item IGetComponentHieu()
    {
        return this;
    }
    public void ResetAfterRelease()
    {

    }
    public void StartCreate()
    {
        //spriteRenderer.sprite = DataManagerHieu.Instance.CurrentBoard;
    }
}
