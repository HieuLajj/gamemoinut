using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Arrow;
    private Nail_Item nail_Item;
    public Slot_Item slot_Item;
    public GameObject textNotification;
   // private Coroutine coroutineMoveArroe;
   
    void Awake()
    {
   
        if (LevelController.Instance.LevelIDInt > 1)
        {
            Destroy(gameObject);
        }
     //   if (LevelController.Instance.LevelIDInt == 1)
        {
           
            LevelController.EventStartGame += Setup;
            LevelController.EventEndGame += DisSetup;      
        }
    //    LevelController.EventStartGame += SetupText;
      //  LevelController.EventEndGame += DisSetupText;
    }
    private void SetupText()
    {
        if(LevelController.Instance.LevelIDInt > 1)
        {
            LevelController.EventStartGame -= SetupText;
            LevelController.EventEndGame -= DisSetupText;
            Destroy(gameObject);          
        }
        textNotification.SetActive(true);
    }

    private void DisSetupText()
    {
        textNotification.SetActive(false);
    }

    private void Active1()
    {
        nail_Item.Outline.gameObject.SetActive(false);
        if (slot_Item == null)
        {
            slot_Item = ControllerHieu.Instance.rootlevel.litsslot_mydictionary.Values.FirstOrDefault(item => item != nail_Item.slot_item);
        }
        Vector3 screenPos = Camera.main.WorldToScreenPoint(slot_Item.transform.position + new Vector3(0, 0.75F, 0));
        //
        Arrow.transform.DOMove(screenPos, 0.5f);
       // Arrow.transform.position = screenPos;
    }

    private void Disactive1()
    {
    
        Vector3 screenPos = Camera.main.WorldToScreenPoint(nail_Item.transform.position + new Vector3(0, 0.75F, 0));
        if (Vector3.Distance(slot_Item.transform.position, nail_Item.transform.position) > 0.2)
        {
            nail_Item.Outline.gameObject.SetActive(true);
        }     
        Arrow.transform.DOMove(screenPos, 0.5f);
    }
    private void ActiveArrow()
    {
        Arrow.SetActive(true);
        nail_Item = ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Values.First();
        nail_Item.Outline.gameObject.SetActive(true);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(nail_Item.transform.position+ new Vector3(0,0.75F,0));   
        Arrow.transform.DOMove(screenPos, 0.5f);
        Slot_Item.EventActiveNail += Active1;
        Slot_Item.EventDisActiveNail += Disactive1;
    }

    private void Setup()
    {
        SetupText();
        slot_Item = null;
        Arrow.SetActive(false);
        Invoke("ActiveArrow", 0.15f);
    }

    private void DisSetup()
    {
        DisSetupText();
        Arrow.gameObject.SetActive(false);
        nail_Item.Outline.gameObject.SetActive(false);
        LevelController.EventStartGame -= Setup;
        LevelController.EventEndGame -= DisSetup;
        Slot_Item.EventActiveNail -= Active1;
        Slot_Item.EventDisActiveNail -= Disactive1;
        //SuGame.Get<SuAnalytics>().LogEvent(EventName.Tuto_Completed);    
    }

    private void OnDestroy()
    {
        LevelController.EventStartGame -= Setup;
        LevelController.EventEndGame -= DisSetup;
        Slot_Item.EventActiveNail -= Active1;
        Slot_Item.EventDisActiveNail -= Disactive1;
        LevelController.EventStartGame -= SetupText;
        LevelController.EventEndGame -= DisSetupText;
    }
}
