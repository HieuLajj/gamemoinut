using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LightController : MonoBehaviour
{
    public GameObject GlobalVolumn;
    public ParticleSystem[] particleThunder;
    private static LightController instance;
    public static LightController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LightController>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public void CallThunder(Vector3 end,int m)
    {
        particleThunder[m].transform.position = end;
        particleThunder[m].gameObject.SetActive(true);
        particleThunder[m].Play();   
    }

    public void DisActiveThunder(int m)
    {
        particleThunder[m].gameObject.SetActive(false);
    }

    public void DisActiveGlobalVolumn()
    {
        GlobalVolumn.SetActive(false);
    }

    public void ActiveGlobalVolumn()
    {
        GlobalVolumn.SetActive(true);
        Invoke("DisActiveGlobalVolumn", 3.5f);
    }
}
