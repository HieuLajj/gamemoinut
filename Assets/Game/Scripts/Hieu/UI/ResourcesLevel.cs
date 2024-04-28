using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLevel : MonoBehaviour
{
    private static ResourcesLevel instance;
    public static ResourcesLevel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ResourcesLevel>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;         
        }    
    }
    public Sprite[] spriteBackgroudLevel;
    public Sprite[] statusLevelButtonLevel;

}
