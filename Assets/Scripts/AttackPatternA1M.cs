using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatternA1M : MonoBehaviour
{
    public GameObject StartBarrier;
    public GameObject MagicJhin;
    private void OnEnable()
    {
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
        StartCoroutine(Pattern());
    }
    IEnumerator Pattern()
    {
        yield return new WaitForSeconds(StartBarrier.GetComponent<PosMove>().Delay);
        //패턴 시작
    }
}
