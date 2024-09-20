using System.Net;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject TalkBalloon;
    public BossTalkCode TalkBalloonText;
    [Header("WScripts")]
    public TextBoxToFightBox T1;
    public TextBoxToFightBox T2;
    public HeartMove HM;
    [Header("Attack Sprite")]
    public GameObject AttackBar;
    public GameObject Slide;
    public GameObject DistanceChecker;

    private Vector3 HeartPos;
    //UI 하트 위치
    private Vector3 FightBtnPos = new Vector3(-5.53f, -4.23f, 0f);
    private Vector3 ActBtnPos = new Vector3(-2.28f, -4.23f, 0f);
    private Vector3 ItemBtnPos = new Vector3(0.86f, -4.23f, 0f);
    private Vector3 MercyBtnPos = new Vector3(4.07f, -4.23f, 0f);
    //전투 하트 위치
    private Vector3 HeartMidPos = new Vector3(0f, -1.35f, 0f);
    //아이템 및 상호작용 선택 하트 위치
    private Vector3[ , ] ListPos = new Vector3[2, 4];
    private int i = 0, j = 0;
    //현재 Act 상황 메시지 출력 중인지 확인
    private bool isActDialogue = false;
    //보스 대화 클릭 횟수
    private int zClick = 0;
    //슬리이더 끝 위치
    private Vector3 SlideEndPos = new Vector3(5.39f, -1.49f, 432.4628f);
    //슬라이더 시작 위치
    private Vector3 SlideStartPos = new Vector3(-5.39f, -1.49f, 432.4628f);
    //한번만 작동할 bool
    private bool Once = false;
    private bool isBossDialogue = false;
    void Start()
    {
        T1.enabled = false;
        T2.enabled = false;
        HM.enabled = false;
        AttackBar.SetActive(false);
        Slide.SetActive(false);
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
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    StateManager.instance._Fighting = false;
                    StartAttack();
                }
            }
            else if (StateManager.instance._Acting)
            {
                if (!isActDialogue)
                {
                    Ttext.text = "";
                    Ttext.text = "      * 야옹거리기      * 욕하기\n      * 항복하기        * 모르는 척 하기";
                    InteractiveSelete();
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    isActDialogue = true;
                    SoundManager.instance.SFXPlay("Selete", SeleteSound);
                    if (HeartPos == ListPos[0, 0])
                    {
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 네이트 코릴에게\n  야옹거렸다.");
                    }
                    else if (HeartPos == ListPos[1, 0])
                    {
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 네이트 코릴에게\n  쌍욕을 박았다.");
                    }
                    else if (HeartPos == ListPos[0, 1])
                    {
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 네이트 코릴에게\n  항복한다고 말했다.");
                    }
                    else if (HeartPos == ListPos[1, 1])
                    {
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 네이트 코릴에게\n  아무것도 기억이 안난다고 말했다.");
                    }
                    StateManager.instance._Acting = false;
                }
            }
        }
        //확인 코드
        if (Input.GetKeyDown(KeyCode.Z) && StateManager.instance.Acting && !StateManager.instance.Starting)
        {
            StateManager.instance.Acting = false;
            StateManager.instance.Starting = true;
            ActState(true);
            SoundManager.instance.SFXPlay("Selete", SeleteSound);
        }
        //취소 코드
        if (Input.GetKeyDown(KeyCode.X) && !StateManager.instance.Acting && StateManager.instance.Starting)
        {
            StateManager.instance.Acting = true;
            StateManager.instance.Starting = false;
            ActState(false);
            ActCancel();
            i = 0;
            j = 0;
            Ttext.text = "";
            Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 어딘가 싸한 기분을 느꼈다.");
            SoundManager.instance.SFXPlay("Move", MoveSound);
        }
        //보스 전투 턴 넘어가는 코드
        if(StateManager.instance.Starting && StateManager.instance.Acting)
        {
            StateManager.instance.Starting = false;
            StateManager.instance.Acting = false;
            Invoke("StartFightTurn", 0.5f);
        }
        //보스 전투 및 말하기 코드
        if (!StateManager.instance.Fighting) Heart.transform.position = HeartPos;
        else
        {
            if (!Once)
            {
                zClick = 0;
                Once = true;
                isBossDialogue = true;
            }
            if (Input.GetKeyDown(KeyCode.Z) && isBossDialogue)
            {
                if(HeartPos == ListPos[0, 0] && !StateManager.instance.Talking)
                {
                    ++zClick;
                    switch (zClick)
                    {
                        case 1:
                            Ttext.text = "";
                            TalkBalloon.SetActive(true);
                            TalkBalloonText.Talk(0.2f, "....지금 뭐하는 거지?");
                            break;
                        case 2:
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            TalkBalloonText.Talk(0.2f, "나랑 장난하자는 건가?");
                            break;
                        case 3:
                            TalkBalloon.SetActive(false);
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            isBossDialogue = false;
                            break;
                    }
                }
                else if(HeartPos == ListPos[1, 0] && !StateManager.instance.Talking)
                {
                    ++zClick;
                    switch (zClick)
                    {
                        case 1:
                            Ttext.text = "";
                            TalkBalloon.SetActive(true);
                            TalkBalloonText.Talk(0.2f, "하하... 그럴 줄 알았어.");
                            break;
                        case 2:
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            TalkBalloonText.Talk(0.2f, "그 정도로 싫다는 거지?");
                            break;
                        case 3:
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            TalkBalloonText.Talk(0.2f, "그럼 나도 죽일 기세로 공격하겠어.");
                            break;
                        case 4:
                            TalkBalloon.SetActive(false);
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            isBossDialogue = false;
                            break;
                    }
                }
                else if(HeartPos == ListPos[0, 1] && !StateManager.instance.Talking)
                {
                    ++zClick;
                    switch (zClick)
                    {
                        case 1:
                            Ttext.text = "";
                            TalkBalloon.SetActive(true);
                            TalkBalloonText.Talk(0.2f, "이제와서 항복하겠다고?");
                            break;
                        case 2:
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            TalkBalloonText.Talk(0.2f, "너무 늦었어.");
                            break;
                        case 3:
                            TalkBalloon.SetActive(false);
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            isBossDialogue = false;
                            break;
                    }
                }
                else if (HeartPos == ListPos[1, 1] && !StateManager.instance.Talking)
                {
                    ++zClick;
                    switch (StateManager.instance.NoLieStack)
                    {
                        case 0:
                            DialogueAdder("뭐..?", "아무것도 기억이 안나..?", "미친 척 해봤자 나한테는 소용없어.");
                            break;
                        case 1:
                            DialogueAdder("...거짓말 하지 마.", "계속 기억이 안난다고 시치미 때봤자 안통해.", "얌전히 사라져.");
                            break;
                    }
                }
            }
            if (!isBossDialogue)
            {
                T1.enabled = true;
                if (!T2.enabled)
                {
                    T2.enabled = true;
                    Heart.transform.position = HeartMidPos;
                }
                Heart.SetActive(true);
                HM.enabled = true;
                //보스 패턴 코드(아마 다른 코드에서 진행될 듯)
            }
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
            if (i == 0) i++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (i == 1) i--;
        }
        HeartPos = ListPos[i, j];
    }
    void StartFightTurn()
    {
        StateManager.instance.Fighting = true;
    }
    void DialogueAdder(string log1, string log2 = "", string log3 = "")
    {
        switch (zClick)
            {
                case 1:
                    Ttext.text = "";
                    TalkBalloon.SetActive(true);
                    TalkBalloonText.Talk(0.2f, log1);
                    break;
                case 2:
                    TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                    TalkBalloonText.Talk(0.2f, log2);
                    break;
                case 3:
                    TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                    TalkBalloonText.Talk(0.2f, log3);
                    break;
                case 4:
                    TalkBalloon.SetActive(false);
                    TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                    StateManager.instance.NoLieStack++;
                    isBossDialogue = false;
                    break;
            }
    }
    void StartAttack()
    {
        Heart.SetActive(false);
        AttackBar.SetActive(true);
        Slide.SetActive(true);
        Slide.transform.position = SlideStartPos;
        //공격 바 움직임 코드 넣어 시불세꺠이ㅑ
    }
}
