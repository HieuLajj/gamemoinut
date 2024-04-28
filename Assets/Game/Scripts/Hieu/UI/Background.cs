using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Background : MonoBehaviour
{

    public static string statusbg;

    public Image imageBackground;
    private Sprite spriteDacbiet;
    private Sprite spritenotDacbiet;
    public void Notdacbiet()
    {
        if(spritenotDacbiet == null)
        {
            LoadSprite("notdacbiet");
            statusbg = "notdacbiet";
        }
        else
        {
            imageBackground.sprite = spritenotDacbiet;
        }
    }

    public void Dacbiet()
    {
        if(spriteDacbiet == null)
        {
            LoadSprite("dacbiet");
            statusbg = "dacbiet";
        }
        else
        {
            imageBackground.sprite = spriteDacbiet;
        }
    }

    public void LoadSprite(string clipstring)
    {
        //string str = AddresSpriteString(clipstring);
        //if (str == "") { return;}
        //AsyncOperationHandle<Sprite> asyncOperationHandle11 = Addressables.LoadAssetAsync<Sprite>(str);     
        //asyncOperationHandle11.Completed += (handle) =>
        //{
        //    if (handle.Status == AsyncOperationStatus.Succeeded)
        //    {
        //        if(clipstring == "dacbiet")
        //        {
        //            spriteDacbiet = handle.Result;
        //        }
        //        else
        //        {
        //            spritenotDacbiet = handle.Result;
        //        }
        //        imageBackground.sprite = handle.Result;
        //    }
        //};
    }
    
    private string AddresSpriteString(string str)
    {
        string h = "";
        switch (str)
        {
            case "dacbiet":
                h = "Assets/Sprites/Background/BG_2.png";
                break;
            case "notdacbiet":
                h = "Assets/Sprites/Background/BG_3.png";
                break;
        }
        return h;
    }
}
