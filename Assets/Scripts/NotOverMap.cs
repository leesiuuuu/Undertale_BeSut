using UnityEngine;

public class NotOverMap : MonoBehaviour
{
    private Vector3 LastPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && StateManager.instance.Fighting)
        {
            Debug.Log("Entered!");
            LastPosition = collision.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && StateManager.instance.Fighting)
        {
            if (!IsInsideTrigger(other)) other.transform.position = LastPosition;
            else LastPosition = other.transform.position;
        }
    }
    bool IsInsideTrigger(Collider2D player)
    {
        // 플레이어의 콜라이더 경계가 트리거 안에 있는지 확인
        return GetComponent<BoxCollider2D>().bounds.Contains(player.bounds.min) && GetComponent<BoxCollider2D>().bounds.Contains(player.bounds.max);
    }
}
