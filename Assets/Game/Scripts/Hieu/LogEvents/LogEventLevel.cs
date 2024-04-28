using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEventLevel : MonoBehaviour
{
    private void Start()
    {
        if (LevelController.Instance.LevelIDInt > 6)
        {
            LevelController.EventStartGame -= LogEvent;
            Destroy(gameObject);
            return;
        }
        LevelController.EventStartGame += LogEvent;
    }

    private void LogEvent()
    {

        if (LevelController.Instance.LevelIDInt > 6)
        {
            LevelController.EventStartGame -= LogEvent;
            Destroy(gameObject);
            return;
        }
        if (LevelController.Instance.LevelDifficule==false && LevelController.Instance.LevelIDInt > 1)
        {
            string level = $"Level_Up_{LevelController.Instance.LevelIDInt}";
           // SuGame.Get<SuAnalytics>().LogEvent(level);
        }
        
    }
    private void OnDestroy()
    {
        LevelController.EventStartGame -= LogEvent;
    }
}
