using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatternA3M : MonoBehaviour
{
    public GameObject BtmAtker;
    public GameObject TopAtker;
    public float DelayTime;
    public UICode UC;
    private void OnEnable()
    {
        StartCoroutine(PatternStarter());
    }
    IEnumerator PatternStarter()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 10; i++)
        {
            Instantiate(BtmAtker);
            Instantiate(TopAtker);
            yield return new WaitForSeconds(DelayTime);
        }
        yield return new WaitForSeconds(0.8f);
        GameObject Clone = Instantiate(Random.value > 0.5 ? BtmAtker : TopAtker);
        Clone.GetComponent<PosMove>().Duration = 0.5f;
        yield return new WaitForSeconds(0.5f);
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }
}
