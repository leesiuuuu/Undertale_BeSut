using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public float Duration;
    private float ElaplsedTime = 0;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
    void Update()
    {
        if(ElaplsedTime < Duration)
        {
            ElaplsedTime += Time.deltaTime;
            float t = ElaplsedTime / Duration;
            t = Linear(t);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, t);
        }
    }
    float Linear(float t)
    {
        return t;
    }
}
