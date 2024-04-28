using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelConfig
{
    public int level;
    public int step1;
    public int step2;
    public int step1Time;
    public int step2Time;
}

[System.Serializable]
public class LevelConfigList
{
    public List<LevelConfig> levelConfig;
}
public class JsonReadLevelConfig : MonoBehaviour
{


    public PhysicsMaterial2D physicMaterial;
    public Dictionary<int, LevelConfig> levelConfigDictionary;

    public Dictionary<int, int> dictionarySwapLevel = new Dictionary<int, int>();
    public Dictionary<int, int> dictionaryTimeConfig = new Dictionary<int,int>();
    private static JsonReadLevelConfig instance;
    public static JsonReadLevelConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JsonReadLevelConfig>();
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        ReadJSONFileAndConvert();



        // foreach (SwapConfigModule item in RemoteConfigManager.General_Config.swap_config)
        // {
        //     dictionarySwapLevel[item.level] = item.position;
        //     dictionarySwapLevel[item.position] = item.level;
        // }
        // foreach (TimeConfigModule item in RemoteConfigManager.General_Config.time_config)
        // {
        //     dictionaryTimeConfig[item.level] = item.time;       
        // }

        // //physic
        // Physics2D.gravity = new Vector2(0, RemoteConfigManager.General_Config.hard_config.gravity);

        // physicMaterial.friction = RemoteConfigManager.General_Config.hard_config.physicmaterial.friction; 
        // physicMaterial.bounciness = RemoteConfigManager.General_Config.hard_config.physicmaterial.bounciness; 
    }

    public string SwapLevel(int level, bool difficult)
    {
        return "";
        int levelswap = 0;
        // if (!difficult)
        // {
        //     levelswap = 2*(level-1) + 1;
        // }
        // else
        // {
        //     levelswap = 2*level;
        // }
        // if (dictionarySwapLevel.ContainsKey(levelswap))
        // {
        //     int levelswapdictionary = dictionarySwapLevel[levelswap];                
        //     if (levelswapdictionary % 2 == 0)
        //     {
        //         return $"Hieu\\Level\\Difficult\\{levelswapdictionary/2}_data_super";
        //     }
        //     else
        //     {
        //         return $"Hieu\\Level\\Easy\\data_{(int)(levelswapdictionary/2)+1}";
        //     }
        // }
        // else
        // {
        //     return  "";
        // }
    }

    void ReadJSONFileAndConvert()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Hieu\\LevelConfig\\Level_Config");
        string jsonContent = jsonFile.text;
        LevelConfigList levelConfigList = JsonUtility.FromJson<LevelConfigList>(jsonContent);
        ConvertListToDictionary(levelConfigList.levelConfig);

    }
    void ConvertListToDictionary(List<LevelConfig> levelConfigs)
    {
        levelConfigDictionary = new Dictionary<int, LevelConfig>();
        foreach (LevelConfig config in levelConfigs)
        {
            levelConfigDictionary.Add(config.level, config);
        }
    }

    public int GetTimer()
    {
        int level = 1;
        int timer = 0;
        int levelswap = 0;
        if (!LevelController.Instance.LevelDifficule)
        {
            levelswap = 2 * (level - 1) + 1;
        }
        else
        {
            levelswap = 2 * level;
        }         
        if (dictionaryTimeConfig.ContainsKey(levelswap))
        {
            timer = dictionaryTimeConfig[levelswap];
        }
        else if (levelConfigDictionary.ContainsKey(level))
        {
            timer = levelConfigDictionary[level].step2Time;
        }
        else
        {
            if (LevelController.Instance.LevelDifficule)
            {
                timer = 150;
            }
            else
            {
                timer = 60;
            }   
        }
        if (timer <= 10)
        {
            timer = 150;
        }
        // if (VIPManager.IsVIP())
        // {
        //     timer += (int)(timer * 0.3f);
        // }
        return timer;
    }




}
