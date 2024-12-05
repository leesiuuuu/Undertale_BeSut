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
        StartCoroutine(UC.BossAttack("���� ���̴�.", "�� �̻� ����� ������ ����.", "�׾��."));
    }
}
