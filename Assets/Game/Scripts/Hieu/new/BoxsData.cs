using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableObject", menuName = "BoxsData")]
public class BoxsData: ScriptableObject
{
   public List<BoxsItemData> Datas = new List<BoxsItemData>();
   public Sprite GetBoxsItem(string nameid){
        for(int i=0; i<Datas.Count; i++){
            if(nameid==Datas[i].Id){
                return Datas[i].sprite;
            }
        }
        Debug.Log("hiihihi");
        return null;
   }
}
[System.Serializable]
public class BoxsItemData
{
    public string Id;
    public Sprite sprite;
}