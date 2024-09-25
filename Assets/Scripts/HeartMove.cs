using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class HeartMove : MonoBehaviour
{
    private Vector3 MoveVelocity;
    public float MoveSpeed;
    private bool isInvin = false;
    void Update()
    {
        MoveVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) MoveVelocity = Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) MoveVelocity = Vector3.right;
        if(Input.GetKey(KeyCode.UpArrow)) MoveVelocity = Vector3.up;
        if(Input.GetKey(KeyCode.DownArrow)) MoveVelocity = Vector3.down;
        transform.position += MoveVelocity * MoveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D cols)
    {
        if (cols.gameObject.CompareTag("AttackSprite") && !isInvin)
        {
            PlayerManager.instance.HP -= 8;
            isInvin = true;
            Debug.Log(PlayerManager.instance.HP);
            Invoke("ReturnInvin", 0.7f);
        }
    }
    void ReturnInvin()
    {
        isInvin = false;
    }
}
