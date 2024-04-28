using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HalfCicle : Board_Item
{
    public override bool Define_intersection(Vector3 positionM)
    {
        //Vector2 am = new Vector2(positionM.x - transform.position.x, positionM.y - transform.position.y);
        //Vector2 ab = new Vector2(positionAnchor[1].position.x - transform.position.x, positionAnchor[1].position.y - transform.position.y);
        //float amab = Vector3.Dot(am, ab);
        //if(amab < 0)
        //{
        //    Debug.Log("co chay nay fff");
        //    float distance2 = Vector3.Distance(transform.position, positionM);
        float radiuscicle2 = Vector3.Distance(positionAnchor[2].position, positionAnchor[0].position);
        //    if (distance2 < radiuscicle2)
        //    {
        //        return true;
        //    }
        //}

        return getPointsIns(positionAnchor[2].position, radiuscicle2, positionAnchor[1].position, positionM);



       // return false;

    }
    public bool getPointsIns(Vector3 positionCicle,
                                float radius,
                                Vector3 positionSub, Vector3 positionM)
            {
    
            bool condOne = false, condTwo = false;
            if ((positionM.y - positionSub.y) * (positionSub.x - positionCicle.x)
                - (positionSub.y - positionCicle.y) * (positionM.x - positionSub.x)
                >= 0)
            {
                condOne = true;
            }
    
            if (radius >= Math.Sqrt(
              Math.Pow((positionCicle.y - positionM.y), 2)
              + Math.Pow(positionCicle.x - positionM.x, 2)))
            {
                condTwo = true;
            }
            if (condOne && condTwo)
            {
                return true;
            }
            return false;
        }
   
    

}
