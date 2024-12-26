using System.Collections;
using UnityEngine;

public class AttackPatternA15M : MonoBehaviour
{
    public UICode UC;

    private bool[] flag = new bool[20];

    [SerializeField]
    private GameObject Square_A5;
    [SerializeField]
    private AudioClip SquareCreateSound;
    [Space(20)]
    [Header("PA14")]
    [SerializeField]
    private GameObject LaserPortal_Big;
    //��ġ ���� �迭
    private Vector3[] Pos = new Vector3[4];
    [Space(20)]
    [SerializeField]
    private GameObject Left;
    [SerializeField]
    private GameObject Right;
    [SerializeField]
    private GameObject Up;
    [SerializeField]
    private GameObject Down;
    [Space(20)]
    [Header("P1")]
    //�������� ��Ⱥ�� ��ġ ���� ����
    [SerializeField]
    private Vector2 Box1Range;
    [SerializeField]
    private Vector3 Pos1;
    [SerializeField]
    private Vector2 Box1_1Range;
    [SerializeField]
    private Vector3 Pos1_1;
    [SerializeField]
    private Vector2 Box2Range;
    [SerializeField]
    private Vector3 Pos2;
    [SerializeField]
    private Vector2 Box2_1Range;
    [SerializeField]
    private Vector3 Pos2_1;

    //���� �ð�
    public float Duration;
    public bool isCreateDone = false;

    //��, �Ʒ�, ����, ������ ��ġ ���� bool
    private bool UD;
    private bool LR;

    //�÷��̾� Ÿ����
    [SerializeField]
    private Transform PlayerTransform;

    //�÷��̾� ���� ���� ������Ʈ
    public GameObject PlayerFollowSprite;

    public AudioClip PFSCreateSound;

    public float MAX;
    public float MIN;

    private float DelayTime;
    [Space(20)]
    [Header("PA11")]

    [SerializeField]
    private Vector2 BoxRange;

    [Space(20)]
    [Header("PA2")]

    public GameObject AtkObj;
    public GameObject SafeZoneEffect;

    float[] PosArray = new float[8];

    [SerializeField]
    private Vector3 XRangePos1;
    [SerializeField] private float X1;

    [Space(20)]
    [Header("P4")]
    public GameObject Effect;
    public GameObject AttackObj;
    public GameObject Plaeyr;
    public int RepeatCount;

    public AudioClip Obj4Se;

    //����Ʈ ���� ��ġ
    Vector3 UpPoint = new Vector3(0, 0, 0);
    Vector3 DownPoint = new Vector3(0, -2.6f, 0);
    Vector3 LeftPoint = new Vector3(-1.8f, -1.3f, 0);
    Vector3 RightPoint = new Vector3(1.8f, -1.3f, 0);

    int LoopTime;
    float[] Times;
    int[] dir;

    //���� �� ������ �� Ȯ��
    public static int LastRotate;

    protected int dirNum = 0;

    [Space(20)]
    [Header("PA12")]

    private float[] YPos = new float[3];
    private float[] XPos = new float[2];

    [SerializeField]
    private GameObject LaserPortal_Small;

    [Space(20)]
    [Header("P5")]
    [SerializeField]
    private float X;
    [SerializeField]
    private Vector3 XRangePos;
    private float Y = -2.171f;

    public GameObject Warning;
    public GameObject AtkObj_A5;

    public AudioClip WarningS;

    public float MAX_P5;
    public float MIN_P5;

    [Space(20)]
    [Header("LastP")]
    [SerializeField]

    private GameObject Last;



    private void OnEnable()
    {
        YPos[0] = -0.67f;
        YPos[1] = -1.37f;
        YPos[2] = -2.07f;
        XPos[0] = -4.22f;
        XPos[1] = 4.22f;

        for (int i = 0; i < flag.Length; i++)
        {
            flag[i] = false;
        }
        MIN = 0.5f;
        MAX = 2f;
        isCreateDone = false;

        Plaeyr = GameObject.Find("Heart");
        PlayerTransform = Plaeyr.transform;


        Pos[0] = new Vector3(4.6f, -1.3f, 0); //����
        Pos[1] = new Vector3(-4.6f, -1.3f, 0); //������
        Pos[2] = new Vector3(0, 2.4f, 0); //��
        Pos[3] = new Vector3(0, -3.9f, 0); //�Ʒ�

        MAX_P5 = 1f;
        MIN_P5 = 0.2f;
        StartCoroutine(Pattern15());
    }
    IEnumerator Pattern15()
    {
        yield return new WaitForSeconds(0.5f);
        /*
        if (StateManager.instance.NormalFaze2)
        {
            StartCoroutine(UC.BossAttack(2, "���̴�.",
            "�� ��� ���� ��ƺξ��ָ�."));
        }
        else if(StateManager.instance.BetrayalFaze2)
        {
            StartCoroutine(UC.BossAttack(2, "���� ������.",
                "�ʵ� ������ �����ݾ�? �ȱ׷�?"));
        }
        */
        /*
        StateManager.instance.Last = true;
        Plaeyr.GetComponent<HeartMove>().NoCool = true;
        Plaeyr.GetComponent<HeartMove>().Pattern3Start = true;
        yield return new WaitForSeconds(5f);
        //���� ��¦ �����ϰ� ���ִ� �ڵ� �߰�
        StartCoroutine(LaserBoom());
        StartCoroutine(Pattern12_2());
        yield return new WaitForSeconds(10f);
        flag[1] = true;
        StartCoroutine(StartAttack());
        yield return new WaitForSeconds(10f);
        StartCoroutine(SquareAppearence());
        isCreateDone = true;
        yield return new WaitForSeconds(5f);
        flag[0] = true;
        flag[6] = true;
        yield return new WaitForSeconds(4f);
        StartCoroutine(AtkPtn41());
        */
        StateManager.instance.Last = true;
        this.enabled = false;
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
        yield break;
    }
    //ū ������ �߻�
    IEnumerator LaserBoom()
    {
        int Before = -1;
        int RandomNum = Random.Range(0, 4);
        while (!flag[0])
        {
            while (RandomNum == Before)
            {
                RandomNum = Random.Range(0, 4);
            }
            Quaternion Rotate = Quaternion.Euler(0, 0, 0);
            switch (RandomNum)
            {
                case 0:
                    Rotate = Quaternion.Euler(0, 0, 0);
                    break;
                case 1:
                    Rotate = Quaternion.Euler(0, 0, 180);
                    break;
                case 2:
                    Rotate = Quaternion.Euler(0, 0, 90);
                    break;
                case 3:
                    Rotate = Quaternion.Euler(0, 0, -90);
                    break;
            }
            GameObject Clone = Instantiate(LaserPortal_Big, Pos[RandomNum], Rotate);

            Laser_Ex LE = Clone.GetComponentInChildren<Laser_Ex>();
            switch (RandomNum)
            {
                case 0: LE.XYCheck = 1; break;
                case 1: LE.XYCheck = 1; break;
                case 2: LE.XYCheck = 0; break;
                case 3: LE.XYCheck = 0; break;
            }
            yield return new WaitForSeconds(3.5f);
            Transition t = Clone.transform.Find("Laser").gameObject.AddComponent<Transition>();
            t.Duration = 1f;
            Destroy(Clone, 1f);
            Before = RandomNum;
        }
        yield break;
    }
    //���� ������ �����ϰ� ��Ÿ��
    IEnumerator Pattern12_1()
    {
        int Before = -1;
        int RandomNum = Random.Range(0, 3);
        while (!flag[6])
        {
            while (RandomNum == Before)
            {
                RandomNum = Random.Range(0, 3);
            }
            bool LR = (Random.value < 0.5f);
            Quaternion Rotate = LR ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 0, 180);
            GameObject Clone = Instantiate(LaserPortal_Small);
            Clone.transform.rotation = Rotate;
            Clone.GetComponent<PosMove>().StartPos.x = LR ? XPos[1] : XPos[0];
            Clone.GetComponent<PosMove>().EndPos = new Vector2(LR ? XPos[1] : XPos[0], YPos[RandomNum]);
            yield return new WaitForSeconds(0.7f);
            Clone.GetComponent<PosMove>().StartPos = new Vector2(LR ? XPos[1] : XPos[0], YPos[RandomNum]);
            Clone.GetComponent<PosMove>().EndPos.x = LR ? XPos[1] : XPos[0];
            yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
            Before = RandomNum;
        }
    }
    //������ ���� ������Ʈ ������ ������ Ư�� ��ġ���� ��ȯ
    IEnumerator Pattern12_2()
    {
        int Before = -1;
        int RandomNum = Random.Range(0, 3);
        float[] BoxX = { -1, 0, 1 };
        while (!flag[1])
        {
            while (RandomNum == Before)
            {
                RandomNum = Random.Range(0, 3);
            }
            SoundManager.instance.SFXPlay("Create", SquareCreateSound);
            GameObject Clone = Instantiate(Square_A5, new Vector3(BoxX[RandomNum], 0, 0), Quaternion.identity);
            Clone.GetComponent<AttackPatternA5>().Rotate = 90;
            Clone.GetComponent<AttackPatternA5>().SpawnTime = 0.3f;
            Clone.GetComponent<AttackPatternA5>().DeSpawnTime = 0.3f;
            Clone.GetComponent<AttackPatternA5>().MaintainTime = 0.3f;
            yield return new WaitForSeconds(1.5f);
            Before = RandomNum;
        }
        yield break;
    }
    //�÷��̾ ���󰡸鼭 �����ϴ� ��������Ʈ ��ȯ
    IEnumerator StartAttack()
    {
        while (!isCreateDone)
        {
            DelayTime = Random.Range(MIN, MAX);
            UD = (Random.value > 0.5);
            LR = (Random.value > 0.5);
            yield return new WaitForSeconds(DelayTime);
            if (UD)
            {
                if (LR) ObjectCreate(Box1Range, Pos1);
                else ObjectCreate(Box1_1Range, Pos1_1);
            }
            else
            {
                if (LR) ObjectCreate(Box2Range, Pos2);
                else ObjectCreate(Box2_1Range, Pos2_1);
            }
            yield return null;
        }
        yield break;
    }
    IEnumerator TimerSet(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        isCreateDone = true;
        yield break;
    }
    void ObjectCreate(Vector2 Range, Vector3 Pos)
    {
        float BoxX = Random.Range((Range.x / 2) * -1, Range.x / 2);
        float BoxY = Random.Range((Range.y / 2) * -1, Range.y / 2);

        Vector3 RandomPos = Pos + new Vector3(BoxX, BoxY, 0);

        SoundManager.instance.SFXPlay("Atk1", PFSCreateSound);
        GameObject clone = Instantiate(PlayerFollowSprite, RandomPos, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().gravityScale = 0;

        clone.GetComponent<AttackPattern1>().target = PlayerTransform;
    }
    //����/���� �̵� ������Ʈ ���� ����
    IEnumerator Attacker()
    {
        int before = -1;
        int RandomNum = Random.Range(0, 4);
        while (!flag[3])
        {
            while (RandomNum == before)
            {
                RandomNum = Random.Range(0, 4);
            }
            switch (RandomNum)
            {
                case 1: Instantiate(Left); break;
                case 2: Instantiate(Right); break;
                case 3: Instantiate(Up); break;
                case 4: Instantiate(Down); break;
            }
            before = RandomNum;
            yield return new WaitForSeconds(Random.Range(0.4f, 0.7f));
        }
    }
    //������ ���� ������Ʈ�� ������ ��ġ�� �������� ��ȯ
    IEnumerator SquareAppearence()
    {
        while (!flag[4])
        {
            float BoxX = Random.Range((BoxRange.x / 2) * -1, BoxRange.x / 2);
            float BoxY = Random.Range((BoxRange.y / 2) * -1, BoxRange.y / 2);
            Vector3 RandomPosition = transform.position + new Vector3(BoxX, BoxY, 0);
            SoundManager.instance.SFXPlay("Create", SquareCreateSound);
            GameObject Clone = Instantiate(Square_A5, RandomPosition, Quaternion.identity);
            Clone.GetComponent<AttackPatternA5>().Rotate = Random.Range(0f, 360f);
            Clone.GetComponent<AttackPatternA5>().SpawnTime = 0.3f;
            Clone.GetComponent<AttackPatternA5>().DeSpawnTime = 0.3f;
            Clone.GetComponent<AttackPatternA5>().MaintainTime = 0.3f;
            yield return new WaitForSeconds(1.5f);

        }
    }
    //������ ��ġ�� Ư�� Ÿ�ֿ̹� ���� �̵�(�߰��� ���� ���� ���� �ڵ� �߰�)
    IEnumerator AtkPtn41()
    {
        LoopTime = (int)Random.Range(4, 6);
        Times = new float[LoopTime];
        dir = new int[LoopTime];
        for (int n = 0; n < RepeatCount; n++)
        {
            for (int i = 0; i < LoopTime; i++)
            {
                float RandomTime = Random.Range(0.3f, 0.7f);
                Times[i] = RandomTime;
                RotateInit();
                dir[i] = dirNum;
                yield return new WaitForSeconds(RandomTime);
            }
            for (int j = 0; j < Times.Length; j++)
            {
                LastRotate = dir[j];
                Debug.Log(LastRotate);
                GameObject Obj = Instantiate(AttackObj);
                Obj.GetComponent<AttackPattern4>().RotateSetting(LastRotate);
                yield return new WaitForSeconds(Times[j]);
            }
            yield return new WaitForSeconds(1.2f);
        }
        flag[2] = true;
        StartCoroutine(CreateDamager());
        yield break;
    }
    void RotateInit()
    {
        dirNum = (int)Random.Range(0, 4);
        GameObject Clone;
        switch (dirNum)
        {
            case 0:
                Clone = Instantiate(Effect, UpPoint, Quaternion.identity);
                SoundManager.instance.SFXPlay("Obj4Se", Obj4Se);
                Destroy(Clone, 0.2f);
                break;
            case 1:
                Clone = Instantiate(Effect, DownPoint, Quaternion.identity);
                SoundManager.instance.SFXPlay("Obj4Se", Obj4Se);
                Destroy(Clone, 0.2f);
                break;
            case 2:
                Clone = Instantiate(Effect, LeftPoint, Quaternion.identity);
                SoundManager.instance.SFXPlay("Obj4Se", Obj4Se);
                Destroy(Clone, 0.2f);
                break;
            case 3:
                Clone = Instantiate(Effect, RightPoint, Quaternion.identity);
                SoundManager.instance.SFXPlay("Obj4Se", Obj4Se);
                Destroy(Clone, 0.2f);
                break;
        }
    }
    //������ ��ġ���� �ھƿ����� ������Ʈ ��ȯ(���� ���� ���� �ڵ� �߰�)
    IEnumerator CreateDamager()
    {
        flag[6] = false;
        flag[4] = true;
        StartCoroutine(Pattern12_1());
        int RandomNum = Random.Range(15, 30);
        for (int i = 0; i < RandomNum; i++)
        {
            float RandomX = Random.Range((X / 2) * -1, X / 2);
            //���� ������Ʈ ��Ÿ���� �ϴ� �ڵ� �߰�
            Vector2 Pos = transform.position + new Vector3(RandomX, Y + 1.47f, 0);
            GameObject clone = Instantiate(Warning, Pos, Quaternion.identity);
            clone.GetComponent<AttackPattern5_2>().Nigg(RandomX, AtkObj, WarningS);
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        }
        yield return new WaitForSeconds(1f);
        flag[6] = true;
        flag[4] = false;
        flag[1] = false;
        StartCoroutine(Attacker());
        StartCoroutine(Pattern12_2());
        yield return new WaitForSeconds(10f);
        flag[3] = true;
        flag[4] = true;
        flag[1] = true;
        StartCoroutine(Ex_1());
        yield break;
    }
    //������ ����, ��� �������� ���� ������Ʈ�� �̵��Ѵ�.
    IEnumerator Ex_1()
    {
        BossManager.instance.StopMove();
        BossManager.instance.ChangeSprite(0);
        float y = 0f;
        float x = 0f;
        for (int i = 0; i <= 360; i += 5)
        {
            Vector3 pos = new Vector3(
                x,
                y,
                Last.transform.position.z);
            if (i < 180)
            {
                x -= 0.00878f;
                y -= 0.0472f;
            }
            else
            {
                x += 0.00878f;
                y += 0.0472f;
            }
            GameObject n = Instantiate(Last, pos, Quaternion.Euler(0, 0, i));
            Destroy(n, 2);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(3f);
        BossManager.instance.StopMove();
        BossManager.instance.ChangeSprite(0);
        this.enabled = false;
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
        yield break;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Pos1, Box1Range);
        Gizmos.DrawWireCube(Pos1_1, Box1_1Range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Pos2, Box2Range);
        Gizmos.DrawWireCube(Pos2_1, Box2_1Range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObject.transform.position, BoxRange);

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(XRangePos1, new Vector2(X1, 0.1f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(XRangePos, new Vector2(X, 0.1f));
    }
}
