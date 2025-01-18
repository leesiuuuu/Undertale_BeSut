using TMPro;
using UnityEngine;

public class LoadName : MonoBehaviour
{
    private TMP_Text tem;
    private void Awake()
    {
        tem = GetComponent<TMP_Text>();
        tem.text = "현재 이름 : " + PlayerPrefs.GetString("name");
    }
}
