using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EShapeBoard : Board_Item
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


        Vector2 am2 = new Vector2(positionM.x - positionAnchor[4].position.x, positionM.y - positionAnchor[4].position.y);
        Vector2 ab2 = new Vector2(positionAnchor[5].position.x - positionAnchor[4].position.x, positionAnchor[5].position.y - positionAnchor[4].position.y);
        Vector2 ad2 = new Vector2(positionAnchor[7].position.x - positionAnchor[4].position.x, positionAnchor[7].position.y - positionAnchor[4].position.y);
        float amab2 = Vector3.Dot(am2, ab2);
        float abab2 = Vector3.Dot(ab2, ab2);
        float amad2 = Vector3.Dot(am2, ad2);
        float adad2 = Vector3.Dot(ad2, ad2);
        if (amab2 > 0 && abab2 > amab2 && amad2 > 0 && adad2 > amad2)
        {
            return true;
        }
        return false;
    }
}
