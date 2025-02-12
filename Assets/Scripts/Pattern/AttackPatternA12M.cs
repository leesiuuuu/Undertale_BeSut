using System.Collections;
using UnityEngine;

public class AttackPatternA12M : MonoBehaviour
{
    public GameObject LaserPortal;
    public GameObject Lines;

    [SerializeField]
    private GameObject Damager;

    public AudioClip AC;


    private bool OnOff = false;
    public UICode UC;
    private float[] YPos = new float[3];
    private float[] XPos = new float[2];
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
        OnOff = false;
        StartCoroutine(Pattern12());
    }
    IEnumerator Pattern12()
    {
        GameObject n = Instantiate(Lines);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Pattern12_1());
        StartCoroutine(Pattern12_2());
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
    IEnumerator Pattern12_1()
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
            yield return new WaitForSeconds(Random.Range(0.1f, 1f));
            Before = RandomNum;
        }
    }
    IEnumerator Pattern12_2()
    {
        int Before = -1;
        int RandomNum = Random.Range(0, 3);
        float[] BoxX = {-1, 0, 1};
        while (!OnOff)
        {
            while (RandomNum == Before)
            {
                RandomNum = Random.Range(0, 3);
            }
            SoundManager.instance.SFXPlay("Create", AC);
            GameObject Clone = Instantiate(Damager, new Vector3(BoxX[RandomNum], 0, 0), Quaternion.identity);
            Clone.GetComponent<AttackPatternA5>().Rotate = 90;
            Clone.GetComponent<AttackPatternA5>().SpawnTime = 0.3f;
            Clone.GetComponent<AttackPatternA5>().DeSpawnTime = 0.3f;
            Clone.GetComponent<AttackPatternA5>().MaintainTime = 0.3f;
            yield return new WaitForSeconds(1.5f);
            Before = RandomNum;
        }
    }
}
