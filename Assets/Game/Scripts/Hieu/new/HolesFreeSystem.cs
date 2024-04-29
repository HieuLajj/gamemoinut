using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolesFreeSystem : MonoBehaviour
{
    public Stack StackHoles;
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
    private void Start() {
        StackHoles = new Stack();
    }
    public void SetupNumbers(int numbers){
        for(int i=0; i<numbers; i++){
            HolesFree holefree = Instantiate(HoleFreePrefab,transform.position,Quaternion.identity,transform).GetComponent<HolesFree>();
            StackHoles.Push(holefree);
        }
    }
}
