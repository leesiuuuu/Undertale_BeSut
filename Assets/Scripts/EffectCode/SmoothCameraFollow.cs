using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SmoothCameraFollow : MonoBehaviour
{
    public  GameObject target;
    public float Smooth;

    void Update()
    {
        Vector3 pos = Vector3.Lerp(transform.position, target.transform.position, Smooth);
        transform.position = new Vector3(
            pos.x, pos.y, -50);
    }
}
