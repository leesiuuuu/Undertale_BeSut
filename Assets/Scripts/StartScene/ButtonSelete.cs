using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private int MAX_BTN_COUNT = 5;

    int CurrentNumber = 0;
    int LeastNumber;

    public AudioClip SeleteSound;

    List<ButtonInfo> ButtonInfos = new List<ButtonInfo>();
    List<Vector2> HeartObjPos = new List<Vector2>();

    public Vector2[] HeartObjPosArray;
    public Button[] Btns;

    List<Button> Sprites = new List<Button>();
    private void Awake()
    {
        MAX_BTN_COUNT = HeartObjPosArray.Length;

        for (int i = 0; i < Btns.Length; i++)
        {
            Sprites.Add(Btns[i]);
        }

        for (int i = 0; i < MAX_BTN_COUNT; i++)
        {
            ButtonInfos.Add(new ButtonInfo($"Btn{i}", false, i, Sprites[i]));
        }

        for (int i = 0; i < MAX_BTN_COUNT; i++)
        {
            HeartObjPos.Add(HeartObjPosArray[i]);
        }
        if (PlayerPrefs.HasKey("CurrentNum")) CurrentNumber = PlayerPrefs.GetInt("CurrentNum");
        else CurrentNumber = 0;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Update()
    {
        LeastNumber = CurrentNumber;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.instance.SFXPlay("Selete", SeleteSound);
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
            SoundManager.instance.SFXPlay("Selete", SeleteSound);
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
            SaveCurrentNumber();
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
        return HeartObjPos[0];
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
                switch(buttonInfo.GetNumber()){
                    case 0:
                        BtnManager.instance.StartGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        SceneManager.sceneLoaded -= OnSceneLoaded;
                        break;
                    case 1:
                        BtnManager.instance.TutorialGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        SceneManager.sceneLoaded -= OnSceneLoaded;
                        break;
                    case 2:
                        BtnManager.instance.SettingGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        SceneManager.sceneLoaded -= OnSceneLoaded;
                        break;
                    case 3:
                        BtnManager.instance.AchiGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        SceneManager.sceneLoaded -= OnSceneLoaded;
                        break;
                    case 4:
                        BtnManager.instance.ExitGame();
                        buttonInfo.GetBtn().SetPressed(false);
                        SceneManager.sceneLoaded -= OnSceneLoaded;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    void SaveCurrentNumber()
    {
        PlayerPrefs.SetInt("CurrentNum", CurrentNumber);
        PlayerPrefs.Save();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if(scene.name == "StartScene")
        {
            if (GameObject.Find("StartMenu") == null)
            {
                SoundManager.instance.StartMenuPlay();
            }
            FindBtnWithID(PlayerPrefs.GetInt("CurrentNum")).SetSeleteState(true);
            transform.position = HeartObjPosArray[PlayerPrefs.GetInt("CurrentNum")];
            BtnSeleteActivate();
        }
    }
}
