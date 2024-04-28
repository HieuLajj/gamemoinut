using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board700x100 : Board_Item
{
    public override bool Define_intersection(Vector3 positionM)
    {
        Vector2 am = new Vector2(positionM.x - positionAnchor[0].position.x, positionM.y - positionAnchor[0].position.y);
        Vector2 ab = new Vector2(positionAnchor[1].position.x - positionAnchor[0].position.x, positionAnchor[1].position.y - positionAnchor[0].position.y);
        Vector2 ad = new Vector2(positionAnchor[3].position.x - positionAnchor[0].position.x, positionAnchor[3].position.y - positionAnchor[0].position.y);
        float amab = Vector3.Dot(am, ab);
        float abab = Vector3.Dot(ab, ab);
        float amad = Vector3.Dot(am, ad);
        float adad = Vector3.Dot(ad, ad);
        if (amab > 0 && abab > amab && amad > 0 && adad > amad)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //public override void AutoRotate(Slot_board_Item slotboardItem, Slot_board_Item findanySlotBoardIteminboard, Slot_Item slotItem)
    //{
    //    Slot_board_Item parentt = FindOtherSlotBoard(slotboardItem);
    //    Vector2 dirboard = findanySlotBoardIteminboard.transform.position - slotItem.transform.position;
    //    Vector2 dir3 = findanySlotBoardIteminboard.transform.position - slotboardItem.transform.position;
    //    findanySlotBoardIteminboard.transform.SetParent(this.transform.parent);
    //    this.transform.SetParent(findanySlotBoardIteminboard.transform);
    //    float a = findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard);  
    //    if (this.transform.eulerAngles.z < 180)
    //    {
    //        if (this.transform.eulerAngles.z >= 90f)
    //        {
    //            findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
    //        }
    //        else
    //        {
    //            findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
    //        }
    //    }
    //    else
    //    {
    //        if (this.transform.eulerAngles.z < 300f)
    //        {
    //            findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z + Vector2.Angle(dir3, dirboard));
    //        }
    //        else
    //        {
    //            findanySlotBoardIteminboard.transform.rotation = Quaternion.Euler(0, 0, findanySlotBoardIteminboard.transform.rotation.eulerAngles.z - Vector2.Angle(dir3, dirboard));
    //        }
    //    }
    //    this.transform.SetParent(findanySlotBoardIteminboard.transform.parent);
    //    findanySlotBoardIteminboard.transform.SetParent(transform);
    //}
}
