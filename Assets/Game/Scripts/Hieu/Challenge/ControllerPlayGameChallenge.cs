using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerPlayGameChallenge : MonoBehaviour
{
    public Camera cameraMain;
    void Update()
    {
        bool isOverUI;

       // bool isOverUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
            //UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (Input.GetMouseButtonDown(0))
        {
#if UNITY_EDITOR
        isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
#else
        isOverUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif
            if (!isOverUI)
            {
                Vector3 mousePositionBD = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 13);
                Vector2 mousePosition = cameraMain.ScreenToWorldPoint(mousePositionBD);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
                foreach (RaycastHit2D hit in hits)
                {

                    ChallengeHole holechallenge = hit.transform.GetComponent<ChallengeHole>();
                    if (holechallenge != null)
                    {
                        holechallenge.ActiveWhenDown();
                    }

                }
            }
        }
    }
}
