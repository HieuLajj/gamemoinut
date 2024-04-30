using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxsItem : MonoBehaviour
{
    public Boxs boxs;
    public int lengthboxscomplete = 0;
    public Image imagebox;
    public BoxsChildItem[] BoxsChildArray;
    public BoxsChildItem GetBoxsChild(string text){
        if(checkMaterial(text)!=boxs.TypeCL){
            return null;
        }
        for(int i=0; i<BoxsChildArray.Length;i++){
            if(BoxsChildArray[i].isOn == false){
                BoxsChildArray[i].isOn = true;
                lengthboxscomplete++;
                return BoxsChildArray[i];
            }
        }
        return null;
    }

    public void CheckCompleteBox(){
        if(lengthboxscomplete == BoxsChildArray.Length){
            gameObject.SetActive(false);
            BoxsSystem.Instance.SetCurrentBoxFromNail();
        }
    }
    public static TypeCL checkMaterial(string text){
        TypeCL typeCl = TypeCL.None;
        switch(text){
            case "pink":
            typeCl = TypeCL.Blink;
            break;
            case "green":
            typeCl = TypeCL.Green;
            break;
            case "blue":
            typeCl = TypeCL.Blue;
            break;
            case "violet":
            typeCl = TypeCL.Violet;
            break;
            case "yellow":
            typeCl = TypeCL.Yellow;
            break;
        }
        Debug.Log(""+typeCl);
        return typeCl;
    }
}

