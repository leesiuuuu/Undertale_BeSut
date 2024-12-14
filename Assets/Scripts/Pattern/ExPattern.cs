using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExPattern : MonoBehaviour
{
    public GameObject Last;
    void Start()
    {
        StartCoroutine(Ex_1());
    }
    IEnumerator Ex_1()
    {
        float n = 0f;
        for (int i = 0; i <= 360; i += 5)
        {
            Vector3 pos = new Vector3(
                Last.transform.position.x,
                n,
                Last.transform.position.z);
            if (i < 180) n -= 0.0472f;
            else n += 0.0472f;
            Instantiate(Last, pos, Quaternion.Euler(0, 0, i));
            yield return new WaitForSeconds(0.5f);
        }
    }

}
