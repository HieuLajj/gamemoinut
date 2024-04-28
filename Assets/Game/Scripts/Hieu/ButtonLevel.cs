using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum StateButtonLevel
{
    Active, Await
}
public class ButtonLevel : MonoBehaviour
{
    //public static string LevelPlaying="";
    //public TextMeshProUGUI textLevel;
    //public Image backgroudImage;
    //public Image statusImage;
    //public StateButtonLevel state = StateButtonLevel.Await;
    //private int level;
    //public int Level
    //{
    //    set
    //    {
    //        level = value;
    //        textLevel.text = level.ToString();
    //    }
    //    get
    //    {
    //        return level;
    //    }
    //}
    //public void SetupButtonLevel(int _level)
    //{
    //    Level = _level;
    //    SetUp();
    //}
    //public void ClickLevel()
    //{    
    //    if(state == StateButtonLevel.Await) {
    //        Debug.Log("chua den level nay");
    //        return;
    //    }
    //    Controller.Instance.LevelIDInt = level;
    //    Controller.Instance.StartLevel();
    //}
    //public static string GetLevelString()
    //{
    //    LevelPlaying = $"Assets/_GameAssets/data_{Controller.Instance.LevelIDInt}.json";
    //    return $"Assets/_GameAssets/data_{Controller.Instance.LevelIDInt}.json";
    //}
    //public static string GetLevelDacbietString(){
    //    LevelPlaying = $"Assets/_GameAssets/data_b_{Controller.Instance.LevelIDInt}.json";
    //    return $"Assets/_GameAssets/data_b_{Controller.Instance.LevelIDInt}.json";
    //}

    //public void SetUp()
    //{
    //    if (Level-1 < PlayerPrefs.GetInt("Playinglevel"))
    //    {
    //        Actived();
    //        state = StateButtonLevel.Active;
    //    }
    //    else if (Level-1 == PlayerPrefs.GetInt("Playinglevel"))
    //    {
    //        Activing();
    //        state = StateButtonLevel.Active;
    //    }
    //    else
    //    {
    //        AwaitActive();
    //        state = StateButtonLevel.Await;
    //    }
    //}

    //public void Actived()
    //{
    //    backgroudImage.sprite = ResourcesLevel.Instance.spriteBackgroudLevel[0];
    //    statusImage.sprite = ResourcesLevel.Instance.statusLevelButtonLevel[0];
    //    statusImage.enabled = true;
    //}

    //public void Activing()
    //{
    //    backgroudImage.sprite = ResourcesLevel.Instance.spriteBackgroudLevel[1];
    //    statusImage.enabled = false;
    //}

    //public void AwaitActive()
    //{ 
    //    backgroudImage.sprite = ResourcesLevel.Instance.spriteBackgroudLevel[2];
    //    statusImage.sprite = ResourcesLevel.Instance.statusLevelButtonLevel[1];
    //    statusImage.enabled = true;
    //}
}
