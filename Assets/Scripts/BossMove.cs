using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public float Duration;

    private float TimeElapsed = 0f;
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration; // 0에서 1까지의 시간 비율을 계산하는 코드
            t = easeInOutSine(t);
            transform.position = Vector2.Lerp(StartPosition, EndPosition, t);
        }
        else
        {
            TimeElapsed = 0f;
            EndPosition = StartPosition;
            StartPosition = transform.position;
        }
    }
    float easeOutQuad(float x) {
        return 1f - (1f - x) * (1f - x);
    }
    float easeInQuad(float x) {
        return x* x;
    }
    float Linear(float x)
    {
        return x;
    }
    float easeInOutSine(float x) {
        return -(Mathf.Cos(Mathf.PI* x) - 1) / 2;
    }
}
