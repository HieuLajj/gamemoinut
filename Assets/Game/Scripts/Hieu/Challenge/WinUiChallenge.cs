using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUiChallenge : MonoBehaviour
{
    public ParticleSystem paritcleSystemWin;
    private void OnEnable()
    {      
        //DailyChallengeManager.SetDailyChallengeComplete(GameManager.dataSave.currentDailyChallengeDay);
        paritcleSystemWin.Play();
    }
    public void Home()
    {
       
        // SuSceneManager.LoadScene(SceneName.scn_home, () =>
        // {
        //     //ItemManager.RaiseItemWithEffect(itemRewardModuleChallenge, null, null);
        // });
    }
}
