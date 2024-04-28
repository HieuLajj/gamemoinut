using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class EffectBlur : MonoBehaviour
{
    public Image imageBlur;
    private Color targetColor;
    public Action ActionAwaitEffect;
   
    private void Awake()
    {
        targetColor = imageBlur.color;
    }

    private void OnEnable()
    {
        targetColor.a = 0f;
        imageBlur.color = targetColor;
        targetColor.a = 1f;
        imageBlur.DOColor(targetColor, 0.75f).OnComplete(LengthSleepDisGameObject);
    }
    private void LengthSleepDisGameObject()
    {
        Invoke("DisGameObject", 0.35f);
    }
    private void DisGameObject()
    {
        ActionAwaitEffect?.Invoke();   
        {
            targetColor.a = 0f;
            imageBlur.DOColor(targetColor, 0.35f).OnComplete(DisActive);
        }
    }
    private void DisActive()
    {
        gameObject.SetActive(false);
    }
}
