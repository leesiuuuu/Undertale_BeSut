using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameCheck : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField]
    private TMP_Text nameField;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        if(PlayerPrefs.GetString("name") == "Leesiu" ||
            PlayerPrefs.GetString("name") == "LEESIU" ||
            PlayerPrefs.GetString("name") == "leesiu")
        {
            text.text = "�� �̸��� �������� �̸��Դϴ�.";
            StartCoroutine(AchievementManager.instance.AchiUIAppearence(3));
        }
        else
        {
            text.text = "���� �� �̸����� �Ͻðڽ��ϱ�?";
        }
        nameField.text = PlayerPrefs.GetString("name");
        StartCoroutine(ScaleChanger());
    }
    IEnumerator ScaleChanger()
    {
        float Duration = 2f;
        float ElapsedTime = 0f;

        while(ElapsedTime < Duration)
        {
            ElapsedTime += Time.deltaTime;
            float t = ElapsedTime / Duration;
            nameField.gameObject.GetComponent<RectTransform>().localScale = Vector3.one + Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }
        yield break;
    }
}
