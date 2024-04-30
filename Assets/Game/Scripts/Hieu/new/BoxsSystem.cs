using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoxsSystem : MonoBehaviour
{
    public static Action ActionStartNewBox;
    public GameObject length2box;
    public GameObject length3box;
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
   // public List<BoxsItem> boxsItemList;
    public Stack boxsItemList;
    private void Start() {
        boxsItemList = new Stack();
    }
    public void InitBox(string name,int length, Boxs boxs){
        Debug.Log("" + name);
        Sprite sprite = boxsData.GetBoxsItem(name);
        GameObject g = GetLengthBox(length);
        BoxsItem boxsItem = Instantiate(g,transform).GetComponent<BoxsItem>();
        boxsItem.imagebox.sprite = sprite;
        boxsItem.boxs = boxs;
        boxsItemList.Push(boxsItem);
        boxsItem.gameObject.SetActive(false);
    }

    public void SetCurrentBox(){
        BoxItemsCurrent = GetCurrentBox();
        if(BoxItemsCurrent!=null){
            BoxItemsCurrent.gameObject.SetActive(true);
        }else{
            Debug.Log("wingame");
        }
    }
    public void SetCurrentBoxFromNail(){
        SetCurrentBox();
        ActionStartNewBox?.Invoke();
    }
    public GameObject GetLengthBox(int length){
        GameObject g = null;
        switch(length){
            case 2:
                g = length2box;
                break;
            case 3:
                g = length3box;
                break;
        }
        return g;
    }
    public BoxsItem GetCurrentBox(){
        if(boxsItemList.Count==0){
            return null;
        }
        return (BoxsItem)boxsItemList.Pop();
    }  
}
