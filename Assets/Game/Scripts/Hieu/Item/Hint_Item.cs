using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class Hint_Item : MonoBehaviour, TInterface<Hint_Item>
{
    //pool
    private ObjectPool<Hint_Item> _pool;
    public TextMeshProUGUI textIdHint;
    public void SetUpTextIdHint(string str)
    {
        textIdHint.text = str;
    }

    public void SetPool(ObjectPool<Hint_Item> pool)
    {
        _pool = pool;
    }
    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Hint_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {

    }

    public void StartCreate()
    {

    }
}
