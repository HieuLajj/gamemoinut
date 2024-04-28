using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //private static AudioController instance;
    //public static AudioController Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<AudioController>();
    //        }
    //        return instance;
    //    }
    //    private set
    //    {
    //        instance = value;
    //    }
    //}

    //public Dictionary<string, AudioClip> myDictionary = new Dictionary<string, AudioClip>();

    //public AudioSource audioSource;
    //public AudioSource audioSourceBackGround;

    //private void Start()
    //{
    //    SettingPanelUI.SoundCheck = PlayerPrefs.GetInt("Sound");
    //    SettingPanelUI.MusicCheck = PlayerPrefs.GetInt("Music");
    //}
    //public void PlayClip(string clipstring)
    //{
    //        if (SettingPanelUI.SoundCheck == 0)
    //        {
    //            return;
    //        }
    //        if (myDictionary.ContainsKey(clipstring))
    //        {
    //            if (myDictionary[clipstring] == null)
    //            {
    //                return;
    //            }
    //            audioSource.PlayOneShot(myDictionary[clipstring]);
    //        }
    //        else
    //        {
    //            string str = AddresAudioString(clipstring);
    //            if (str == "") { return; }
    //            //AsyncOperationHandle<AudioClip> asyncOperationHandle11 = Addressables.LoadAssetAsync<AudioClip>(str);

    //            //DateTime datecheck = DateTime.Now;
    //            //myDictionary[clipstring] = null;
    //            //asyncOperationHandle11.Completed += (handle) =>
    //            //{
    //            //    if (handle.Status == AsyncOperationStatus.Succeeded)
    //            //    {
    //            //        myDictionary[clipstring] = handle.Result;
    //            //        if ((DateTime.Now - datecheck).TotalSeconds <= 1)
    //            //        {
    //            //           audioSource.PlayOneShot(handle.Result);
    //            //        }
    //            //    }
    //            //};
    //        }
    //}
    //private string AddresAudioString(string str)
    //{
    //    string h = "";

    //    switch (str)
    //    {
    //        case "naildam":
    //            h = "Assets/Audio/CollectEgg.ogg";
    //            break;
    //        case "clean":
    //            h = "Assets/Audio/PullThePin.ogg";
    //            break;
    //        case "win":
    //            h = "Assets/Audio/Confetti.wav";
    //            break;
    //        case "vacham":
    //            h = "Assets/Audio/etfx_target_hit.ogg";
    //            break;
    //        case "vachamdinh":
    //            h = "Assets/Audio/etfx_screw_break.ogg";
    //            break;
    //    }
    //    return h;
    //}

    //public void StartSoundBackGround()
    //{
    //    audioSourceBackGround.Play();
    //}

    //public void EndSoundBackGround()
    //{
    //    audioSourceBackGround.Stop();
    //}
}
