using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class ControllerHieu : MonoBehaviour
{

    public Material[] materialsthunder;
    private int indexMaterialThunder = 0;
    public const int MAX_LEVEL = 100;

    public delegate void TimeDelegate(int a);
    public static event TimeDelegate TimeEvent;

    private AsyncOperation async;
    private static ControllerHieu instance;
    public static ControllerHieu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ControllerHieu>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public RootLevel rootlevel;

    public NailSpawner nailSpawner;

    public NailLayerController nailLayerController;

    //[Header("UI")]
    //[Space(10)]
    //public Background background_ui;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        Application.runInBackground = false;
    }

    public void StartLevel(){
        LevelController.Instance.StartGame();
    }
    
    public void LoadLevelScene(string namescene){
        StartCoroutine(LoadSceneAsync(namescene));
    }

    IEnumerator LoadSceneAsync (string sceneName){
        if(!string.IsNullOrEmpty(sceneName)){
            
            async = SceneManager.LoadSceneAsync(sceneName);
            while(!async.isDone){
                yield return 0;
            }
            
        }
    }
    public void TimeRemotetoController(int a)
    {
        TimeEvent?.Invoke(a);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, 30);
    }

    void DrawCircle(Vector3 center, float radius)
    {
        float theta = 0;
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);
        Vector3 startPos = center + new Vector3(x, 0, y);
        Vector3 endPos = Vector3.zero;

        for (int i = 1; i <= 360; i++)
        {
            theta = (i * Mathf.PI) / 180;
            x = radius * Mathf.Cos(theta);
            y = radius * Mathf.Sin(theta);
            endPos = center + new Vector3(x, 0, y);
            Debug.DrawLine(startPos, endPos, Color.red);
            startPos = endPos;
        }
    }
    public Material GetMaterinalThunder()
    {
        int materialindex = 0;
        if(indexMaterialThunder>= materialsthunder.Length)
        {
            indexMaterialThunder = 0;
        }
        materialindex = indexMaterialThunder;
        indexMaterialThunder++;
        return materialsthunder[materialindex];
    }

}
