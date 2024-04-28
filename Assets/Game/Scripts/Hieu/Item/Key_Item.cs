using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
using System.Linq;

public class Key_Item : MonoBehaviour, TInterface<Key_Item>
{
    //pool
    private ObjectPool<Key_Item> _pool;
    private bool checkRunning = false;
    private Tween moveTween;
    public void SetPool(ObjectPool<Key_Item> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {
        _pool.Release(this);
    }

    public Key_Item IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {
        checkRunning = false;
        if (moveTween != null)
        {
            moveTween.Kill(); 
            moveTween = null;
        }
    }

    public void StartCreate()
    {

    }

    public void FindLock(){    
        if (checkRunning == true || ControllerHieu.Instance.rootlevel.litslock_mydictionary.Count == 0) return;     
        Lock_Item lock_Item = ControllerHieu.Instance.rootlevel.litslock_mydictionary.First().Value;
        if (lock_Item == null){
            return;
        }
        checkRunning = true;
        moveTween = transform.DOMove(lock_Item.transform.position,2.0f).OnComplete(ResetKey);
    }
    private void ResetKey(){
            this.ResetPool();
            ControllerHieu.Instance.rootlevel.listkey.Remove(this);       
    }
}
