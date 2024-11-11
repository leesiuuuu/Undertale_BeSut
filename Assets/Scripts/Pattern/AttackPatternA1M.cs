using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPatternA1M : MonoBehaviour
{
    public GameObject StartBarrier;
    public GameObject MagicJhin;

    public GameObject AtkObj;

    public float LoopTime1;
    public float SpawnDelay;

    public Vector2 BoxRange;
    public Vector2 BoxSize;

    public UICode UC;

    private float timer;
    private void OnEnable()
    {
        timer = 0;
        for(int i = 0; i < 2; i++)
        {
            GameObject Clone = Instantiate(MagicJhin);
            if(i == 0)
            {
                Clone.GetComponent<PosMove>().StartPos = new Vector3(-5.42f, 2.6f, 0);
                Clone.GetComponent<PosMove>().EndPos = new Vector3(-4.08f, 2.6f, 0);
            }
            else
            {
                Clone.GetComponent<PosMove>().StartPos = new Vector3(5.42f, 2.6f, 0);
                Clone.GetComponent<PosMove>().EndPos = new Vector3(4.08f, 2.6f, 0);
            }
        }
        Instantiate(StartBarrier);
        StartCoroutine(Pattern(LoopTime1, SpawnDelay));
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    IEnumerator Pattern(float LoopTime, float SpawnDelay)
    {
        yield return new WaitForSeconds(StartBarrier.GetComponent<PosMove>().Delay);
        while (timer < LoopTime)
        {
            timer += Time.deltaTime;
            GameObject Clone = Instantiate(AtkObj);
            float RandomX = Random.Range(-BoxSize.x/2, BoxSize.x/2);
            float RandomY = Random.Range(-BoxSize.y / 2, BoxSize.y / 2);
            Clone.transform.position = new Vector3(RandomX, BoxRange.y + RandomY, 0);
            Destroy(Clone, 1f);
            yield return new WaitForSeconds(SpawnDelay);
        }
        UC.MyTurnBack();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(BoxRange, BoxSize);
    }
}
