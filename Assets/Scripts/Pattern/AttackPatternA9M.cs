using System.Collections;
using UnityEngine;

public class AttackPatternA9M : MonoBehaviour
{
    [SerializeField]
    private GameObject AtkObj;
    [SerializeField]
    private GameObject Player;


    public GameObject LaserPortal;
    public GameObject Lines;

    public int SpawnRandom1;
    public int SpawnRandom2;

    public AudioClip SpawnSound;

    public UICode UC;

    [SerializeField]
    private Vector2 BoxSize;
    [SerializeField]
    private Vector3 BoxPos;

    private bool OnOff = false;

    private float[] YPos = new float[3];
    private void Start()
    {
        YPos[0] = -0.67f;
        YPos[1] = -1.37f;
        YPos[2] = -2.07f;
    }

    private void OnEnable()
    {
        StartCoroutine(Pattern9());
    }
    IEnumerator Pattern9()
    {
        GameObject n = Instantiate(Lines);
        yield return new WaitForSeconds(0.5f);
        int RandomCount = Random.Range(SpawnRandom1, SpawnRandom2 + 1);
        StartCoroutine(Pattenr9_1());
        for (int i = 0; i < RandomCount; i++)
        {
            int RandomNum = Random.Range(0, 3);
            GameObject Clone = Instantiate(LaserPortal);
            Clone.GetComponent<PosMove>().EndPos.y = YPos[RandomNum];
            yield return new WaitForSeconds(0.5f);
            Clone.GetComponent<PosMove>().StartPos.y = YPos[RandomNum];
            yield return new WaitForSeconds(2f);
        }
        OnOff = true;
        StopCoroutine(Pattenr9_1());
        yield return new WaitForSeconds(1f);
        Destroy(n);
        //³¡
        this.enabled = false;
        StateManager.instance.Fighting = false;
        PatternManager.instance.isSpeicalMove = false;
        UC.MyTurnBack();
    }
    IEnumerator Pattenr9_1()
    {
        while(OnOff != true)
        {
            float BoxX = Random.Range((BoxSize.x / 2) * -1, BoxSize.x / 2);
            float BoxY = Random.Range((BoxSize.y / 2) * -1, BoxSize.y / 2);

            Vector3 RandomPos = BoxPos + new Vector3(BoxX, BoxY, 0);

            SoundManager.instance.SFXPlay("SpawnSound", SpawnSound);
            GameObject clone = Instantiate(AtkObj, RandomPos, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().gravityScale = 0.3f;

            clone.GetComponent<AttackPattern1>().target = Player.transform;
            yield return new WaitForSeconds(Random.Range(0.8f, 1.5f));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(BoxPos, BoxSize);
    }
}
