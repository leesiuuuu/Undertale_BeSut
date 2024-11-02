using UnityEngine;
using UnityEngine.LowLevel;

public class Laser_Ex : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_LR;
    public int segmentCount = 5; // 레이저 두께를 위한 세그먼트 개수
    public float segmentOffset = 0.01f; // 세그먼트 간 간격
    [SerializeField] private LayerMask ignoreLayers; // 무시할 레이어 추가

    public float laserSpeed;
    private float currentLaserDistance = 0f; // 현재 레이저 길이

    private void Start()
    {
        m_LR.positionCount = segmentCount * 2; // 각 세그먼트의 시작과 끝 점
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        // 레이저 길이를 점진적으로 증가시킴
        currentLaserDistance = Mathf.Min(currentLaserDistance + laserSpeed * Time.deltaTime, defDistanceRay);

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
