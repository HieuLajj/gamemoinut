using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public interface TInterface<X> where X : MonoBehaviour
{
    
    void SetPool(ObjectPool<X> pool);
    X IGetComponentHieu();
    void ResetAfterRelease();
    void StartCreate();
}
