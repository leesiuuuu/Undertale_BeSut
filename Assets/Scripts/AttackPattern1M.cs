using System.Collections;
using UnityEngine;

public class AttackPattern1M : MonoBehaviour
{
    [SerializeField]
    private Vector2 Box1Range;
    [SerializeField]
    private Vector3 Pos1;
    [SerializeField]
    private Vector2 Box2Range;
    [SerializeField]
    private Vector3 Pos2;
    [SerializeField]
    private float Duration;
    private bool Up;

    public GameObject Damager;


    public UICode UC;


    private float DelayTime;
    private bool isCreateDone = false;
    private void OnEnable()
    {
        isCreateDone = false;
    }

    private void Start()
    {
        StartCoroutine(StartAttack());
        StartCoroutine(TimerSet(Duration));
    }
    IEnumerator StartAttack()
    {
        while (!isCreateDone)
        {
            DelayTime = Random.Range(0.1f, 0.8f);
            Up = (Random.value > 0.5);
            yield return new WaitForSeconds(DelayTime);
            if (Up)
            {
                float UpBoxX = Random.Range((Box1Range.x / 2) * -1, Box1Range.x / 2);
                float UpBoxY = Random.Range((Box1Range.y / 2) * -1, Box1Range.y / 2);
                Vector3 RandomPos = new Vector3(UpBoxX, UpBoxY, 0);
                //사운드 재생
                GameObject clone = Instantiate(Damager, RandomPos, Quaternion.identity);
                clone.GetComponent<AttackPattern1>().target = transform;
            }
            else
            {
                float DownBoxX = Random.Range((Box2Range.x / 2) * -1, Box2Range.x / 2);
                float DownBoxY = Random.Range((Box2Range.y / 2) * -1, Box2Range.y / 2);
                Vector3 RandomPos = new Vector3(DownBoxX, DownBoxY, 0);
                //사운드 재생
                GameObject clone = Instantiate(Damager, RandomPos, Quaternion.identity);
                clone.GetComponent<Rigidbody2D>().gravityScale = -1;
                clone.GetComponent<AttackPattern1>().target = transform;
            }
        }
        yield return new WaitForSeconds(1.75f);
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }
    IEnumerator TimerSet(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        isCreateDone = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Pos1, Box1Range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Pos2, Box2Range);
    }
}
