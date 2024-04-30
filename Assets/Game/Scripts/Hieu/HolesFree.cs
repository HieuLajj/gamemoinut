using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolesFree : MonoBehaviour
{
    public Nail_Item nail_Item;
    private void OnEnable() {
      BoxsSystem.ActionStartNewBox+= ActionRegisterStartBox;
    }

   private void OnDisable() {
    BoxsSystem.ActionStartNewBox-= ActionRegisterStartBox;
   }
   public void ActionRegisterStartBox(){
    if(nail_Item!=null){
        Debug.Log("thuch hien nay heheheh");
        StartCoroutine(check());
    }
   }
    IEnumerator check()
    {
        yield return new WaitForSeconds(transform.GetSiblingIndex()*0.01f);
        nail_Item.CheckAfterNewBox();
    }
   
}
