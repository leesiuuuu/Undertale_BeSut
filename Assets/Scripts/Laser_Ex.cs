using UnityEngine;

public class Laser_Ex : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_LR;
    public int segmentCount = 5; // 레이저 두께를 위한 세그먼트 개수
    private float segmentOffset = 0.01f; // 세그먼트 간 간격

    [SerializeField] private LayerMask ignoreLayers; // 무시할 레이어 추가

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
        for (int i = 0; i < segmentCount; i++)
        {
            // 세그먼트마다 약간의 오프셋을 적용해 위치를 변경
            Vector3 offset = new Vector3(0, (i - segmentCount / 2) * segmentOffset, 0);
            Vector2 start = laserFirePoint.position + offset;

            // 특정 레이어를 제외한 Raycast 수행
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
