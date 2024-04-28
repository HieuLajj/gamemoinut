using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System;
public class NailSpawner : SpawnerHieu<NailSpawner,Nail_Item,Nail_Item>
{
//     private static NailSpawner instance;
//     public static NailSpawner Instance
//     {
//         get
//         {
//             if (instance == null)
//             {
//                 instance = FindObjectOfType<NailSpawner>();
//             }
//             return instance;
//         }
//         private set
//         {
//             instance = value;
//         }
//     }

//     public ObjectPool<Nail_Item> _pool_nail_item;
//     public Nail_Item nail_ItemPrefab;
//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(this);
//             AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(AddressAbleStringEdit.URLAddress("nail"));
//             asyncOperationHandle.Completed += (handle) =>
//             {
//                 if (handle.Status == AsyncOperationStatus.Succeeded)
//                 {
//                     Debug.Log("?????????????????1");
//                     Controller.Instance.LoadDataIndex++;
//                     nail_ItemPrefab = handle.Result.GetComponent<Nail_Item>();
//                 }
//             };
//             Debug.Log("?????????????????2");
//             _pool_nail_item = new ObjectPool<Nail_Item>(CreateNailItem, OnTakeNailItemFromPool, OnReturnNailItemToPool, OnDestroyNailItem, true, 1000, 2000);
//         }
//         else
//         {
//             Destroy(this);
//         }
//     }

//     private void Start()
//     {

//     }

//     private Nail_Item CreateNailItem()
//     {
//         Nail_Item nail_Item = null;
//         if (nail_ItemPrefab != null)
//         {
//             Debug.Log("tao moi nail");
//             nail_Item = Instantiate(nail_ItemPrefab).GetComponent<Nail_Item>();
//             nail_Item.SetPool(_pool_nail_item);
//         }
//         return nail_Item;
//     }

//     private void OnTakeNailItemFromPool(Nail_Item nail_Item)
//     {
//         if (nail_Item == null)
//         {
//            return;
//         }
//         nail_Item.gameObject.SetActive(true);
//     }

//     private void OnReturnNailItemToPool(Nail_Item nail_Item)
//     {
//         if (nail_Item == null) return;
//         nail_Item.transform.parent = Controller.Instance.transform;
//         nail_Item.gameObject.SetActive(false);
//         //LevelController.Instance.rootlevel.litsnail.Remove(nail_Item);
//     }

//     private void OnDestroyNailItem(Nail_Item nail_Item)
//     {
//         if (nail_Item == null) return;    
//         Destroy(nail_Item.gameObject);
//     }
}
