using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    public string[] textLoading;
    public TextMeshProUGUI textLoad;
    private void OnEnable()
    {
        StartCoroutine(LoopString());
    }
    IEnumerator LoopString()
    {
        int i = 0;
        while (true)
        {
            i++;
            if (i >= textLoading.Length)
            {
                i = 0;
            }
            textLoad.text = textLoading[i];
            yield return new WaitForSeconds(0.2f);
        }
    }
}
