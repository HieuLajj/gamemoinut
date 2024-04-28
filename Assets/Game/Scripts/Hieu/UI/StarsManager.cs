using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StarsManager : MonoBehaviour
{
    public delegate void OnNewratingEventHandler(int index);
    public static event OnNewratingEventHandler onNewratingEvent;
    public GameObject[] stars;
    //void Awake()
    //{   
    //    for (int i = 0; i < stars.Length; i++)
    //    {
    //        SetColorAlpha(stars[i], 0f);
    //    }
    //}
    public void Setup()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            SetColorAlpha(stars[i], 0f);
        }
    }
    public void OnClicked(int num)
    {       
        for (int i = 0; i <= num; i++)
        {
            SetColorAlpha(stars[i], 1f);

        }

        for (int i = num + 1; i < stars.Length; i++)
        {
            SetColorAlpha(stars[i], 0f);
        }
        if (onNewratingEvent != null)
            onNewratingEvent(num);
    }

    public void Refresh()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            SetColorAlpha(stars[i], 0f);
        }
        if (onNewratingEvent != null)
            onNewratingEvent(-1);
    }

    void SetColorAlpha(GameObject obj, float alpha)
    {
        Color c = obj.GetComponent<Image>().color;
        c.a = alpha;
        obj.GetComponent<Image>().color = c;
    }
}
