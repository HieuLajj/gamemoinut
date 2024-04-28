using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameIn1 : MonoBehaviour
{
    public Transform SettingPanel;
    public Transform RatePanel;
    private static CanvasGameIn1 instance;
    public static CanvasGameIn1 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CanvasGameIn1>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private void Awake() {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Back()
    {
        ControllerHieu.Instance.TimeRemotetoController(1);
    }
}
