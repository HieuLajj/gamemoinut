using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;

public class SpawnerHieu<T,U,X> : MonoBehaviour where T : MonoBehaviour where U : MonoBehaviour,TInterface<X> where X : MonoBehaviour
{
  
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public ObjectPool<X> _pool;
    public X _poolItemPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this.GetComponent<T>();
            DontDestroyOnLoad(this);
          
            _pool = new ObjectPool<X>(CreatePoolItem, OnTakePoolItemFromPool, OnReturnPoolItemToPool, OnDestroyPoolItem, true, 150, 150);
        }
        else
        {
            Destroy(this);
        }
    }

    
    private X CreatePoolItem()
    {
       
        U u = Instantiate(_poolItemPrefab).GetComponent<U>();
        X pool_Item = u.IGetComponentHieu();
        u.SetPool(_pool);
        return pool_Item;
    }

    private void OnTakePoolItemFromPool(X pool_Item)
    {
        if (pool_Item == null)
        {
           return;
        }
        pool_Item.gameObject.SetActive(true);
        pool_Item.GetComponent<U>().StartCreate();
    }

    private void OnReturnPoolItemToPool(X pool_Item)
    {
        if (pool_Item == null) return;
        pool_Item.transform.parent = ControllerHieu.Instance.transform;
        pool_Item.gameObject.SetActive(false);
        pool_Item.GetComponent<U>().ResetAfterRelease();
        //LevelController.Instance.rootlevel.litsnail.Remove(nail_Item);
    }

    private void OnDestroyPoolItem(X pool_Item)
    {
        if (pool_Item == null) return;    
        Destroy(pool_Item.gameObject);
    }
}
