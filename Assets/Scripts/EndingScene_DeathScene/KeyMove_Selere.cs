using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyMove_Selere : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Re;
    [SerializeField]
    private TMP_Text GM;
    [SerializeField]
    private SceneMove sm;

    bool Moveable = true;

    bool flag = false;
    bool flagSeleted = false;
    private void Awake()
    {
        if (StateManager.instance.Last) StartCoroutine(AchievementManager.instance.AchiUIAppearence(5));
        flag = false;
        flagSeleted = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !GM.TryGetComponent(out Transition_TMP tt) && Moveable)
        {
            if (!flag) { flag = true; flagSeleted = true; }
            flagUpdate();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !GM.TryGetComponent(out Transition_TMP Tt) && Moveable)
        {
            if (flag) { flag = false; flagSeleted = true; }
            flagUpdate();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && !GM.TryGetComponent(out Transition_TMP tT) && Moveable && flagSeleted)
        {
            CheckFlag();
        }
    }
    void flagUpdate()
    {
        if(flag)
        {
            GM.color = Color.yellow;
            Re.color = Color.white;
        }
        else
        {
            GM.color= Color.white;
            Re.color= Color.yellow;
        }
    }
    void CheckFlag()
    {
        if (flag) StartCoroutine(sm.MainSceneMove());
        else StartCoroutine(sm.RestartGame());
        Moveable = false;
    }
}
