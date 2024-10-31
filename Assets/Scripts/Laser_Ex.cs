using UnityEngine;

public class Laser_Ex : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_LR;
    public int segmentCount = 5; // ������ �β��� ���� ���׸�Ʈ ����
    private float segmentOffset = 0.01f; // ���׸�Ʈ �� ����

    [SerializeField] private LayerMask ignoreLayers; // ������ ���̾� �߰�

    private void Start()
    {
        m_LR.positionCount = segmentCount * 2; // �� ���׸�Ʈ�� ���۰� �� ��
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            // ���׸�Ʈ���� �ణ�� �������� ������ ��ġ�� ����
            Vector3 offset = new Vector3(0, (i - segmentCount / 2) * segmentOffset, 0);
            Vector2 start = laserFirePoint.position + offset;

            // Ư�� ���̾ ������ Raycast ����
            RaycastHit2D hit = Physics2D.Raycast(start, transform.right, defDistanceRay, ~ignoreLayers);

            if (hit)
            {
                Draw2DRay(i, start, hit.point);
            }
            else
            {
                Draw2DRay(i, start, (Vector2)start + (Vector2)transform.right * defDistanceRay);
            }
        }
    }

    void Draw2DRay(int index, Vector2 StartPos, Vector2 EndPos)
    {
        m_LR.SetPosition(index * 2, StartPos);
        m_LR.SetPosition(index * 2 + 1, EndPos);
    }
}
