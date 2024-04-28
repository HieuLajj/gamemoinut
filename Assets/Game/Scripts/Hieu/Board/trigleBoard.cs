using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class trigleBoard : Board_Item
{
    // Start is called before the first frame update
    public override bool Define_intersection(Vector3 positionM)
    {
        float Ax = positionAnchor[0].position.x;
        float Ay = positionAnchor[0].position.y;

        float Bx = positionAnchor[1].position.x;
        float By = positionAnchor[1].position.y;

        float Cx = positionAnchor[2].position.x;
        float Cy = positionAnchor[2].position.y;

        float Px = positionM.x;
        float Py = positionM.y;

        float w1 = (Ax * (Cy - Ay) + (Py - Ay) * (Cx - Ax) - Px * (Cy - Ay)) / ((By - Ay)*(Cx -Ax)-(Bx - Ax)*(Cy - Ay));

        float w2 = (Py - Ay - w1 * (By - Ay))/(Cy - Ay);
        if(w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1)
        {
            return true;
        }
        return false;
    }
}
