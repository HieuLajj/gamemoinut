using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackFeature : MonoBehaviour
{
    private static BackFeature instance;
    public static BackFeature Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BackFeature>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public Action EventBackFeature;
    public Action EventBackFeatureBoard;
    //public List<GameObject> listsBoardAwaitDeath = new List<GameObject>();
    [Button()]
    public void Active()
    {
        if (EventBackFeature == null) return;
        if (ControllPlayGame.Instance.targetNail != null)
        {
            ControllPlayGame.Instance.targetNail.ResetImageNail();
            ControllPlayGame.Instance.targetNail = null;
          
        }
        EventBackFeature?.Invoke();
    }
    public void UnsubscribeAll()
    {
        if (EventBackFeature != null)
        {
            EventBackFeatureBoard?.Invoke();
            foreach (Action handler in EventBackFeature.GetInvocationList())
            {              
                EventBackFeature -= handler;
            }
        }
    }
}
