using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class HeartMove : MonoBehaviour
{
    private Vector3 MoveVelocity;
    public float MoveSpeed;
    void Update()
    {
        MoveVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) MoveVelocity = Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) MoveVelocity = Vector3.right;
        if(Input.GetKey(KeyCode.UpArrow)) MoveVelocity = Vector3.up;
        if(Input.GetKey(KeyCode.DownArrow)) MoveVelocity = Vector3.down;
        transform.position += MoveVelocity * MoveSpeed * Time.deltaTime;
    }
}
