using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AttackPatternA13M : MonoBehaviour
{
    public GameObject LaserPortal;
    public GameObject Lines;

    [SerializeField]
    private GameObject Damager_Main;

    public AudioClip AC;


    private bool OnOff = false;

    private float[] YPos = new float[3];
    private float[] XPos = new float[2];

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
    public float Duration;
    private bool UD;
    private bool LR;

    [SerializeField]
    private Transform PlayerTransform;

    public GameObject Damager;

    public AudioClip Atk1;

    public float MAX;
    public float MIN;

    private float DelayTime;
    private bool isCreateDone = false;

    public UICode UC;

    private void Start()
    {
        YPos[0] = -0.67f;
        YPos[1] = -1.37f;
        YPos[2] = -2.07f;
        XPos[0] = -4.22f;
        XPos[1] = 4.22f;
    }
    private void OnEnable()
    {
        MIN = 0.5f;
        MAX = 2f;
        isCreateDone = false;
        OnOff = false;
        StartCoroutine(Pattern13());
        StartCoroutine(TimerSet(Duration));
        StartCoroutine(StartAttack());
    }
    IEnumerator Pattern13()
    {
        GameObject n = Instantiate(Lines);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Pattern13_1());
        StartCoroutine(Pattern13_2());
        yield return new WaitForSeconds(20);
        OnOff = true;
        yield return new WaitForSeconds(3.5f);
        Destroy(n);
        this.enabled = false;
        StateManager.instance.Fighting = false;
        PatternManager.instance.isSpeicalMove = false;
        UC.MyTurnBack();
        yield break;
    }
    IEnumerator Pattern13_1()
    {
        int Before = -1;
        int RandomNum = Random.Range(0, 3);
        while (!OnOff)
        {
            while (RandomNum == Before)
            {
                RandomNum = Random.Range(0, 3);
            }
            bool LR = (Random.value < 0.5f);
            Quaternion Rotate = LR ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 0, 180);
            GameObject Clone = Instantiate(LaserPortal);
            Clone.transform.rotation = Rotate;
            Clone.GetComponent<PosMove>().StartPos.x = LR ? XPos[1] : XPos[0];
            Clone.GetComponent<PosMove>().EndPos = new Vector2(LR ? XPos[1] : XPos[0], YPos[RandomNum]);
            yield return new WaitForSeconds(0.7f);
            Clone.GetComponent<PosMove>().StartPos = new Vector2(LR ? XPos[1] : XPos[0], YPos[RandomNum]);
            Clone.GetComponent<PosMove>().EndPos.x = LR ? XPos[1] : XPos[0];
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            Before = RandomNum;
        }
    }
    IEnumerator Pattern13_2()
    {
        int Before = -1;
        int RandomNum = Random.Range(0, 3);
        float[] BoxX = { -1, 0, 1 };
        while (!OnOff)
        {
            while (RandomNum == Before)
            {
                RandomNum = Random.Range(0, 3);
            }
            SoundManager.instance.SFXPlay("Create", AC);
            GameObject Clone = Instantiate(Damager_Main, new Vector3(BoxX[RandomNum], 0, 0), Quaternion.identity);
            Clone.GetComponent<AttackPatternA5>().Rotate = 90;
            Clone.GetComponent<AttackPatternA5>().SpawnTime = 0.5f;
            Clone.GetComponent<AttackPatternA5>().DeSpawnTime = 0.5f;
            Clone.GetComponent<AttackPatternA5>().MaintainTime = 0.5f;
            yield return new WaitForSeconds(2.5f);
            Before = RandomNum;
        }
    }
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
        }
    }
    IEnumerator TimerSet(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        isCreateDone = true;
    }
    void ObjectCreate(Vector2 Range, Vector3 Pos)
    {
        float BoxX = Random.Range((Range.x / 2) * -1, Range.x / 2);
        float BoxY = Random.Range((Range.y / 2) * -1, Range.y / 2);

        Vector3 RandomPos = Pos + new Vector3(BoxX, BoxY, 0);

        SoundManager.instance.SFXPlay("Atk1", Atk1);
        GameObject clone = Instantiate(Damager, RandomPos, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().gravityScale = 0;

        clone.GetComponent<AttackPattern1>().target = PlayerTransform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Pos1, Box1Range);
        Gizmos.DrawWireCube(Pos1_1, Box1_1Range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Pos2, Box2Range);
        Gizmos.DrawWireCube(Pos2_1, Box2_1Range);
    }
}
