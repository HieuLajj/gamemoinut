using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxTScaleAuto : MonoBehaviour
{
    public Transform transformText;
    public Vector3 originalScale;
    //private Tween tween;
    public void SetUp()
    {
       // transformText.transform.localScale = originalScale;
        //tween  = transformText.transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 1f)
        //    .SetEase(Ease.InOutQuad)
        //    .SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDisable()
    {
        //if(tween != null)
        //{
        //    tween.Kill();
        //}
        //transformText.transform.localScale = originalScale;
    }
}
