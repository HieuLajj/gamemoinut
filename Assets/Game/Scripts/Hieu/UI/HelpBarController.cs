using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBarController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ButtonReplay;
    public RectTransform rectTransform;
    private void OnEnable()
    {
        LevelController.EventStartGame += Setup;
    }

    private void OnDisable()
    {
        LevelController.EventStartGame -= Setup;
    }

   
    private void Setup()
    {
        if (LevelController.Instance.LevelDifficule)
        {
            ButtonReplay.SetActive(true);
            rectTransform.sizeDelta = new Vector2(753, 229);

        }
        else
        {
            ButtonReplay.SetActive(false);
            rectTransform.sizeDelta = new Vector2(603, 229);
        }
    }
}
