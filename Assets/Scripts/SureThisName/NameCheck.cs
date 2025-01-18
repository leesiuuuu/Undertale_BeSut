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
            text.text = "이 이름은 개발자의 이름입니다.";
            StartCoroutine(AchievementManager.instance.AchiUIAppearence(3));
        }
        else
        {
            text.text = "정말 이 이름으로 하시겠습니까?";
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
