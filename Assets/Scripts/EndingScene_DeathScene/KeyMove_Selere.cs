using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyMove_Selere : MonoBehaviour
{
    [SerializeField]
    private Text Re;
    [SerializeField]
    private Text GM;
    [SerializeField]
    private SceneMove sm;

    bool Moveable = true;

    bool flag = false;
    private void Start()
    {
        flag = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !GM.TryGetComponent(out Transition_Text tt) && Moveable)
        {
            if (!flag) flag = true;
            flagUpdate();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !GM.TryGetComponent(out Transition_Text Tt) && Moveable)
        {
            if(flag) flag = false;
            flagUpdate();
        }
        if (Input.GetKeyDown(KeyCode.Z))
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
