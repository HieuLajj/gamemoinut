using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Locked : MonoBehaviour, TInterface<Locked>, IUIEffectItem
{
    private ObjectPool<Locked> _pool;
    public void SetPool(ObjectPool<Locked> pool)
    {
        _pool = pool;
    }

    public void ResetPool()
    {           
        gameObject.SetActive(false);
        _pool.Release(this);
    }

    public Locked IGetComponentHieu()
    {
        return this;
    }

    public void ResetAfterRelease()
    {

    }

    public void StartCreate()
    {     
        LevelController.Instance.ListUIEffectControll.Add((IUIEffectItem)this);
    }

    public static void CreateLocked(Vector3 _position)
    {    
        Locked locked = LockedSpawner.Instance._pool.Get();
        locked.transform.position = _position;      
    }
    public void ResetPool2()
    {
        LevelController.Instance.ListUIEffectControll.Remove((IUIEffectItem)this);
        gameObject.SetActive(false);
        _pool.Release(this);
    }
}
