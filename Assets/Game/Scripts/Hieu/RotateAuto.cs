using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAuto : MonoBehaviour
{
    public float rotationSpeed = 25f; // Tốc độ quay của UI

    public RectTransform rectTransform;


    private void Update()
    {
        // Quay UI xung quanh vị trí của nó
        rectTransform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
