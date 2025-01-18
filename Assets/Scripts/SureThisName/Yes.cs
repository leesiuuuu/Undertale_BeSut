using TMPro;
using UnityEngine;

public class Yes : MonoBehaviour
{
    private TMP_Text T;
    bool trueToggle = false;
    private void Awake()
    {
        T = GetComponent<TMP_Text>();
        T.color = Color.white;
        trueToggle = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (trueToggle)
            {
                T.color = Color.white;
                //하얀 화면 페이드 인
            }
            else
            {
                trueToggle = true;
                T.color = Color.yellow;
            }
        }

    }
}
