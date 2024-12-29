using UnityEngine;

public class AttackPattern2 : AttackPattern2M
{
    /// <summary>
    /// 0 = ��
    /// 1 = ��
    /// 2 = ��
    /// 3 = �Ʒ�
    /// </summary>
    
    protected int dir;
    private float time;
    public float EndTime;
    //���� �� ���� ����� �� ���� �ӵ��� �� 3~4�� ���̳�.
    //ex) 40 -> 150
    public float Speed;
    private Vector3 direction;
    private void Start()
    {
        direction = Vector3.zero;
        dir = Random.Range(0, 4);
        ReturnRandomValue(dir);
        RotateInit();
        Posinit(gameObject, dir);
    }
    private void Update()
    {
        FromMove(this.gameObject, gameObject.transform.position, dir, EndTime);
    }
    void FromMove(GameObject MoveObject, Vector3 From, int Rotate, float ttime)
    {
        time += Time.deltaTime;
        switch (Rotate)
        {
            case 0:
                MoveObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                direction = Vector3.left;
                break;
            case 1:
                MoveObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                direction = Vector3.right;
                break;
            case 2:
                MoveObject.transform.rotation = Quaternion.Euler(0, 0, -90);
                direction = Vector3.up;
                break;
            case 3:
                MoveObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                direction = Vector3.down;
                break;
        }

        if (time < ttime)
            transform.position += direction * 0.001f * Speed;
        else
        {
            Destroy(gameObject);
        }
    }
}
