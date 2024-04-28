using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    private Coroutine notifiCoroutine;
    private bool checknotifiCoroutine;
    public TextMeshProUGUI textnotifiMeshProUGUI;
    private static Notification instance;
    public static Notification Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Notification>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public void Awake()
    {
        LevelController.EventEndGame += SetUp;
    }
    private void SetUp()
    {
        if (checknotifiCoroutine)
        {
            textnotifiMeshProUGUI.gameObject.SetActive(false);
            if(notifiCoroutine!= null)
            {
                StopCoroutine(notifiCoroutine);
            }
        }
    }
    public void CallNotificaiton(string text, Color color)
    {
        textnotifiMeshProUGUI.color = color;
        textnotifiMeshProUGUI.text = text;
        notifiCoroutine = StartCoroutine(NotifiOutmove());
    }

    private IEnumerator NotifiOutmove()
    {

        checknotifiCoroutine = true;
        textnotifiMeshProUGUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        textnotifiMeshProUGUI.gameObject.SetActive(false);
        checknotifiCoroutine = false;
    }
}
