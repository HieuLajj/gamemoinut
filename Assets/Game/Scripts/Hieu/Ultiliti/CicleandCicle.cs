using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class CicleandCicle : MonoBehaviour
{
    //2 hinh tron giao nhau theo ti le %
    public static double Dientichgiaonhauhaihinhtron(float d, float R1, float R2)
    {
        double gocb = (Math.Pow(d, 2) + Math.Pow(R2, 2) - Math.Pow(R1, 2)) / (2 * d * R2);
        double goca = (Math.Pow(d, 2) + Math.Pow(R1, 2) - Math.Pow(R2, 2)) / (2 * d * R1);
        float gocbdo = Mathf.Acos((float)gocb);
        float gocado = Mathf.Acos((float)goca);
   
        float alpha = 2 * gocado;
        float beta = 2 * gocbdo;
        double dientichchung = (alpha * Math.Pow(R1, 2) ) / 2 - (Math.Pow(R1,2) * Math.Sin(alpha))/2 + (beta * Math.Pow(R2, 2)) / 2 - (Math.Pow(R2, 2) * Math.Sin(beta)) / 2;
        Debug.Log(dientichchung / (Math.PI * Math.Pow(R1, 2)) + "dien tich hinh tron 2");
        return dientichchung;
    }

    public static float CalculateOverlapArea(float distance, float r1, float r2)
    {
        float d = distance;
        float rA = r1;
        float rB = r2;

        float x = (d * d - rB * rB + rA * rA) / (2 * d);
        float z = x * x;
        float y = Mathf.Sqrt(rA * rA - z);

        float angleA = Mathf.Acos(x / rA);
        float angleB = Mathf.Acos((d - x) / rB);

        float areaA = angleA * rA * rA;
        float areaB = angleB * rB * rB;
        float areaOverlap = areaA - x * y + areaB;

        return areaOverlap;
    }
}
