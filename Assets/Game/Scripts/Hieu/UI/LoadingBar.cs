
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class LoadingBar : MonoBehaviour
{
    public Image ladingBar;
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }
    IEnumerator LoadAsyncOperation()
    {
        float elapsedTime = 0;
        while (elapsedTime < 3f)
        {
            elapsedTime += Time.deltaTime;
            ladingBar.fillAmount = Mathf.Clamp01(elapsedTime / 3f);
            yield return null;
        }
        //while (Controller.Instance.LoadDataIndex < 17 && Controller.Instance.LoadDataIndex >= 0)
        //{
        //    yield return 0;
        //}
        SceneManager.LoadSceneAsync("GamePlay");
    }
}
