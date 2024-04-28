using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolesFreeSystem : MonoBehaviour
{
    private static HolesFreeSystem instance;
    public static HolesFreeSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HolesFreeSystem>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public GameObject HoleFreePrefab;
    public void SetupNumbers(int numbers){
        for(int i=0; i<numbers; i++){
            Instantiate(HoleFreePrefab,transform.position,Quaternion.identity,transform);
        }
    }
}
