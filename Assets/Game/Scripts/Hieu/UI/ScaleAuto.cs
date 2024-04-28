using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAuto : MonoBehaviour
{
    public Transform transformText;
    private Vector3 originalScale;
    private Tween tween;
    //private void OnEnable()
    //{     
    //    originalScale = transformText.transform.localScale;
    //    tween = transformText.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1.25f)
    //       .SetEase(Ease.InOutQuad)
    //       .SetLoops(-1, LoopType.Yoyo);
    //}

    //private void OnDisable()
    //{
    //    if (tween != null)
    //    {
    //        tween.Kill();
    //    }
    //    transformText.transform.localScale = originalScale; 
    //}
}
