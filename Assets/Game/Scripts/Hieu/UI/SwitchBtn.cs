using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBtn : MonoBehaviour
{
    public Sprite[] sprites;
    public Image imageView;
    public bool checkActive = false;
    public RectTransform rectTransform;
    private void Start()
    {
        switch (gameObject.name)
        {
            case "SFXButton":
                if(SettingPanelUI.SoundCheck == 0)
                {
                    imageView.sprite = sprites[1];
                    rectTransform.anchoredPosition = new Vector2(-57, -7.5f);
                 
                }
                else
                {
                    imageView.sprite = sprites[0];
                    rectTransform.anchoredPosition = new Vector2(57, -7.5f);
                  
                }
                break;
            case "MusicButton":
                if (SettingPanelUI.MusicCheck == 0)
                {
                    imageView.sprite = sprites[1];
                    rectTransform.anchoredPosition = new Vector2(-57, -7.5f);

                }
                else
                {
                    imageView.sprite = sprites[0];
                    rectTransform.anchoredPosition = new Vector2(57, -7.5f);
                }
                break;
        }
    }
    public void ClickButton()
    {
        checkActive = !checkActive;
        if (checkActive)
        {
            imageView.sprite = sprites[0];
            rectTransform.anchoredPosition = new Vector2(57, -7.5f);
            switch (gameObject.name)
            {
                case "SFXButton":
                    SettingPanelUI.SoundCheck = 1;
                    PlayerPrefs.SetInt("Sound", 1);
                    break;
                case "MusicButton":
                    SettingPanelUI.MusicCheck = 1;
                    PlayerPrefs.SetInt("Music", 1);
                    break;
            }
        }
        else
        {
            imageView.sprite = sprites[1];
            rectTransform.anchoredPosition = new Vector2(-57, -7.5f);
            switch (gameObject.name)
            {
                case "SFXButton":
                    SettingPanelUI.SoundCheck = 0;
                    PlayerPrefs.SetInt("Sound", 0);
                    break;
                case "MusicButton":
                    SettingPanelUI.MusicCheck = 0;
                    PlayerPrefs.SetInt("Music", 0);
                    break;
            }
        }
    }

}
