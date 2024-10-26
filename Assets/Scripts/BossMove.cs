using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public float Duration;

    private float TimeElapsed = 0f;

    private void OnEnable()
    {
        StartPosition = new Vector3(0, 2.56f, 0);
    }
    void Start()
    {
        StartPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (!BossManager.instance.shakeing) 
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
    }
    float easeInOutSine(float x) {
        return -(Mathf.Cos(Mathf.PI* x) - 1) / 2;
    }
}
