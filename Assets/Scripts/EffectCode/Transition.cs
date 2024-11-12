using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public float Duration;
    public bool FadeIn;
    private float ElaplsedTime = 0;

    private void Start()
    {
        if (FadeIn) GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        else GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    void Update()
    {
        if (ElaplsedTime < Duration)
        {
            if (FadeIn)
            {
                ElaplsedTime += Time.deltaTime;
                float t = ElaplsedTime / Duration;
                t = Linear(t);
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, t);
            }
            else
            {
                ElaplsedTime += Time.deltaTime;
                float t = ElaplsedTime / Duration;
                t = Linear(t);
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1-t);
            }
        }
        else
        {
            Transition t = GetComponent<Transition>();
            Destroy(t);
        }
        float Linear(float t)
        {
            return t;
        }
    }
}
