//Chat GPT Code
using UnityEngine;

public class Laser_Ex : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_LR;
    public int segmentCount = 5; // ������ �β��� ���� ���׸�Ʈ ����
    public float segmentOffset = 0.01f; // ���׸�Ʈ �� ����
    [SerializeField] private LayerMask ignoreLayers; // ������ ���̾� �߰�

    public float laserSpeed; // �������� �����ϴ� �ӵ�
    public float laserShrinkSpeed; // �������� �����ϴ� �ӵ�
    private float currentLaserDistance = 0f; // ���� ������ ����

    public bool isLaserExpanding = true; // �������� ���� ������ ���� ������ Ȯ��

    public float _LaserDelay;
    private float LaserDelay;

    private void Start()
    {
        m_LR.positionCount = segmentCount * 2; // �� ���׸�Ʈ�� ���۰� �� ��
    }

    private void Update()
    {
        if(LaserDelay <= _LaserDelay)
        {
            LaserDelay += Time.deltaTime;
        }
        else
        {
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        // �������� ���� ���̸� ������Ű��, �ƴϸ� ���ҽ�Ŵ
        if (isLaserExpanding)
        {
            currentLaserDistance = Mathf.Min(currentLaserDistance + laserSpeed * Time.deltaTime, defDistanceRay);
            if (currentLaserDistance >= defDistanceRay)
            {
                currentLaserDistance = defDistanceRay;
            }
        }
        else
        {
            currentLaserDistance = Mathf.Max(currentLaserDistance - laserShrinkSpeed * Time.deltaTime, 0);
            if (currentLaserDistance <= 0)
            {
                this.enabled = false;
                Destroy(this.gameObject);
            }
        }

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 offset = new Vector3(0, (i - segmentCount / 2) * segmentOffset, 0);
            Vector2 start = laserFirePoint.position + offset;

            RaycastHit2D hit = Physics2D.Raycast(start, transform.right, currentLaserDistance, ~ignoreLayers);

            if (hit)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Debug.Log("Hit!");
                    Draw2DRay(i, start, (Vector2)start + (Vector2)transform.right * currentLaserDistance);
                }
                else
                {
                    Draw2DRay(i, start, hit.point);
                }
            }
            else
            {
                Draw2DRay(i, start, (Vector2)start + (Vector2)transform.right * currentLaserDistance);
            }
        }
    }

    void Draw2DRay(int index, Vector2 StartPos, Vector2 EndPos)
    {
        m_LR.SetPosition(index * 2, StartPos);
        m_LR.SetPosition(index * 2 + 1, EndPos);
    }
}