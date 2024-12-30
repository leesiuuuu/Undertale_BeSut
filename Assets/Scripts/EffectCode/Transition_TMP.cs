using TMPro;
using UnityEngine;

public class Transition_TMP : MonoBehaviour
{
    public float Duration;
    public bool FadeIn;
    public float Delay;
    private float ElaplsedTime = 0;
    private float DelayTime = 0f;

    private void Start()
    {
        if (FadeIn) GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0);
        else GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1);
    }
    void Update()
    {
        DelayTime += Time.deltaTime;
        if(Delay < DelayTime)
        {
            if (ElaplsedTime < Duration)
            {
                if (FadeIn)
                {
                    ElaplsedTime += Time.deltaTime;
                    float t = ElaplsedTime / Duration;
                    t = Linear(t);
                    GetComponent<TMP_Text>().color = new Color(1, 1, 1, t);
                }
                else
                {
                    ElaplsedTime += Time.deltaTime;
                    float t = ElaplsedTime / Duration;
                    t = Linear(t);
                    GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1 - t);
                }
            }
            else
            {
                Transition_TMP t = GetComponent<Transition_TMP>();
                Destroy(t);
            }
        }
    }
    float Linear(float t)
    {
        return t;
    }
}
