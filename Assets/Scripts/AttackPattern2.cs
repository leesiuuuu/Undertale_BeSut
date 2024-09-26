using UnityEngine;

public class AttackPattern2 : AttackPattern2M
{
    /// <summary>
    /// 0 = ¿Þ
    /// 1 = ¿À
    /// 2 = À§
    /// 3 = ¾Æ·¡
    /// </summary>
    protected int dir;
    private float time;
    public float EndTime;
    public float constant;
    public float Speed;
    private Vector3 direction;
    private void Start()
    {
        dir = Random.Range(0, 4);
        EndTime = constant / Speed;
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
