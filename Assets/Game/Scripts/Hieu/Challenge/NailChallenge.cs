using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NailChallenge : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public ChallengeHole holeNail;
    public int id;
    private Coroutine _EffectNailCoroutine;
    public Vector3 startScale;
    private void Start()
    {
        startScale = spriteRenderer.transform.localScale;
    }
    [Button()]
    public void ResetImage()
    {
       
        spriteRenderer.transform.localPosition = new Vector3(0, 0, 0);
        if(startScale != Vector3.zero)
        {
            spriteRenderer.transform.localScale = startScale; 
        }
        spriteRenderer.transform.rotation = Quaternion.identity;
    }
    public void ResetImageNail()
    {
        //SoundManager.Instance.PlaySound(AudioClipType.SFX_WOODIMPACT_2);
        if (_EffectNailCoroutine != null)
        {
            StopCoroutine(_EffectNailCoroutine);
            _EffectNailCoroutine = null;
        }
        _EffectNailCoroutine = StartCoroutine(ResetRotateAndMoveCoroutine());
    }
    
    public void ActiveImageNail()
    {
        //SoundManager.Instance.PlaySound(AudioClipType.SFX_WOODIMPACT_1);
        if (_EffectNailCoroutine != null)
        {
            StopCoroutine(_EffectNailCoroutine);
            _EffectNailCoroutine = null;
        }
        spriteRenderer.transform.localPosition = Vector3.zero;
        spriteRenderer.transform.rotation = Quaternion.identity;
        _EffectNailCoroutine = StartCoroutine(RotateAndMoveCoroutine());
    }
    IEnumerator RotateAndMoveCoroutine()
    {
        spriteRenderer.sortingOrder = 1500;
        float elapsedTime = 0f;
        Quaternion startRotation = spriteRenderer.transform.rotation;
        Vector3 startLocalPosition = spriteRenderer.transform.localPosition;

        while (elapsedTime < 0.25f)
        {
            spriteRenderer.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0f, 0, -180), elapsedTime / 0.25f);
            spriteRenderer.transform.localScale = Vector3.Lerp(startScale, new Vector3(0.55f, 0.55f, 1), elapsedTime / 0.25f);
            spriteRenderer.transform.localPosition = Vector3.Lerp(startLocalPosition, new Vector3(0, 0.664f, 0), elapsedTime / 0.25f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.transform.rotation = Quaternion.identity;
        spriteRenderer.transform.localPosition = new Vector3(0, 0.664f, 0);
    }

    IEnumerator ResetRotateAndMoveCoroutine()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = spriteRenderer.transform.rotation;
        Vector3 resetStartScale = spriteRenderer.transform.localScale;
        Vector3 resetStartPosition = spriteRenderer.transform.localPosition;

        while (elapsedTime < 0.25f)
        {
            spriteRenderer.transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0f, 0, 180), elapsedTime / 0.25f);
            spriteRenderer.transform.localScale = Vector3.Lerp(resetStartScale, startScale, elapsedTime / 0.25f);
            spriteRenderer.transform.localPosition = Vector3.Lerp(resetStartPosition, Vector3.zero, elapsedTime / 0.25f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.transform.localPosition = new Vector3(0, 0, 0);
        spriteRenderer.transform.localScale = startScale;
        spriteRenderer.transform.rotation = Quaternion.identity;
        spriteRenderer.sortingOrder = 1200;
    }


}
