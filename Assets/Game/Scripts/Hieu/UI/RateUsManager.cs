using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsManager : MonoBehaviour
{
    private void OnEnable()
    {      
        StarsManager.onNewratingEvent += onNewratingEvent;

    }
    private void OnDisable()
    {
        StarsManager.onNewratingEvent -= onNewratingEvent;
    }
    void onNewratingEvent(int num)
    {
        //Debug.Log("ban dang an" + num);
    }
}
