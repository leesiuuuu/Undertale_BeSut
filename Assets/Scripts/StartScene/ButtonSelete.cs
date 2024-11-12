using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo
{
    private string BtnName;
    private bool isSelete;
    private int Number;
    private Button Btn_Object;

    public ButtonInfo (string BtnName, bool isSelete, int Number, Button btn_Object)
    {
        this.BtnName = BtnName;
        this.isSelete = isSelete;
        this.Number = Number;
        Btn_Object = btn_Object;
        Btn_Object.SetPressed(false);
    }
    public bool GetSeleteState()
    {
        return this.isSelete;
    }
    public void SetSeleteState(bool isSelete)
    {
        this.isSelete =isSelete;
    }
    public void BtnMove(ButtonInfo NextBtn)
    {
        isSelete = false;
        NextBtn.SetSeleteState(true);
    }
    public void SetNumber(int number)
    {
        this.Number = number;
    }
    public int GetNumber()
    {
        return Number;
    }
    public string GetName()
    {
        return BtnName;
    }
    public Button GetBtn()
    {
        return Btn_Object;
    }
}

public class ButtonSelete : MonoBehaviour
{
    const int MAX_BTN_COUNT = 4;

    int CurrentNumber = 0;
    int LeastNumber;

    public Button StartBtn;
    public Button TutoBtn;
    public Button SetBtn;
    public Button ExitBtn;

    List<ButtonInfo> ButtonInfos = new List<ButtonInfo>();
    List<Vector2> HeartObjPos = new List<Vector2>();

    List<Button> Sprites = new List<Button>();
    private void Start()
    {
        Sprites.Add(StartBtn);
        Sprites.Add(TutoBtn);
        Sprites.Add(SetBtn);
        Sprites.Add(ExitBtn);

        ButtonInfos.Add(new ButtonInfo("StartBtn", true, 0, Sprites[0]));
        ButtonInfos.Add(new ButtonInfo("TutoBtn", false, 1, Sprites[1]));
        ButtonInfos.Add(new ButtonInfo("SetBtn", false, 2, Sprites[2]));
        ButtonInfos.Add(new ButtonInfo("ExitBtn", false, 3, Sprites[3]));

        HeartObjPos.Add(new Vector2(-1.49f, 0.54f));
        HeartObjPos.Add(new Vector2(-1.91f, -0.94f));
        HeartObjPos.Add(new Vector2(-1.67f, -2.37f));
        HeartObjPos.Add(new Vector2(-1.63f, -3.91f));

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
            transform.position = MoveHeartObj();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FindBtnWithID(CurrentNumber).GetBtn().SetPressed(true);
        }
        BtnSeleteActivate();
        BtnPressedCheck();
    }
    ButtonInfo FindBtnWithID(int id)
    {
        foreach (ButtonInfo button in ButtonInfos)
        {
            if(button.GetNumber() == id)
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
        foreach(ButtonInfo button in ButtonInfos)
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
        foreach(ButtonInfo btn in ButtonInfos)
        {
            if(btn.GetSeleteState())
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
        foreach(ButtonInfo buttonInfo in ButtonInfos)
        {
            if (buttonInfo.GetBtn().GetPressed())
            {
                switch(buttonInfo.GetName()){
                    case "StartBtn":
                        BtnManager.instance.StartGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    case "TutoBtn":
                        BtnManager.instance.TutorialGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    case "SetBtn":
                        BtnManager.instance.SettingGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                    case "ExitBtn":
                        BtnManager.instance.ExitGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        break;
                }
            }
        }
    }
}
