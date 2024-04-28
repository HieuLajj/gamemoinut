using Sirenix.OdinInspector;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hammer : MonoBehaviour
{
    public SkeletonGraphic skeletonAnimation;
    public float Radius = 20;
    public float ForceInt = 500;
    public GameObject animationHammer;
    private static Hammer instance;
    public static Hammer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Hammer>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

   // private float _shockWaveTime = 3f;
  
   // public Material _material;
   
  //  private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");
    public Action ActionHammer;

    [Button()]
    public void Active()
    {
        animationHammer.SetActive(true);
        skeletonAnimation.AnimationState.SetAnimation(0, "animation", false);
        StartCoroutine("ActiveHammer");
    }
    
    IEnumerator ActiveHammer()
    {
        yield return new WaitForSeconds(0.85f);
      //  SoundManager.Instance.PlaySound(AudioClipType.SFX_HAMMER_SLAM);
        yield return new WaitForSeconds(0.5f);

        //  _shockWaveCoroutine = StartCoroutine(ShockWaveAction(0.065f, 1f));
        ActionHammer?.Invoke();
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, Radius);
        foreach (Collider2D coliderObject in objetos)
        {
            if (coliderObject.gameObject.CompareTag("Board"))
            {
                Rigidbody2D rb2D = coliderObject.GetComponent<Rigidbody2D>();
                if (rb2D != null)
                {
                    Vector2 direction = coliderObject.transform.position - transform.forward;
                    float distancia = 1 + direction.magnitude;
                    float fuerzaFinal = ForceInt / distancia;
                    rb2D.AddForce(direction * fuerzaFinal);
                }
            }
        }
        animationHammer.SetActive(false);
    }

    //private IEnumerator ShockWaveAction(float startPos, float endPos)
    //{
    //    EffectShockwave.SetActive(true);
    //    _material.SetFloat(_waveDistanceFromCenter, startPos);
    //    float lerpedAmount = 0f;
    //    float elapsedTime = 0f;
    //    while(elapsedTime < _shockWaveTime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / _shockWaveTime));
    //        _material.SetFloat(_waveDistanceFromCenter, lerpedAmount);
    //        yield return null;
    //    }
    //    EffectShockwave.SetActive(false);
    //}

}
