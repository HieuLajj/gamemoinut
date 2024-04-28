
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonFunction : MonoBehaviour
{
    public void ClickFuction()
    {
       
        switch (gameObject.name)
        {           
            case "SettingBtn":
                Slot_Item.flag = 1;
                SettingBtn();
                break;            
            case "PullNailBtn":
                PullNailBtn();
                break;
            case "ReplayBtn":            
                ResetBtn();
                break;
            case "HammerBtn":
                HammerBtn();
                break;
            //case "BackBtn":
            //    BackBtn();
            //    break;
        }
    }

 
    private void HammerBtn()
    {
        Hammer.Instance.Active();
    }

    private void BackBtn()
    {
        BackFeature.Instance.Active();
    }


    private void SettingBtn(){
        ControllerHieu.Instance.TimeRemotetoController(0);
      
    }
    

    private void ResetBtn()
    {   
        LevelController.Instance.ResetLevel();
    }
    private void PullNailBtn()
    {
        NailPullFeature.Instance.SetupFeature();
    }
}
