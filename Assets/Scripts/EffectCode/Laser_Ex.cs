//Chat GPT Code
using Unity.VisualScripting;
using UnityEngine;

public class Laser_Ex : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_LR;
    public int segmentCount = 5; // 레이저 두께를 위한 세그먼트 개수
    public float segmentOffset = 0.01f; // 세그먼트 간 간격
    [SerializeField] private LayerMask ignoreLayers; // 무시할 레이어 추가

    public float laserSpeed; // 레이저가 증가하는 속도
    public float laserShrinkSpeed; // 레이저가 감소하는 속도
    private float currentLaserDistance = 0f; // 현재 레이저 길이

    public bool isLaserExpanding = true; // 레이저가 증가 중인지 감소 중인지 확인

    public float _LaserDelay;

    public float LaserUnexpandingDelay;
    private float LaserDelay;

    public static bool LaserDamaged = false;

    private GameObject Player;
    private HeartMove HM_Player;

    public int XYCheck = 1; //0 = 수직 1 = 수평


    public float damageInterval = 0.05f; // 데미지를 줄 간격
    private float damageTimer = 0f; // 현재 데미지 쿨다운 상태

    public AudioClip LaserReady;
    public AudioClip LaserBoom;

    private bool once = false;

    private void Start()
    {
        SoundManager.instance.SFXPlay("LaserReady", LaserReady);
        m_LR.positionCount = segmentCount * 2; // 각 세그먼트의 시작과 끝 점
        Player = GameObject.Find("Heart");
        HM_Player = Player.GetComponent<HeartMove>();
    }

    private void Update()
    {
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }

        if (LaserDelay <= _LaserDelay)
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
        if (!once)
        {
            SoundManager.instance.SFXPlay("LaserBoom", LaserBoom);
            once = true;
        }
        // 레이저가 증가 중이면 증가시키고, 아니면 감소시킴
        if (isLaserExpanding)
        {
            currentLaserDistance = Mathf.Min(currentLaserDistance + laserSpeed * Time.deltaTime, defDistanceRay);
            if (currentLaserDistance >= defDistanceRay)
            {
                currentLaserDistance = defDistanceRay;
                Invoke("ChangeExpanding", LaserUnexpandingDelay);
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
            Vector3 offset = Vector3.zero;
            if(XYCheck == 1) offset = new Vector3(0, (i - segmentCount / 2) * segmentOffset, 0);
            else offset = new Vector3((i - segmentCount / 2) * segmentOffset, 0, 0);

            Vector2 start = laserFirePoint.position + offset;

            RaycastHit2D hit = Physics2D.Raycast(start, transform.right, currentLaserDistance, ~ignoreLayers);

            if (hit)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    if(damageTimer <= 0)
                    {
                        Debug.Log("Hit!");
                        HM_Player.Damaged(1);
                        damageTimer += damageInterval;
                    }
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
    void ChangeExpanding()
    {
        isLaserExpanding = false;
    }
}
