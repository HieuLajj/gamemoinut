using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textLevel;
    public Image ImageCicle1;
    public Image ImageCicle2;
    public Image ImageSub1;
    public Sprite[] ImagesCicle;
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
            ImageCicle2.sprite = ImagesCicle[0];
            ImageCicle1.sprite = ImagesCicle[1];
            ImageCicle1.transform.localScale = new Vector2(0.75f, 0.75f);
            ImageSub1.gameObject.SetActive(true);
        }
        else
        {
            ImageCicle2.sprite = ImagesCicle[1];
            ImageCicle1.sprite = ImagesCicle[0];
            ImageCicle1.transform.localScale = new Vector2(1f, 1f);
            ImageSub1.gameObject.SetActive(false);
        }
    }
}

