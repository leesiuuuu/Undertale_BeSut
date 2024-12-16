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
        float y = 0f;
        float x = 0f;
        for (int i = 0; i <= 720; i += 5)
        {
            Vector3 pos = new Vector3(
                x,
                y,
                Last.transform.position.z);
            if (i < 180)
            {
                x -= 0.00878f;
                y -= 0.0472f;
            }
            else
            {
                x += 0.00878f;
                y += 0.0472f;
            }
            Instantiate(Last, pos, Quaternion.Euler(0, 0, i));
            yield return new WaitForSeconds(0.3f);
        }
    }

}
