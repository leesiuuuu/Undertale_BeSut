using System.Collections;
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

    private GameObject Jhin1;
    private GameObject Jhin2;

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
                Jhin1 = Clone;
            }
            else
            {
                Clone.GetComponent<PosMove>().StartPos = new Vector3(5.42f, 2.6f, 0);
                Clone.GetComponent<PosMove>().EndPos = new Vector3(4.08f, 2.6f, 0);
                Jhin2 = Clone;
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
        yield return new WaitForSeconds(StartBarrier.GetComponent<PosMove>().Delay + 0.5f);
        
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
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Jhin1Add());
        StartCoroutine(Jhin2Add());
        yield return new WaitForSeconds(0.8f);
        UC.MyTurnBack();

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(BoxRange, BoxSize);
    }
    IEnumerator Jhin1Add()
    {
        Jhin1.AddComponent<PosMove>();
        Jhin1.GetComponent<PosMove>().StartPos = new Vector2(-4.08f, 2.6f);
        Jhin1.GetComponent<PosMove>().EndPos = new Vector2(-5.42f, 2.6f);
        Jhin1.GetComponent<PosMove>().Duration = 0.8f;
        Jhin1.GetComponent<PosMove>().Delete = true;
        Jhin1.GetComponent<PosMove>().ease = PosMove.Ease.easeOutQuad;

        Jhin1.AddComponent<Transition>();
        Jhin1.GetComponent<Transition>().Duration = 0.5f;
        Jhin1.GetComponent<Transition>().FadeIn = false;
        yield return null;
    }
    IEnumerator Jhin2Add()
    {
        Jhin2.AddComponent<PosMove>();
        Jhin2.GetComponent<PosMove>().StartPos = new Vector2(4.08f, 2.6f);
        Jhin2.GetComponent<PosMove>().EndPos = new Vector2(5.42f, 2.6f);
        Jhin2.GetComponent<PosMove>().Duration = 0.8f;
        Jhin2.GetComponent<PosMove>().Delete = true;
        Jhin2.GetComponent<PosMove>().ease = PosMove.Ease.easeOutQuad;

        Jhin2.AddComponent<Transition>();
        Jhin2.GetComponent<Transition>().Duration = 0.5f;
        Jhin2.GetComponent<Transition>().FadeIn = false;
        yield return null;
    }
    IEnumerator BarrierDisappear()
    {
        StartBarrier.AddComponent<PosMove>();
        StartBarrier.GetComponent<PosMove>().StartPos = new Vector2(-0.06f, -1.19f);
        StartBarrier.GetComponent<PosMove>().EndPos = new Vector3(-0.06f, -8,47f);
        StartBarrier.GetComponent<PosMove>().Duration = 0.3f;
        StartBarrier.GetComponent<PosMove>().Delay = 1.2f;
        StartBarrier.GetComponent<PosMove>().ease = PosMove.Ease.easeOutQuart;
        
        yield return null;
    }
}
