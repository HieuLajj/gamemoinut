using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cicle150 : Board_Item
{
    public override bool Define_intersection(Vector3 positionM)
    {
        float distance = Vector3.Distance(transform.position, positionM);
        float radiuscicle = Mathf.Abs(positionAnchor[0].position.y);
        if (distance < radiuscicle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
