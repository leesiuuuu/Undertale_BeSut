using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    public AchievementSO ASO;
    public Text Title;
    public Text Description;
    public Image icon;

    private float Duration = 1f;
    private float TimeElapsed = 0f;
    void Start()
    {
        Title.text = ASO.Title;
        Description.text = ASO.Description;
        icon.sprite = ASO.Icon;
    }

    // Update is called once per frame
    void Update()
    {
        while(TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration;
            t = Linear(t);

        }
    }
    float Linear(float x)
    {
        return x;
    }
}
