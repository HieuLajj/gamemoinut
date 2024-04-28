using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NailPullFeature : MonoBehaviour
{
    //public Material materialOutlineNail;
    private static NailPullFeature instance;
    public static NailPullFeature Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NailPullFeature>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public bool check = false;
    //private void Awake()
    //{
    //    materialOutlineNail.SetFloat("_Active", 0);
    //}
    public void SetupFeature()
    {
        check = true;
        if (ControllPlayGame.Instance.targetNail != null)
        {
            ControllPlayGame.Instance.targetNail.ResetImageNail();
          
            ControllPlayGame.Instance.targetNail = null;
        }
        
        foreach (Nail_Item nailItem in ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Values)
        {
            if (nailItem.gameObject.activeInHierarchy)
            {
                nailItem.Outline.gameObject.SetActive(true);
            }
        }
    }

    public void NotSetupFeature()
    {
        check = false;
        foreach (Nail_Item nailItem in ControllerHieu.Instance.rootlevel.litsnail_mydictionary.Values)
        {
            if (nailItem.gameObject.activeInHierarchy)
            {
                nailItem.Outline.gameObject.SetActive(false);
            }
        }
    }
}
