using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableObject", menuName = "BoxsData")]
public class BoxsData: ScriptableObject
{
   public List<BoxsItemData> Datas = new List<BoxsItemData>();
   public GameObject GetBoxsItem(string nameid){
        for(int i=0; i<Datas.Count; i++){
            if(nameid==Datas[i].Id){
                return Datas[i].PrefabBoxs;
            }
        }
        return null;
   }
}
[System.Serializable]
public class BoxsItemData
{
    public string Id;
    public GameObject PrefabBoxs;
}