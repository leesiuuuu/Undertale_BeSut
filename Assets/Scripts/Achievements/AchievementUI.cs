using UnityEngine;
using TMPro;

public class AchievementUI : MonoBehaviour
{
    public AchievementSO ASO;

    public SpriteRenderer Icon;
    public TextMeshPro TMP1;
    public TextMeshPro TMP2;

    public Sprite Lock;

    public SpriteRenderer[] SR;
    public TextMeshPro[] TMPN;

    public bool exit;
    private void Start()
    {
        SR = GetComponentsInChildren<SpriteRenderer>();
        TMPN = GetComponentsInChildren<TextMeshPro>();
        if (!exit)
        {
            if (PlayerPrefs.GetInt(ASO.key) == 1)
            {
                Icon.sprite = ASO.Icon;
                TMP1.text = ASO.Title;
                TMP2.text = ASO.Description;
            }
            else
            {
                Icon.sprite = Lock;
                TMP1.text = "????";
                TMP2.text = "??????????????";
            }
        }
        else
        {

        }
        StateDeSelete();
    }
    public void StateSelete()
    {
        for (int i = 0; i < SR.Length; i++)
        {
            if (SR[i].gameObject.name != "Black" && SR[i] != null)
            {
                SR[i].color = new Color(1, 1, 1, 1f);
            }
        }
        for (int i = 0; i < TMPN.Length; i++)
        {
            TMPN[i].color = new Color(1, 1, 1, 1f);
        }
    }
    public void StateDeSelete()
    {
        for (int i = 0; i < SR.Length; i++)
        {
            if (SR[i].gameObject.name != "Black" && SR[i] != null)
            {
                SR[i].color = new Color(1, 1, 1, 0.5f);
            }
        }
        for (int i = 0; i < TMPN.Length; i++)
        {
            TMPN[i].color = new Color(1, 1, 1, 0.5f);
        }
    }
}

