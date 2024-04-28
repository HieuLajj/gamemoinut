using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WarningImage : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 0.75f;
    private Coroutine coroutine;

    private bool fadeIn = true;
    // public static int checkWarning;
    private Color color = Color.white;
   
    private void OnEnable()
    {
      //  checkWarning = 0;
        coroutine = StartCoroutine(FadeImage());
    }

    public void StopWarning()
    {
        color.a = 0;
        StopCoroutine(coroutine);
        image.color = color;
    }

    private void OnDisable()
    {
       StopCoroutine(coroutine);
    }
    IEnumerator FadeImage()
    {
       

        while (true)
        {
            float timer = 0;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;

                if (fadeIn)
                {
                    color.a = Mathf.Lerp(0, 1, timer / fadeDuration);
                }
                else
                {
                    color.a = Mathf.Lerp(1, 0, timer / fadeDuration);
                }

                image.color = color;
                yield return null;
            }

            fadeIn = !fadeIn;
        }
    }
}
