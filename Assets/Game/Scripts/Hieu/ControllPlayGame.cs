
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ControllPlayGame : MonoBehaviour
{
    private static ControllPlayGame instance;
    public static ControllPlayGame Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ControllPlayGame>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public Nail_Item targetNail;
   // public Nail_Item pre_targetNail;
   // public Nail_Item prepre_targetNail;
    public static bool isOverUI;
  
   
    private void Start()
    {
        ControllerHieu.Instance.rootlevel = new RootLevel();
        ControllerHieu.Instance.StartLevel();
    }
    [Button()]
    public void RunNay()
    {
        ControllerHieu.Instance.StartLevel();
    }
    void Update()
    {
       // bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePositionBD = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 13);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mousePositionBD);       
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {          
                if (hit.collider.CompareTag("Nail")){           
                    hit.transform.GetComponent<Nail_Item>().ActiveWhenDown();
                }
            }
        }
    }
}
