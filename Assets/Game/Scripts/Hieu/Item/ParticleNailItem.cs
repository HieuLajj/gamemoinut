using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticleNailItem : MonoBehaviour, TInterface<ParticleNailItem>
{
    private ObjectPool<ParticleNailItem> _pool;
    public void SetPool(ObjectPool<ParticleNailItem> pool)
    {
        _pool = pool;
    }
    public void ResetPool()
    {
        _pool.Release(this);
    }
    public ParticleNailItem IGetComponentHieu()
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
