using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoard : Board_Item
{
    public Transform cicle2;
    public override bool Define_intersection(Vector3 positionM)
    {
        float distance2 = Vector3.Distance(cicle2.position, positionM);
        float radiuscicle2 = Vector3.Distance(cicle2.position, positionAnchor[1].position);
        if (distance2 < radiuscicle2)
        {         
            return false;
        }

        float distance = Vector3.Distance(transform.position, positionM);
        float radiuscicle = Vector3.Distance(transform.position, positionAnchor[0].position);
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
