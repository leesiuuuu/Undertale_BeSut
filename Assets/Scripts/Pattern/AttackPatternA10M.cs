using System.Collections;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class AttackPatternA10M : MonoBehaviour
{
    [SerializeField]
    //private GameObject BigLaser;

    private UICode UC;

    private void OnEnable()
    {
        StartCoroutine(Pattern10());
    }
    IEnumerator Pattern10()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(UC.BossAttack("이제 끝이다.", "더 이상 살려둘 이유는 없다.", "죽어라."));
    }
}
