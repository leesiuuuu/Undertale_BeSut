using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPatternA3M : MonoBehaviour
{
    public GameObject BtmAtker;
    public GameObject TopAtker;
    private void OnEnable()
    {
        StartCoroutine(PatternStarter());
    }
    IEnumerator PatternStarter()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
