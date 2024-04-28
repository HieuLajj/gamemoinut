using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoDisplayerParticle : MonoBehaviour
{
    // Reference to the particle system
    new ParticleSystem particleSystem;
    public ParticleNailItem particleNail;

    void Awake()
    {
        // Get the reference to the particle system
        particleSystem = GetComponent<ParticleSystem>();

    }

    private void OnEnable()
    {
        StartCoroutine(DisalbeOjbect());
    }

    IEnumerator DisalbeOjbect()
    {
        yield return new WaitForSeconds(particleSystem.main.duration);
        particleNail.ResetPool();
    }

}