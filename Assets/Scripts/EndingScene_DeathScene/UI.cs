using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    private int buttonPress = 0;
    [SerializeField]
    private TextMeshPro TMP;
    [SerializeField]
    private TextMeshPro GoMainText;
    private GoMain GM;

    bool nomove = false;

    private void Start()
    {
        GM = GetComponent<GoMain>();
        TMP.text = "";
        TMP.text = StateManager.instance.EndingDialogue();
        if (TMP.text == null) Debug.LogWarning("Ending Dialogue is null!");
        if (PlayerManager.instance.HP == PlayerManager.instance.MAX_HP) StartCoroutine(AchievementManager.instance.AchiUIAppearence(6));
        else if (PlayerManager.instance.HP <= 10) StartCoroutine(AchievementManager.instance.AchiUIAppearence(1));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (buttonPress)
            {
                case 0:
                    GoMainText.color = Color.yellow;
                    Mathf.Clamp(++buttonPress, 0, 1);
                    break;
                case 1:
                    Debug.Log("Go Main");
                    StartCoroutine(GM.GoMainScene());
                    nomove = true;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.X) && !nomove)
        {
            if(buttonPress == 1)
            {
                Mathf.Clamp(--buttonPress, 0, 1);
                GoMainText.color = Color.white;
            }
        }
    }
}
