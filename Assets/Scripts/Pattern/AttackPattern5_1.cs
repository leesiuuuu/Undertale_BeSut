using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackPattern5_1 : MonoBehaviour
{
    public Vector2 StartPos;
    public Vector3 StartScale;
    [Space(20f)]
    public Vector2 EndPos;
    public Vector3 EndScale;
    [Space(20f)]
    public float Duration;
    public AudioClip AtkSound;

    private float TimeElapsed = 0f;
    private bool Donw = false;
    private void Start()
    {
        SoundManager.instance.SFXPlay("AtkSound", AtkSound);
    }
    private void Update()
    {
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed < (Donw == false ? Duration : 1f))
        {
            float t = TimeElapsed / (Donw == false ? Duration : 1f);
            t = EaseOutQuint(t);
            transform.position = Vector2.Lerp(StartPos, EndPos, t);
            transform.localScale = Vector3.Lerp(StartScale, EndScale, t);
        }
        if(TimeElapsed > Duration + 0.5f)
        {
            TimeElapsed = 0f;
            EndPos = StartPos;
            EndScale = StartScale;
            StartPos = transform.position;
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
