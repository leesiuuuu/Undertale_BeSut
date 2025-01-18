using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    public AchievementSO ASO;
    public TMP_Text Title;
    public TMP_Text Description;
    public Image icon;
    private string key;

    private float Duration = 3f;
    private float EndDuration = 1.2f;
    private float TimeElapsed = 0f;

    private RectTransform RT;
    private Vector3 EndRT;
    private Vector3 StartRT;
    void Awake()
    {
        Title.text = ASO.Title;
        Description.text = ASO.Description;
        icon.sprite = ASO.Icon;
        RT = GetComponent<RectTransform>();
        key = ASO.key;

        StartRT = RT.anchoredPosition;
        EndRT = new Vector3(RT.anchoredPosition.x, 33.405f, 0f);
        StartCoroutine(Appear());
    }
    IEnumerator Appear()
    {
        while (TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration;
            t = Linear(t);
            RT.anchoredPosition = Vector3.Lerp(RT.anchoredPosition, EndRT, t);

            yield return null;
        }
        yield return new WaitForSeconds(3f);
        TimeElapsed = 0f;
        while (TimeElapsed < EndDuration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / EndDuration;
            t = Linear(t);
            RT.anchoredPosition = Vector3.Lerp(EndRT, StartRT, t);

            yield return null;
        }
        AchievementManager.instance.SaveAchi(key, 1);
        Debug.Log($"Achi State Saved! Name : {key}");
        Destroy(gameObject);
    }
    float Linear(float x)
    {
        return x;
    }
}
