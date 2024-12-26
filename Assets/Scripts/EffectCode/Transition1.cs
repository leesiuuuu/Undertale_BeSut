using UnityEngine;
using UnityEngine.UI;

public class Transition1 : MonoBehaviour
{
    public float Duration;
    public bool FadeIn;
    private float ElaplsedTime = 0;

    private void Start()
    {
        if (FadeIn) GetComponent<Image>().color = new Color(0, 0, 0, 0);
        else GetComponent<Image>().color = new Color(0, 0, 0, 1);
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
                GetComponent<Image>().color = new Color(0, 0, 0, t);
            }
            else
            {
                ElaplsedTime += Time.deltaTime;
                float t = ElaplsedTime / Duration;
                t = Linear(t);
                GetComponent<Image>().color = new Color(0, 0, 0, 1-t);
            }
        }
        else
        {
            Transition1 t = GetComponent<Transition1>();
            Destroy(t);
        }
        float Linear(float t)
        {
            return t;
        }
    }
}
