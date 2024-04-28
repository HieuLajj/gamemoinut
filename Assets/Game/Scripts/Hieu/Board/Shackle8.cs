using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shackle8 : Board_Item
{
    public Transform Cicle4;
    public Transform Cicle5;
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

        float distance = Vector3.Distance(Cicle4.position, positionM);
        float radiuscicle = Vector3.Distance(Cicle4.position, positionAnchor[4].position);
        if (distance < radiuscicle)
        {
            return true;
        }

        float distance2 = Vector3.Distance(Cicle5.position, positionM);
        float radiuscicle2 = Vector3.Distance(Cicle5.position, positionAnchor[5].position);
        if (distance2 < radiuscicle2)
        {
            return true;
        }

        return false;
        
    }
}
