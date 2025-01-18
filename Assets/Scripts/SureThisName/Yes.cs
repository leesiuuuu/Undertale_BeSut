using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Yes : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer White;
    [SerializeField]
    private FadeAsync FA;
    [SerializeField]
    private float Duration;

    private Color white = new Color(1f, 1f, 1f, 1f);
    private Color none = new Color(1f, 1f, 1f, 0f);
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
                StartCoroutine(FA.fade(White, Duration, none, white));
            }
            else
            {
                trueToggle = true;
                T.color = Color.yellow;
            }
        }

    }
}
