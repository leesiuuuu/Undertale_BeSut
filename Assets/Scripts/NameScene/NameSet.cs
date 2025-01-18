using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameSet : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Yes;
    [SerializeField]
    private TMP_Text No;

    [SerializeField]
    private FadeAsync FA;
    [SerializeField]
    private SpriteRenderer sr;

    private bool isLeft;
    public bool StartSelete = false;
    private void Awake()
    {
        StartSelete = false;
        isLeft = false;
    }

    void Update()
    {
        Key();
        if (Input.GetKeyDown(KeyCode.Z) && StartSelete)
        {
            Selete();
        }
    }
    private void StateCheck()
    {
        if (isLeft)
        {
            Yes.color = Color.yellow;
            No.color = Color.white;
        }
        else
        {
            Yes.color = Color.white;
            No.color = Color.yellow;
        }
    }
    private void Key()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isLeft = true;
            StartSelete = true;
            StateCheck();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isLeft = false;
            StartSelete = true;
            StateCheck();
        }
    }
    private void Selete()
    {
        if(isLeft)
        {
            SceneManager.LoadScene("NameSettingScene");
        }
        else
        {
            StartCoroutine(FA.fade(sr, 4f, new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f)));
        }
    }
}
