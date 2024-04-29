using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxsSystem : MonoBehaviour
{
    public BoxsData boxsData;
    public BoxsItem BoxItemsCurrent;
    private static BoxsSystem instance;
    public static BoxsSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoxsSystem>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    public List<BoxsItem> boxsItemList;

    public void InitBox(string name){
        GameObject g = boxsData.GetBoxsItem(name);
        BoxsItem boxsItem = Instantiate(g,transform).GetComponent<BoxsItem>();
        boxsItemList.Add(boxsItem);

        BoxItemsCurrent = GetCurrentBox();
    }
    public BoxsItem GetCurrentBox(){
        return boxsItemList[0];
    }
    
}
