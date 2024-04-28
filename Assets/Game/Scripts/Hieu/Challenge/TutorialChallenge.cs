using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChallenge : MonoBehaviour
{  
    public void Start()
    {
        //SoundManager.Instance.PlaySound(AudioClipType.SFX_UI_POPUP);
        Timer.instance.Pause();
    }

    private void OnDisable()
    {
        Timer.instance.Resume();
    }
}
