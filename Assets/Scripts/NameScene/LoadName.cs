using TMPro;
using UnityEngine;

public class LoadName : MonoBehaviour
{
    private TMP_Text tem;
    private void Awake()
    {
        tem = GetComponent<TMP_Text>();
        tem.text = "���� �̸� : " + PlayerPrefs.GetString("name");
    }
}
