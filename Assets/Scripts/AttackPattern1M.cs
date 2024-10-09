using System.Collections;
using UnityEngine;

public class AttackPattern1M : MonoBehaviour
{
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

    public GameObject Damager;

    public AudioClip Atk1;
    public UICode UC;

    public float MAX;
    public float MIN;

    private float DelayTime;
    private bool isCreateDone = false;
    private void OnEnable()
    {
        MIN = 0.3f;
        MAX = 1f;
        isCreateDone = false;
        StartCoroutine(TimerSet(Duration));
        StartCoroutine(StartAttack());
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
        yield return new WaitForSeconds(2f);
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
        Gizmos.DrawWireCube(Pos1_1, Box1_1Range);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Pos2, Box2Range);
        Gizmos.DrawWireCube(Pos2_1, Box2_1Range);
    }
    void ObjectCreate(Vector2 Range, Vector3 Pos)
    {
        float BoxX = Random.Range((Range.x / 2) * -1, Range.x / 2);
        float BoxY = Random.Range((Range.y / 2) * -1, Range.y / 2);

        Vector3 RandomPos = Pos + new Vector3(BoxX, BoxY, 0);

        SoundManager.instance.SFXPlay("Atk1", Atk1);
        GameObject clone = Instantiate(Damager, RandomPos, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().gravityScale = 0;
  
        clone.GetComponent<AttackPattern1>().target = transform;
    }
}
