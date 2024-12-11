using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPatternA4M : MonoBehaviour
{
    public GameObject LaserPortal;
    public GameObject Lines;

    public int SpawnRandom1;
    public int SpawnRandom2;
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
        StartCoroutine(Pattern4());
    }
    IEnumerator Pattern4()
    {
        GameObject n = Instantiate(Lines);
        yield return new WaitForSeconds(0.5f);
        int RandomCount = Random.Range(SpawnRandom1, SpawnRandom2+1);
        for(int i = 0; i < RandomCount; i++)
        {
            int RandomNum = Random.Range(0, 3);
            GameObject Clone = Instantiate(LaserPortal);
            Clone.GetComponent<PosMove>().EndPos.y = YPos[RandomNum];
            yield return new WaitForSeconds(0.5f);
            Clone.GetComponent<PosMove>().StartPos.y = YPos[RandomNum];
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(0.9f);
        Destroy(n);
        this.enabled = false;
        StateManager.instance.Fighting = false;
        PatternManager.instance.isSpeicalMove = false;
        UC.MyTurnBack();
    }
}
