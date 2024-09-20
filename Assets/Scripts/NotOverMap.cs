using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotOverMap : MonoBehaviour
{
    private Vector3 LastPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Heart" && StateManager.instance.Fighting)
        {
            Debug.Log("Entered!");
            LastPosition = collision.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Heart" && StateManager.instance.Fighting)
        {
            if (!IsInsideTrigger(other)) other.transform.position = LastPosition;
            else LastPosition = other.transform.position;
        }
    }
    bool IsInsideTrigger(Collider2D player)
    {
        // �÷��̾��� �ݶ��̴� ��谡 Ʈ���� �ȿ� �ִ��� Ȯ��
        return GetComponent<BoxCollider2D>().bounds.Contains(player.bounds.min) && GetComponent<BoxCollider2D>().bounds.Contains(player.bounds.max);
    }
}
