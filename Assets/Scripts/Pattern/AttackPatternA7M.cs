using System.Collections;
using UnityEngine;

public class AttackPatternA7M : MonoBehaviour
{
    public GameObject LaserPortal;
    public GameObject Lines;
    public GameObject AtkObj;

    public int SpawnRandom1;
    public int SpawnRandom2;
    public float ObjSpawnDelay;

    public Vector2 BoxRange;
    public Vector2 BoxSize;

    private bool OnOff = false;
    public UICode UC;
    private float[] YPos = new float[3];
    private void Start()
    {
        YPos[0] = -0.67f;
        YPos[1] = -1.37f;
        YPos[2] = -2.07f;
    }
    private void OnEnable()
    {
        OnOff = false;
        StartCoroutine(Pattern7());
    }
    IEnumerator Pattern7()
    {
        GameObject n = Instantiate(Lines);
        yield return new WaitForSeconds(0.5f);
        int RandomCount = Random.Range(SpawnRandom1, SpawnRandom2 + 1);
        StartCoroutine(Pattern7_1());
        for (int i = 0; i < RandomCount; i++)
        {
            int RandomNum = Random.Range(0, 3);
            GameObject Clone = Instantiate(LaserPortal);
            Clone.GetComponent<PosMove>().EndPos.y = YPos[RandomNum];
            yield return new WaitForSeconds(0.7f);
            Clone.GetComponent<PosMove>().StartPos.y = YPos[RandomNum];
            yield return new WaitForSeconds(2f);
        }
        OnOff = true;
        StopCoroutine(Pattern7_1());
        yield return new WaitForSeconds(0.9f);
        Destroy(n);
        this.enabled = false;
        StateManager.instance.Fighting = false;
        PatternManager.instance.isSpeicalMove = false;
        UC.MyTurnBack();
        yield break;
    }
    IEnumerator Pattern7_1()
    {
        while(OnOff == false)
        {
            GameObject Clone = Instantiate(AtkObj);
            float RandomX = Random.Range(-BoxSize.x / 2, BoxSize.x / 2);
            float RandomY = Random.Range(-BoxSize.y / 2, BoxSize.y / 2);
            Clone.transform.position = new Vector3(RandomX, BoxRange.y + RandomY, 0);
            Destroy(Clone, 1f);
            yield return new WaitForSeconds(ObjSpawnDelay);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(BoxRange, BoxSize);
    }
}
