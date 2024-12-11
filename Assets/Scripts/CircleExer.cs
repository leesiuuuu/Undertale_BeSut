using UnityEngine;

public class CircleExer : MonoBehaviour
{
    public float circleR; //������
    public float deg; //����
    public float objSpeed; //��� �ӵ�

    public GameObject Barrier;

    void Update()
    {
        deg += Time.deltaTime * objSpeed;
        if (deg < 360)
        {
            var rad = Mathf.Deg2Rad * (deg);
            var x = circleR * Mathf.Sin(rad);
            var y = circleR * Mathf.Cos(rad);
            Barrier.transform.position = transform.position + new Vector3(x, y);
            Barrier.transform.rotation = Quaternion.Euler(Barrier.transform.rotation.x, Barrier.transform.rotation.y, Barrier.transform.rotation.z + deg * -1); //����� �ٶ󺸰� ���� ����
        }
        else
        {
            deg = 0;
        }
    }
}
