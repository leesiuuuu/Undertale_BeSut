using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatternA6M : MonoBehaviour
{
    public GameObject BtmAtker;
    public GameObject TopAtker;
    public GameObject LeftAtker;
    public GameObject RightAtker;
    public float DelayTime;
    public UICode UC;
    private void OnEnable()
    {
        StartCoroutine(PatternStarter());
    }
    IEnumerator PatternStarter()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        GameObject clone = Instantiate(BtmAtker);
                        yield return new WaitForSeconds(1f / (i+1));
                        break;
                    case 1:
                        GameObject clone2 = Instantiate(RightAtker);
                        clone2.GetComponent<PosMove>().Delay = 0.2f;
                        yield return new WaitForSeconds(1f / (i + 1));
                        break;
                    case 2:
                        GameObject clone3 = Instantiate(TopAtker);
                        clone3.GetComponent<PosMove>().Delay = 0.2f;
                        yield return new WaitForSeconds(1f / (i + 1));
                        break;
                    case 3:
                        GameObject clone4 = Instantiate(LeftAtker);
                        clone4.GetComponent<PosMove>().Delay = 0.3f;
                        yield return new WaitForSeconds(1f / (i + 1));
                        break;

                }
                
            }
            yield return new WaitForSeconds(DelayTime * 2);
        }
        yield return new WaitForSeconds(0.8f);
        GameObject Clone = Instantiate(Random.value > 0.5 ? BtmAtker : TopAtker);
        Clone.GetComponent<PosMove>().Duration = 0.5f;
        yield return new WaitForSeconds(0.5f);
        this.enabled = false;
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }
}
