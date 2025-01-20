using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialBtn : MonoBehaviour
{
    private int MAX_BTN_COUNT = 4;

    int CurrentNumber = 0;
    int LeastNumber;

    List<ButtonInfo> ButtonInfos = new List<ButtonInfo>();
    List<Vector2> HeartObjPos = new List<Vector2>();
    List<Description> Descriptions = new List<Description>();

    public Vector2[] HeartObjPosArray;
    public Button[] Btns;
    public Description[] DescriptionArray;

    [SerializeField]
    private TMP_Text DescriptionText;
    [SerializeField]
    private Image DescriptionImage;
    [SerializeField]
    private Sprite ExTutorialImage;

    public Sprite LaserShield;
    public Sprite SeleteLaserShield;

    List<Button> Sprites = new List<Button>();

    private void Start()
    {
        MAX_BTN_COUNT = HeartObjPosArray.Length;
        DescriptionText.text = "";

        for (int i = 0; i < Btns.Length; i++)
        {
            Sprites.Add(Btns[i]);
        }

        for (int i = 0; i < MAX_BTN_COUNT; i++)
        {
            ButtonInfos.Add(new ButtonInfo($"Btn{i}", (i == 0) ? true : false, i, Sprites[i]));
        }

        for (int i = 0; i < MAX_BTN_COUNT; i++)
        {
            HeartObjPos.Add(HeartObjPosArray[i]);
        }
        
        for(int i = 0; i < Btns.Length; i++)
        {
            Descriptions.Add(DescriptionArray[i]);
        }

        if (PlayerPrefs.GetInt("bf") == 1)
        {
            Debug.Log("특수 튜토리얼 해방!");
            Sprites[4].Btn = LaserShield;
            Sprites[4].SeleteBtn = SeleteLaserShield;
            Descriptions[4].SetDescription("레이져 종류 공격을 방어할 수 있는 방어막을 생성합니다. V키를 통해 생성 가능하고 WASD를 통해 방향을 설정할 수 있습니다.");
            Descriptions[4].SetDescritpionImage(ExTutorialImage);
        }

        transform.position = HeartObjPos[0];
        DescriptionText.text = Descriptions[0].GetDescription();
        DescriptionImage.sprite = Descriptions[0].GetDescriptionImage();

    }
    void Update()
    {
        LeastNumber = CurrentNumber;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentNumber <= 0)
            {
                CurrentNumber = MAX_BTN_COUNT - 1;
            }
            else
            {
                CurrentNumber--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentNumber >= MAX_BTN_COUNT - 1)
            {
                CurrentNumber = 0;
            }
            else
            {
                CurrentNumber++;
            }
        }

        if (CurrentNumber != LeastNumber)
        {
            FindBtnWithID(LeastNumber).SetSeleteState(false);
            FindBtnWithID(CurrentNumber).SetSeleteState(true);
            DescriptionText.text = "";
            DescriptionText.text = Descriptions[CurrentNumber].GetDescription();
            DescriptionImage.sprite = Descriptions[CurrentNumber].GetDescriptionImage();
            transform.position = MoveHeartObj();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(CurrentNumber == MAX_BTN_COUNT - 1)
            {
                SceneManager.LoadScene("StartScene");
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("StartScene");
        }
        BtnSeleteActivate();
        BtnPressedCheck();
    }
    ButtonInfo FindBtnWithID(int id)
    {
        foreach (ButtonInfo button in ButtonInfos)
        {
            if (button.GetNumber() == id)
            {
                Debug.Log($"버튼을 찾았습니다! id : {id}");
                return button;
            }
        }
        Debug.LogWarning($"{id}의 번호를 가진 버튼이 없습니다.");
        return null;
    }

    Vector2 MoveHeartObj()
    {
        foreach (ButtonInfo button in ButtonInfos)
        {
            if (button.GetSeleteState())
            {
                return HeartObjPos[button.GetNumber()];
            }
        }
        return Vector2.zero;
    }
    void BtnSeleteActivate()
    {
        foreach (ButtonInfo btn in ButtonInfos)
        {
            if (btn.GetSeleteState())
            {
                btn.GetBtn().SeleteOn();
            }
            else
            {
                btn.GetBtn().SeleteOff();
            }
        }
    }
    void BtnPressedCheck()
    {
        foreach (ButtonInfo buttonInfo in ButtonInfos)
        {
            if (buttonInfo.GetBtn().GetPressed())
            {
                switch (buttonInfo.GetNumber())
                {
                    case 0:
                        BtnManager.instance.MoveTutorial();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    case 1:
                        BtnManager.instance.SeleteTutorial();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    case 2:
                        BtnManager.instance.CancelTutorial();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    case 3:
                        BtnManager.instance.ShieldTutorial();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    case 4:
                        BtnManager.instance.QQQTutorial();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    case 5:
                        BtnManager.instance.ExitTutorial();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
