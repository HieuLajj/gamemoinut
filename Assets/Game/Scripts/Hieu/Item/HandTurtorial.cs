
using UnityEngine;


public class HandTurtorial : MonoBehaviour
{
    private void OnEnable()
    {     
        ActionHand();
        LevelController.Instance.checkActionUser += ActionHand;
    }

    private void OnDisable()
    {
        LevelController.Instance.checkActionUser -= ActionHand;
    }
    public void ActionHand()
    {
        if (ControllerHieu.Instance.rootlevel.listHand.Count != 0)
        {
            HandTut handTut = ControllerHieu.Instance.rootlevel.listHand[0];
            //Debug.Log("co chay ma ba con oi" + handTut.pos.x);
            ControllerHieu.Instance.rootlevel.listHand.RemoveAt(0);
            SetUp(handTut);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void SetUp(HandTut handTut)
    {
        transform.position = new Vector3(handTut.pos.x, handTut.pos.y, handTut.pos.z);
        transform.localScale = new Vector3(handTut.scale.x, handTut.scale.y, handTut.scale.z);
        transform.rotation =  Quaternion.Euler(new Vector3(handTut.rot.x, handTut.rot.y, handTut.rot.z));
    }
}
