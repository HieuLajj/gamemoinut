using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxsItem : MonoBehaviour
{
    public BoxsChildItem[] BoxsChildArray;
    public BoxsChildItem GetBoxsChild(){
        for(int i=0; i<BoxsChildArray.Length;i++){
            if(BoxsChildArray[i].isOn == false){
                BoxsChildArray[i].isOn = true;
                return BoxsChildArray[i];
            }
        }
        return null;
    }
}
