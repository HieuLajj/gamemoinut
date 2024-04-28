using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BshapeBoard : Board_Item
{
    public override bool Define_intersection(Vector3 positionM)
    {

        // failed khac may cai thong thuong
        Vector2 am = new Vector2(positionM.x - positionAnchor[0].position.x, positionM.y - positionAnchor[0].position.y);
        Vector2 ab = new Vector2(positionAnchor[1].position.x - positionAnchor[0].position.x, positionAnchor[1].position.y - positionAnchor[0].position.y);
        Vector2 ad = new Vector2(positionAnchor[3].position.x - positionAnchor[0].position.x, positionAnchor[3].position.y - positionAnchor[0].position.y);
        float amab = Vector3.Dot(am, ab);
        float abab = Vector3.Dot(ab, ab);
        float amad = Vector3.Dot(am, ad);
        float adad = Vector3.Dot(ad, ad);
        if (amab > 0 && abab > amab && amad > 0 && adad > amad)
        {
            return false;
        }

        Vector2 am2 = new Vector2(positionM.x - positionAnchor[4].position.x, positionM.y - positionAnchor[4].position.y);
        Vector2 ab2 = new Vector2(positionAnchor[5].position.x - positionAnchor[4].position.x, positionAnchor[5].position.y - positionAnchor[4].position.y);
        Vector2 ad2 = new Vector2(positionAnchor[7].position.x - positionAnchor[4].position.x, positionAnchor[7].position.y - positionAnchor[4].position.y);
        float amab2 = Vector3.Dot(am2, ab2);
        float abab2 = Vector3.Dot(ab2, ab2);
        float amad2 = Vector3.Dot(am2, ad2);
        float adad2 = Vector3.Dot(ad2, ad2);
        if (amab2 > 0 && abab2 > amab2 && amad2 > 0 && adad2 > amad2)
        {
            return false;
        }
        Vector2 am3 = new Vector2(positionM.x - positionAnchor[8].position.x, positionM.y - positionAnchor[8].position.y);
        Vector2 ab3 = new Vector2(positionAnchor[9].position.x - positionAnchor[8].position.x, positionAnchor[9].position.y - positionAnchor[8].position.y);
        Vector2 ad3 = new Vector2(positionAnchor[11].position.x - positionAnchor[8].position.x, positionAnchor[11].position.y - positionAnchor[8].position.y);
        float amab3 = Vector3.Dot(am3, ab3);
        float abab3 = Vector3.Dot(ab3, ab3);
        float amad3 = Vector3.Dot(am3, ad3);
        float adad3 = Vector3.Dot(ad3, ad3);
        if (amab3 > 0 && abab3 > amab3 && amad3 > 0 && adad3 > amad3)
        {
            return true;
        }

        float distance = Vector3.Distance(positionAnchor[12].position, positionM);
        float radiuscicle = Vector3.Distance(positionAnchor[12].position, positionAnchor[13].position);
        if (distance < radiuscicle)
        {
            return true;
        }
        float distance2 = Vector3.Distance(positionAnchor[14].position, positionM);
        float radiuscicle2 = Vector3.Distance(positionAnchor[14].position, positionAnchor[15].position);
        if (distance2 < radiuscicle2)
        {
            return true;
        }






        return false;
    }
}
