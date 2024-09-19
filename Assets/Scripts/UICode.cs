using JetBrains.Annotations;
using System;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICode : MonoBehaviour
{
    [Header("Button List")]
    public Image FightBtn;
    public Image ActBtn;
    public Image ItemBtn;
    public Image MercyBtn;
    [Header("Sprite List")]
    public Sprite SeleteFight;
    public Sprite SeleteAct;
    public Sprite SeleteItem;
    public Sprite SeleteMercy;
    [Header("Sprite Normal List")]
    public Sprite NormalFight;
    public Sprite NormalAct;
    public Sprite NormalItem;
    public Sprite NormalMercy;
    [Header("State")]
    public bool Fight = false;
    public bool Act = false;
    public bool Item = false;
    public bool Mercy = false;
    [Header("Sound")]
    public AudioClip MoveSound;
    public AudioClip SeleteSound;
    [Header("Text")]
    public TMP_Text Ttext;
    [Header("Sprite")]
    public GameObject Heart;

    private Vector3 HeartPos;
    //UI 하트 위치
    private Vector3 FightBtnPos = new Vector3(-5.53f, -4.23f, 0f);
    private Vector3 ActBtnPos = new Vector3(-2.28f, -4.23f, 0f);
    private Vector3 ItemBtnPos = new Vector3(0.86f, -4.23f, 0f);
    private Vector3 MercyBtnPos = new Vector3(4.07f, -4.23f, 0f);
    //아이템 및 상호작용 선택 하트 위치
    private Vector3[ , ] ListPos = new Vector3[2, 4];
    private int i = 0, j = 0;

    void Start()
    {
        //배열 초기화
        ListPos[0, 0] = new Vector3(-4.55f, -0.71f, 0f);
        ListPos[1, 0] = new Vector3(-0.18f, -0.71f, 0f);
        ListPos[0, 1] = new Vector3(-4.55f, -1.11f, 0f);
        ListPos[1, 1] = new Vector3(-0.18f, -1.11f, 0f);

        FightBtn.sprite = SeleteFight;
        Fight = true;
        Heart.SetActive(true);
        HeartPos = FightBtnPos;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Heart.transform.position = HeartPos;
        if (StateManager.instance.Acting)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SoundManager.instance.SFXPlay("Move", MoveSound);
                StateChangeLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SoundManager.instance.SFXPlay("Move", MoveSound);
                StateChangeRight();
            }
        }
        else if(StateManager.instance.Starting)
        {
            if (StateManager.instance._Fighting)
            {
                Ttext.text = "";
                Ttext.text = "      * 네이트 코릴";
                HeartPos = ListPos[0, 0];
            }
            else if (StateManager.instance._Acting)
            {
                Ttext.text = "";
                Ttext.text = "      * 야옹거리기      * 욕하기\n      * 항복하기        * 모르는 척 하기";
                InteractiveSelete();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z) && StateManager.instance.Acting && !StateManager.instance.Starting)
        {
            StateManager.instance.Acting = false;
            StateManager.instance.Starting = true;
            ActState(true);
            SoundManager.instance.SFXPlay("Selete", SeleteSound);
        }
        if (Input.GetKeyDown(KeyCode.X) && !StateManager.instance.Acting && StateManager.instance.Starting)
        {
            StateManager.instance.Acting = true;
            StateManager.instance.Starting = false;
            ActState(false);
            ActCancel();
            i = 0;
            j = 0;
            Ttext.text = "";
            Ttext.gameObject.GetComponent<TalkBox>().Talk(0);
            SoundManager.instance.SFXPlay("Move", MoveSound);
        }
    }
    void StateChangeRight()
    {
        if (Fight)
        {
            Fight = false;
            Act = true;
            HeartPos = ActBtnPos;
            ImageChanger(FightBtn, NormalFight);
            ImageChanger(ActBtn, SeleteAct);
        }
        else if (Act)
        {
            Act = false;
            Item = true;
            HeartPos = ItemBtnPos;
            ImageChanger(ActBtn, NormalAct);
            ImageChanger(ItemBtn, SeleteItem);
        }
        else if (Item)
        {
            Item = false;
            Mercy = true;
            HeartPos = MercyBtnPos;
            ImageChanger(ItemBtn, NormalItem);
            ImageChanger(MercyBtn, SeleteMercy);
        }
        else if (Mercy)
        {
            Mercy = false;
            Fight = true;
            HeartPos = FightBtnPos;
            ImageChanger(MercyBtn, NormalMercy);
            ImageChanger(FightBtn, SeleteFight);
        }
    }
    void StateChangeLeft()
    {
        if (Fight)
        {
            Fight = false;
            Mercy = true;
            HeartPos = MercyBtnPos;
            ImageChanger(FightBtn, NormalFight);
            ImageChanger(MercyBtn, SeleteMercy);
        }
        else if (Act)
        {
            Act = false;
            Fight = true;
            HeartPos = FightBtnPos;
            ImageChanger(ActBtn, NormalAct);
            ImageChanger(FightBtn, SeleteFight);
        }
        else if (Item)
        {
            Item = false;
            Act = true;
            HeartPos = ActBtnPos;
            ImageChanger(ItemBtn, NormalItem);
            ImageChanger(ActBtn, SeleteAct);
        }
        else if (Mercy)
        {
            Mercy = false;
            Item = true;
            HeartPos = ItemBtnPos;
            ImageChanger(MercyBtn, NormalMercy);
            ImageChanger(ItemBtn, SeleteItem);
        }
    }
    void ImageChanger(Image Btn, Sprite Seleted)
    {
        Btn.sprite = Seleted;
    }
    void ActState(bool Toggle)
    {
        if (Fight) StateManager.instance._Fighting = Toggle;
        else if (Act) StateManager.instance._Acting = Toggle;
        else if (Item) StateManager.instance._Iteming = Toggle;
        else if (Mercy) StateManager.instance._Mercying = Toggle;
    }
    void ActCancel()
    {
        if (Fight) HeartPos = FightBtnPos;
        else if (Act) HeartPos = ActBtnPos;
        else if (Item) HeartPos = ItemBtnPos;
        else if (Mercy) HeartPos = MercyBtnPos;
    }
    void InteractiveSelete()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (j == 0) j++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (j == 1) j--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(i == 0) i++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(i == 1) i--;
        }
        HeartPos = ListPos[i, j];
    }
}
