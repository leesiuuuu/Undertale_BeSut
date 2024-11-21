using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShawSpawn : MonoBehaviour
{
    public float StartY;
    public Vector3 StartScale;
    [Space(20f)]
    public float EndY;
    public Vector3 EndScale;
    [Space(20f)]
    public float Duration;
    public float EndDuration;
    [Space(20f)]
    public float Delay;

    private float Delaytime = 0f;

    public AudioClip AtkSound;

    private float TimeElapsed = 0f;
    private bool Donw = false;
    private void Start()
    {
        SoundManager.instance.SFXPlay("AtkSound", AtkSound);
    }
    private void Update()
    {
        if (Delaytime <= Delay)
        {
            Delaytime += Time.deltaTime;
        }
        else
        {
            TimeElapsed += Time.deltaTime;
        }

        if (TimeElapsed < (Donw == false ? Duration : EndDuration))
        {
            float t = TimeElapsed / (Donw == false ? Duration : EndDuration);
            t = EaseOutQuint(t);
            transform.position = Vector2.Lerp(
                new Vector2(transform.position.x, StartY),
                new Vector2(transform.position.x, EndY), t);
            transform.localScale = Vector3.Lerp(StartScale, EndScale, t);
        }
        if (TimeElapsed > Duration + 0.5f)
        {
            TimeElapsed = 0f;
            EndY = StartY;
            EndScale = StartScale;
            StartY = transform.position.y;
            StartScale = transform.localScale;
            Destroy(gameObject, 0.5f);
        }
    }
    float EaseOutQuint(float x)
    {
        return 1 - Mathf.Pow(1 - x, 5);
    }
    float Linear(float x)
    {
        return x;
    }
}
