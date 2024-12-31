using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public AudioClip AttackSound;
    public AudioClip DamagedSound;
    [Header("Text")]
    public TMP_Text Ttext;
    public Text ExitText;
    [Header("Sprite")]
    public GameObject Heart;
    public GameObject TalkBalloon;
    public BossTalkCode TalkBalloonText;
    public ScaleMOve scalemove;
    [Header("Scripts")]
    public TextBoxToFightBox T1;
    public TextBoxToFightBox T2;
    public TextBoxToFightBox1 T_1;
    public TextBoxToFightBox1 T_2;
    [Space(20f)]
    public HeartMove HM;
    public ItemUse IU;
    [Space(20f)]
    public AttackPatternA10M AtkPtnA10M;
    [Header("Attack Sprite")]
    public GameObject AttackBar;
    public GameObject Slide;
    public GameObject DistanceChecker;
    [Header("TurnDialogue")]
    public string SpecialDialogue;
    [Header("ItemList")]
    public List<string> ItemList = new List<string>();
    public GameObject ItemAndActText;
    public int Page = 1;
    const int MAX_PAGE = 2;
    public bool PageAdd = false;
    [Header("Act")]
    public List<string> ActList = new List<string>();
    [Header("Effect")]
    public GameObject AttackEffect;
    public GameObject BlackFade;

    //출력 대사
    private string Dialogue;

    //공격 슬라이더 Blink 애니메이션
    private Animator SliderAniamtor;

    //Item창에서 이전 선택 위치
    private Vector3 Before;
    private int BeforeItemLocate;
    private int Bf_i;
    private int Bf_j;

    /// <summary>
    /// 두 번째 페이지 비어있는 요소 상태
    /// 1 : 1 ~ 2개의 요소 있음
    /// 2 : 3 ~ 4개의 요소 있음
    /// 3 : 5 ~ 6개의 요소 있음
    /// </summary>
    private int SecPage_Element = 0;

    private int Damage;

    private int MAX_ITEM_LOCATE;
    private int ItemLocate = 0;

    private Vector3 HeartPos;
    //UI 하트 위치
    private Vector3 FightBtnPos = new Vector3(-5.53f, -4.23f, 0f);
    private Vector3 ActBtnPos = new Vector3(-2.28f, -4.23f, 0f);
    private Vector3 ItemBtnPos = new Vector3(0.86f, -4.23f, 0f);
    private Vector3 MercyBtnPos = new Vector3(4.07f, -4.23f, 0f);
    //전투 하트 위치
    private Vector3 HeartMidPos = new Vector3(0f, -1.35f, 0f);
    //아이템 및 상호작용 선택 하트 위치
    private Vector3[ , ] ListPos = new Vector3[2, 3];
    //아이템 요소 있는지 없는지 확인

    private bool[ , ] ListELement = new bool[2, 3];

    private int i = 0, j = 0;
    //현재 Act 상황 메시지 출력 중인지 확인
    private bool isActDialogue = false;
    //현재 Item 상황 메시지 출력 중인지 확인
    private bool isItemDialogue = false;
    //현재 Mercy 상황 메시지 출력 중인지 확인
    private bool isMercyDialogue = false;
    //보스 대화 클릭 횟수
    private int zClick = 0;
    //슬리이더 끝 위치
    private Vector3 SlideEndPos = new Vector3(5.39f, -1.49f, 432.4628f);
    //슬라이더 시작 위치
    private Vector3 SlideStartPos = new Vector3(-5.39f, -1.49f, 432.4628f);
    //Attack 선택 시 공격 중 판별 bool
    private bool AttackSliding = false;
    //한번만 작동할 bool
    private bool Once = false;
    private bool isBossDialogue = false;
    private bool isCountStarted = false;
    private bool GameDoneLogAdd = false;
    //각 행동 횟수별 보스 대화 로그 변경
    private int isMercyCheck = 0;
    private int isAct1Check = 0;
    private int isAct2Check = 0;
    private int isAct3Check = 0;
    private int isAct4Check = 0;
    private int Faze2ActCheck = 0;
    /// <summary>
    /// Meow = 야옹거리기
    /// Surnen = 항복하기
    /// Swear = 쌍욕
    /// IDK = 모름
    /// IDK_out = 모름(불살에서 노멀로 넘어갈 때)
    /// Mercy = 자비
    /// None = 2페이즈
    /// </summary>
    private string LastState;
    //첫번쨰 턴에서 모르는 척 하기를 했을 때 켜짐
    public bool FirstTurnAct = false;
    //불살 2페이즈 루트를 탔을 때 켜짐
    private bool SpriteChangeEvent = false;
    //피할건지 판단
    private bool IsMiss;

    private bool isOnce123 = false;
    private void Awake()
    {
        StateManager.instance.GameDone = false;
        scalemove.enabled = true;
        //남아있는 요소 확인
        if(ItemList.Count > 0 ) ItemList.Clear();
        //리스트 요소 추가
        ItemList.Add("자연산 커피");
        ItemList.Add("마법 롤케이크");
        ItemList.Add("마법 소다");
        ItemList.Add("마법 소다");
        ItemList.Add("마법 소다");
        ItemList.Add("뮤즈 룰스");
        ItemList.Add("뮤즈 룰스");
        ItemList.Add("뮤즈 룰스");
        ItemList.Add("특제 케-이크");
        MAX_ITEM_LOCATE = ItemList.Count;

        //Act 리스트 요소 추가
        if(ActList.Count > 0) ActList.Clear();
        ActList.Add("야옹거리기");
        ActList.Add("욕하기");
        ActList.Add("항복하기");
        ActList.Add("모르는 척 하기");
        ActList.Add("");
        ActList.Add("");
        
        StateManager.instance.ManagerInit();

        ItemAndActText.SetActive(false);

        ItemLocate = 0;
        Page = 1;
        PageAdd = false;
        i = 0;
        j = 0;

        SoundManager.instance.BGPlay();

        SliderAniamtor = Slide.GetComponent<Animator>();
        T1.enabled = false;
        T2.enabled = false;
        T_1.enabled = false;
        T_2.enabled = false;
        HM.enabled = false;
        AttackBar.SetActive(false);
        Slide.SetActive(false);
        //배열 초기화
        ListPos[0, 0] = new Vector3(-4.81f, -0.748f, 0f);
        ListPos[1, 0] = new Vector3(0.08f, -0.748f, 0f);
        ListPos[0, 1] = new Vector3(-4.81f, -1.28f, 0f);
        ListPos[1, 1] = new Vector3(0.08f, -1.28f, 0f);
        ListPos[0, 2] = new Vector3(-4.81f, -1.875f, 0f);
        ListPos[1, 2] = new Vector3(0.08f, -1.875f, 0f);

        FightBtn.sprite = SeleteFight;
        Fight = true;
        Act = false;
        Item = false;
        Mercy = false;
        Heart.SetActive(true);
        HeartPos = FightBtnPos;
        Cursor.visible = false;
    }
    private void Start()
    {
        AchievementManager.instance.InitAchi();
    }

    // Update is called once per frame
    void Update()
    {
        //UI 키 이동 코드
        if (StateManager.instance.Acting)
        {
            if (!StateManager.instance.Last && !GameDoneLogAdd)
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
        }
        //마지막에는 공격만 할 수 있게 함
        else if (StateManager.instance.Last && StateManager.instance.logAppear)
        {
            ActBtn.color = new Color(1, 1, 1, 0.3f);
            ItemBtn.color = new Color(1, 1, 1, 0.3f);
            MercyBtn.color = new Color(1, 1, 1, 0.3f);
        }
        //UI 선택 시 Text 변경 코드
        else if(StateManager.instance.Starting && !AttackSliding && !GameDoneLogAdd)
        {
            if (StateManager.instance._Fighting)
            {
                Ttext.text = "";
                Ttext.text = "      * 네이트 코릴";
                HeartPos = ListPos[0, 0];
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    ImageChanger(FightBtn, NormalFight);
                    SoundManager.instance.SFXPlay("Selete", SeleteSound);
                    Ttext.text = "";
                    StartCoroutine(ActAttacking());
                }
                else if (Input.GetKeyDown(KeyCode.X)) BackKey();
            }
            else if (StateManager.instance._Acting)
            {
                if (!isActDialogue)
                {
                    Ttext.text = "";
                    ItemAndActText.SetActive(true);
                    for(int i = 0; i < 6; i++)
                    {
                        GameObject.Find("Element" + (i + 1)).GetComponent<TMP_Text>().text = "";
                        GameObject.Find("Element" + (i + 1)).GetComponent<TMP_Text>().text = string.IsNullOrEmpty(ActList[i]) ? "" : "* " + ActList[i];
                    }
                    InteractiveSelete();
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    isActDialogue = true;
                    ImageChanger(ActBtn, NormalAct);
                    SoundManager.instance.SFXPlay("Selete", SeleteSound);
                    ItemAndActText.SetActive(false);
                    if (HeartPos == ListPos[0, 0])
                    {
                        FirstTurnAct = false;
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 네이트 코릴에게\n  야옹거렸다.");
                    }
                    else if (HeartPos == ListPos[1, 0])
                    {
                        FirstTurnAct = false;
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 네이트 코릴에게\n  쌍욕을 박았다.");
                    }
                    else if (HeartPos == ListPos[0, 1])
                    {
                        FirstTurnAct = false;
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 네이트 코릴에게\n  항복한다고 말했다.");
                    }
                    else if (HeartPos == ListPos[1, 1])
                    {
                        Heart.SetActive(false);
                        if (StateManager.instance.TurnCount == 1) FirstTurnAct = true;
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 당신은 네이트 코릴에게\n  아무것도 기억이 안난다고 말했다.");
                    }
                    StateManager.instance._Acting = false;
                }
                else if (Input.GetKeyDown(KeyCode.X)) BackKey();
            }
            else if (StateManager.instance._Iteming)
            {
                if (!isItemDialogue)
                {
                    ItemPageUdate();
                    InteractiveSelete_Item();
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    isItemDialogue = true;
                    ImageChanger(ItemBtn, NormalItem);
                    SoundManager.instance.SFXPlay("Selete", SeleteSound);
                    MAX_ITEM_LOCATE--;
                    IU.ItemUsed(Ttext, ItemAndActText, ItemList, Heart, ItemLocate, MAX_ITEM_LOCATE);
                    StateManager.instance._Iteming = false;
                }
                else if (Input.GetKeyDown(KeyCode.X)) BackKey();
            }
            else if (StateManager.instance._Mercying)
            {
                if (!isMercyDialogue)
                {
                    if (StateManager.instance.NoKill)
                    {
                        Color Yellow = new Color(255, 255, 0);
                        Ttext.text = "";
                        Ttext.color = Yellow;
                        Ttext.text = "      * 살려주기";
                    }
                    else
                    {
                        Ttext.text = "";
                        Ttext.text = "      * 살려주기";
                    }
                    HeartPos = ListPos[0, 0];
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    ImageChanger(MercyBtn, NormalMercy);
                    SoundManager.instance.SFXPlay("Selete", SeleteSound);
                    Heart.SetActive(false);
                    FirstTurnAct = false;
                    Ttext.text = "";
                    isMercyDialogue = true;
                    StateManager.instance.Acting = false;
                    StateManager.instance.Starting = false;
                    StateManager.instance.Fighting = true;
                    StateManager.instance._Mercying = false;
                }
                else if (Input.GetKeyDown(KeyCode.X)) BackKey();
            }
        }
        if (!StateManager.instance.logAppear)
        {
            //확인 코드
            if (Input.GetKeyDown(KeyCode.Z) && StateManager.instance.Acting && !StateManager.instance.Starting)
            {
                StateManager.instance.Acting = false;
                StateManager.instance.Starting = true;
                ActState(true);
                SoundManager.instance.SFXPlay("Selete", SeleteSound);
            }
            //취소 코드
        }
        //보스 전투 턴 넘어가는 코드
        if(StateManager.instance.Starting && StateManager.instance.Acting && !StateManager.instance.GameDone)
        {
            if(BossManager.instance.bossHP > 1)
            {
                StateManager.instance.Starting = false;
                StateManager.instance.Acting = false;
                Invoke("StartFightTurn", 0.5f);
            }
        }
        //하트 오브젝트 위치 지속적으로 확인
        if (!StateManager.instance.Fighting && !GameDoneLogAdd)
        {
            Heart.transform.position = HeartPos;
        }
        //보스 전투 및 말하기 코드
        else
        {
            if (isItemDialogue)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Ttext.text = "";
                    T_1.enabled = false;
                    T_2.enabled = false;
                    T1.enabled = true;
                    if (!T2.enabled)
                    {
                        T2.enabled = true;
                        Heart.transform.position = HeartMidPos;
                    }
                    Heart.SetActive(true);
                    StartCoroutine("EnablePlayer", 0.3f);   
                    if (StateManager.instance.NoKill)
                    {
                        Heart.transform.position = HeartMidPos;
                        Invoke("MyTurnBack", 0.75f);
                        StateManager.instance.Fighting = false;
                    }
                    else
                    {
                        if (!StateManager.instance.Faze2) StartCoroutine(PatternManager.instance.SeqPatternStart(StateManager.instance.TurnCount));
                        else
                        {
                            PatternManager.instance.Before = PatternManager.instance.PatternCountFaze2;
                            StartCoroutine(PatternManager.instance.SeqPatternStart2(PatternManager.instance.PatternCountFaze2));
                        }
                    }
                }
            }
            else if (isActDialogue)
            {
                if (!Once)
                {
                    zClick = 0;
                    Once = true;
                    isBossDialogue = true;
                }
                if (Input.GetKeyDown(KeyCode.Z) && isBossDialogue)
                {
                    if (!StateManager.instance.Faze2)
                    {
                        if (HeartPos == ListPos[0, 0] && !StateManager.instance.Talking)
                        {
                            ++zClick;
                            if (StateManager.instance.NoKill)
                            {
                                DialogueAdder("지금 행동에는 다 이유가 있겠지.", "빨리 끝내자.");
                            }
                            else
                            {
                                switch (isAct1Check)
                                {
                                    case 0:
                                        DialogueAdder("지금 뭐하는 거지?", "나랑 장난하자는 건가?");
                                        break;
                                    case 1:
                                        DialogueAdder(".....미친거냐?", "징그러워 죽겠군.");
                                        break;
                                    default:
                                        DialogueAdder("......");
                                        break;
                                }
                            }
                            LastState = "Meow";
                        }
                        else if (HeartPos == ListPos[1, 0] && !StateManager.instance.Talking)
                        {
                            ++zClick;
                            if (StateManager.instance.NoKill)
                            {
                                DialogueAdder("이런 행동을 하는 데 다 이유가 있겠지.", "빨리 들어보고 싶군.");
                            }
                            else
                            {
                                switch (isAct2Check)
                                {
                                    case 0:
                                        DialogueAdder(".......", "그게 다냐?");
                                        break;
                                    case 1:
                                        DialogueAdder("한심하군...");
                                        break;
                                    default:
                                        DialogueAdder("......");
                                        break;
                                }
                            }
                            LastState = "Swear";
                        }
                        else if (HeartPos == ListPos[0, 1] && !StateManager.instance.Talking)
                        {
                            ++zClick;
                            if (StateManager.instance.NoKill)
                            {
                                DialogueAdder("이런 행동을 하는 데 다 이유가 있겠지.", "누가 이런걸 시키는 거지?", "빨리 들어보고 싶군.");
                            }
                            else
                            {
                                if (FirstTurnAct) FirstTurnAct = false;
                                switch (isAct3Check)
                                {
                                    case 0:
                                        DialogueAdder("이제와서 항복하겠다고?", "너무 늦었어.");
                                        break;
                                    case 1:
                                        DialogueAdder("왜 항복하는 거지?", "내가 무서운가?");
                                        break;
                                    case 2:
                                        DialogueAdder("그렇게 여기서 벗어나고 싶나?", "좋아.", "최대한 빠르게 보내주지."); ;
                                        break;
                                    case 3:
                                        DialogueAdder("지치지도 않는군.");
                                        break;
                                    default:
                                        DialogueAdder(".......");
                                        break;
                                }
                            }
                            LastState = "Surnen";
                        }
                        else if (HeartPos == ListPos[1, 1] && !StateManager.instance.Talking)
                        {
                            ++zClick;
                            if (StateManager.instance.NoKill)
                            {
                                DialogueAdder("그래, 진짜 아무 기억도 안나는 것 같구나.", "그러니 빨리 끝내자.");
                                LastState = "IDK";
                            }
                            else
                            {
                                if (FirstTurnAct)
                                {
                                    switch (StateManager.instance.NoLieStack)
                                    {
                                        case 0:
                                            DialogueAdder("뭐..?", "아무것도 기억이 안나..?", "미친 척 해봤자 나한테는 소용없어.");
                                            break;
                                        case 1:
                                            DialogueAdder("...거짓말 하지 마.", "계속 기억이 안난다고 시치미 때봤자 안통해.", "얌전히 사라져.");
                                            break;
                                        case 2:
                                            DialogueAdder("진심이냐?", "만약 정말로 기억이 안난다면...", "......아니, 아니야.", "그럴리가 없지.");
                                            break;
                                        case 3:
                                            DialogueAdder("아무것도 기억이 안나..?",
                                                "정말로?",
                                                "반항하지 않는 걸 봐서는....",
                                                "어쩌면 사실일 수도 있겠군.");
                                            break;
                                        case 4:
                                            DialogueAdder("그럼 지금 너한테 묻은 자국들..",
                                                "설명할 수 있나?",
                                                "아니면, 지금 무슨 상황인지도 모르면서 싸우고 었었던 건가?",
                                                "흠....");
                                            break;
                                        case 5:
                                            DialogueAdder("하지만 넌 수많은 동료들을 죽였어.",
                                                "그건 절대 용서 될 수 없다.",
                                                "아무리 기억이 안 난다고 해도..",
                                                "넌 결국에는 죽게 될거다.");
                                            break;
                                        case 6:
                                            DialogueAdder("정말 끈질기군.",
                                                "이 점은 예전의 너랑 똑같네.",
                                                ".....");
                                            break;
                                        case 7:
                                            DialogueAdder("아직은 너를 완벽하게 못 믿겠어.",
                                                "계속해서 나를 믿게 해봐.",
                                                "만약 정말 기억이 안난다면,",
                                                "넌 나를 공격할 이유가 없다.");
                                            break;
                                        case 8:
                                            DialogueAdder("혹시 지금...",
                                                "누군가에게 조종당하고 있나?",
                                                "아, 아니다.",
                                                "너한테 한 말이 아니야.");
                                            break;
                                        case 9:
                                            DialogueAdder("좋다.",
                                                "이정도면 어느정도 신뢰할 만 하군.",
                                                "너에게 자비를 배풀어주마.");
                                            break;
                                    }
                                    LastState = "IDK";
                                }
                                else
                                {
                                    switch (isAct4Check)
                                    {
                                        case 0:
                                            DialogueAdder("난 너의 실체를 알고 있다.", "그런 거짓말은 안통해.");
                                            break;
                                        case 1:
                                            DialogueAdder("소용없다.", "더 이상 의미 없어.");
                                            break;
                                        default:
                                            DialogueAdder(".....");
                                            break;
                                    }
                                    LastState = "IDK_out";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!StateManager.instance.Talking)
                        {
                            ++zClick;
                            switch (Faze2ActCheck)
                            {
                                case 0:
                                    DialogueAdder("더이상의 반응은 없다.", "어떠한 대답도 하지 않을 거다.");
                                    break;
                                default:
                                    DialogueAdder("......");
                                    break;
                            }
                            LastState = "None";
                        }
                    }

                }
                if (!isBossDialogue)
                {
                    switch (LastState)
                    {
                        case "Meow":
                            ++isAct1Check;
                            LastState = "";
                            break;
                        case "Swear":
                            ++isAct2Check;
                            LastState = "";
                            break;
                        case "Surnen":
                            ++isAct3Check;
                            LastState = "";
                            break;
                        case "IDK":
                            ++StateManager.instance.NoLieStack;
                            LastState = "";
                            break;
                        case "IDK_out":
                            ++isAct4Check;
                            LastState = "";
                            break;
                        case "None":
                            ++Faze2ActCheck;
                            LastState = "";
                            break;
                    }
                    if (isAct1Check > 1 && !StateManager.instance.Faze2 && !isOnce123)
                    {
                        StartCoroutine(AchievementManager.instance.AchiUIAppearence(2));
                        isOnce123 = true;
                    }
                    T_1.enabled = false;
                    T_2.enabled = false;
                    T1.enabled = true;
                    if (!T2.enabled)
                    {
                        T2.enabled = true;
                        Heart.transform.position = HeartMidPos;
                    }
                    Heart.SetActive(true);
                    StartCoroutine("EnablePlayer", 0.3f);
                    if (StateManager.instance.NoKill)
                    {
                        Heart.transform.position = HeartMidPos;
                        Invoke("MyTurnBack", 0.75f);
                        StateManager.instance.Fighting = false;
                    }
                    if (!isCountStarted && !StateManager.instance.NoKill)
                    {
                        if(StateManager.instance.NoLieStack == 10)
                        {
                            StateManager.instance.NoKill = true;
                            Invoke("MyTurnBack", 0.75f);
                            StateManager.instance.Fighting = false;
                        }
                        else
                        {
                            if (StateManager.instance.Faze2)
                            {
                                PatternManager.instance.Before = PatternManager.instance.PatternCountFaze2;
                                StartCoroutine(PatternManager.instance.SeqPatternStart2(PatternManager.instance.PatternCountFaze2));
                            }
                            StartCoroutine(PatternManager.instance.SeqPatternStart(StateManager.instance.TurnCount));
                            isCountStarted = true;
                        }
                    }
                }
            }
            else if (isMercyDialogue)
            {
                if (!Once)
                {
                    zClick = 0;
                    Once = true;
                    isBossDialogue = true;
                }
                if (Input.GetKeyDown(KeyCode.Z) && isBossDialogue && !StateManager.instance.Talking)
                {
                    ++zClick;
                    if (!StateManager.instance.NoKill && !StateManager.instance.Faze2)
                    {
                        switch (isMercyCheck)
                        {
                            case 0:
                                DialogueAdder("이제와서 살려주겠다고?", "자비는 없다.");
                                break;
                            case 1:
                                DialogueAdder("전부 죽여놓고, 이제와서 살려준다고?", "한심하기 짝이 없군.");
                                break;
                            case 2:
                                DialogueAdder("반응도 하기 싫군.", "죽어라.");
                                break;
                            default:
                                DialogueAdder("......");
                                break;
                        }
                    }
                    else if (StateManager.instance.Faze2)
                    {
                        switch (isMercyCheck)
                        {
                            case 0:
                                DialogueAdder("아직도 목숨을 구걸하다니.", "그렇게 살고 싶나?");
                                break;
                            default:
                                DialogueAdder("더 이상 대답할 이유는 없다.");
                                break;
                        }
                    }
                    else
                    {
                        DialogueAdder("자세한 이야기는 좀 쉬면서 들어보도록 하지.");
                    }
                    LastState = "Mercy";
                }
                if (!isBossDialogue)
                {
                    if(LastState == "Mercy") 
                    {
                        ++isMercyCheck;
                        LastState = "";
                    }
                    StateManager.instance.Talking = false;
                    //불살 루트 종료 코드(게임 종료)
                    if (StateManager.instance.NoKill && !GameDoneLogAdd)
                    {
                        StateManager.instance.GameDone = true;
                        Debug.Log("GameDone");
                        isCountStarted = true;
                    }
                    //GameDone 이후 반복될 시 나타나는 조건을 나누기
                    else if (StateManager.instance.NoKill && GameDoneLogAdd)
                    {
                        Debug.Log("GameEnding...");
                    }
                    //기타
                    else
                    {
                        T_1.enabled = false;
                        T_2.enabled = false;
                        T1.enabled = true;
                        if (!T2.enabled)
                        {
                            T2.enabled = true;
                            Heart.transform.position = HeartMidPos;
                        }
                        Heart.SetActive(true);
                        StartCoroutine("EnablePlayer", 0.3f);
                        if (!isCountStarted)
                        {
                            if(!StateManager.instance.Faze2) StartCoroutine(PatternManager.instance.SeqPatternStart(StateManager.instance.TurnCount));
                            else
                            {
                                PatternManager.instance.Before = PatternManager.instance.PatternCountFaze2;
                                StartCoroutine(PatternManager.instance.SeqPatternStart2(PatternManager.instance.PatternCountFaze2));
                            }
                            isCountStarted = true;
                        }
                    }
                }
            }
        }
        //전투가 종료되었는지 확인
        if (StateManager.instance.GameDone && !GameDoneLogAdd)
        {
            Ttext.text = "";
            Ttext.color = new Color(255, 255, 255);
            if(StateManager.instance.Last) Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 전투 종료!\n* 당신은 128xp와 999골드를 얻었다!");
            else Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* 전투 종료!\n* 당신은 0xp와 0골드를 얻었다!");
            StateManager.instance.GameDone = false;
            GameDoneLogAdd = true;
            if(!StateManager.instance.NormalFaze2 && !StateManager.instance.BetrayalFaze2) StateManager.instance.MercyEnd = true;
            StartCoroutine(EndScene());
        }
        if (PlayerManager.instance.HP <= 0) PlayerManager.instance.Death();
        //게임 종료 코드
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitGame());
        }
    }
    //추후에 더 다룰 예정
    IEnumerator ExitGame()
    {
        float DownTime = 0f;
        float TimeElapsed = 0f;
        float Duration = 1f;
        StartCoroutine(ExitText.GetComponent<Quitting>().Quit());
        while (!Input.GetKeyUp(KeyCode.Escape))
        {
            DownTime += Time.deltaTime;
            if (TimeElapsed <= Duration) TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration;
            ExitText.color = new Color(1, 1, 1, t);
            if (DownTime > 1f)
            {
                DownTime = 0f;
                Debug.Log("Game Exit!");
                ExitText.color = new Color(1, 1, 1, 0);
                StopCoroutine(ExitText.GetComponent<Quitting>().Quit());
                Application.Quit();
                yield break;
            }
            yield return null;
        }
        StopCoroutine(ExitText.GetComponent<Quitting>().Quit());
        ExitText.color = new Color(1, 1, 1, 0);
        yield break;
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
    void InteractiveSelete_Item()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (j >= 0 && j < 2)
            {
                j++;
                ItemLocate += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (j > 0 && j < 3)
            {
                j--;
                ItemLocate -= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //페이지 넘어갈 시 요소 변경 가능하게 해줌
            if (PageAdd)
            {
                if (Page == 1 && i == 1)
                {
                    Page = 2;
                    //현재 비어있는 요소 갯수 저장
                    switch (ItemList.FindAll(x => x == "").Count)
                    {
                        case 5: case 4:
                            SecPage_Element = 1;
                            break;
                        case 3: case 2:
                            SecPage_Element = 2;
                            break;
                        case 1: case 0:
                            SecPage_Element = 3;
                            break;
                    }
                    i = 0;
                    ItemLocate += 5;
                    //비어있는 요소에 따른 UI 반응
                    switch (SecPage_Element)
                    {
                        case 1:
                            if (j == 2 || j == 1)
                            {
                                j = 0;
                                ItemLocate = 6;
                            }
                            break;
                        case 2:
                            if (j == 2)
                            {
                                j = 1;
                                ItemLocate = 8;
                            }
                            break;
                        case 3:
                            break;
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        i++;
                        ItemLocate += 1;
                    }
                }
            }
            else
            {
                if (i == 0)
                {
                    i++;
                    ItemLocate += 1;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //페이지 넘어갈 시 요소 변경 가능하게 해줌
            if (PageAdd)
            {
                if (Page == 2 && i == 0)
                {
                    Debug.Log("Item Moved!");
                    Page = 1;
                    i = 1;
                    ItemLocate -= 5;
                    ItemPageUdate();
                }
                else
                {
                    if (i == 1)
                    {
                        i--;
                        ItemLocate -= 1;
                    }
                }
            }
            else
            {
                if (i == 1)
                {
                    i--;
                    ItemLocate -= 1;
                }
            }
        }
        //요소가 존재하지 않을 때 발생
        if (ListELement[i, j])
        {
            Debug.Log("Befored");
            HeartPos = Before;
            ItemLocate = BeforeItemLocate;
            i = Bf_i; j = Bf_j;
        }
        //요소가 존재할 시 실행
        else
        {
            HeartPos = ListPos[i, j];
            Before = ListPos[i, j];
            BeforeItemLocate = ItemLocate;
            Bf_i = i;
            Bf_j = j;
        }
    }
    void StartFightTurn()
    {
        StateManager.instance.Fighting = true;
    }
    //대사를 받고 출력해줌
    void DialogueAdder(params string[] logs)
    {
        if(zClick == 1)
        {
            Ttext.text = "";
            TalkBalloon.SetActive(true);
            TalkBalloonText.Talk(0.2f, logs[zClick - 1]);
        }
        else if(zClick > 1 &&  zClick < logs.Length + 1)
        {
            TalkBalloon.SetActive(true);
            TalkBalloonText.Talk(0.2f, logs[zClick - 1]);
        }
        else
        {
            TalkBalloon.SetActive(false);
            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
            isBossDialogue = false;
        }
    }
    private IEnumerator ActAttacking()
    {
        Damage = 0;
        IsMiss = (Random.value < 0.7f);
        bool Stop = false;
        Heart.SetActive(false);
        AttackBar.SetActive(true);
        Slide.SetActive(true);
        AttackSliding = true;
        Slide.transform.position = SlideStartPos;
        while (Slide.transform.position.x < SlideEndPos.x && !Stop)
        {
            Slide.transform.Translate(Vector3.right * 7 * Time.deltaTime);
            yield return null;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SliderAniamtor.SetBool("Blink", true);
                Stop = true;
                if (StateManager.instance.NoKill && !SpriteChangeEvent)
                {
                    Damage = BossManager.instance.bossHP - 1;
                    SpriteChangeEvent = true;
                }
                else if((StateManager.instance.BetrayalFaze2 || StateManager.instance.NormalFaze2) && !StateManager.instance.Last)
                {
                    Damage = (int)(DistanceCheck.DistancetoDamage(DistanceChecker.transform.position.x, Slide.transform.position.x) * 1.3f);
                }
                else if (StateManager.instance.Last)
                {
                    Damage = BossManager.instance.bossHP;
                }
                else
                {
                    Damage = DistanceCheck.DistancetoDamage(DistanceChecker.transform.position.x, Slide.transform.position.x);
                }
                Debug.Log(Damage);
            }
        }
        if (Stop)
        {
            //공격 후 데미지 UI 생성 및 전투 창 넘어가기
            if (StateManager.instance.Faze2 && IsMiss && !StateManager.instance.Last)
            {
                StartCoroutine(BossManager.instance.BossMoveToLeftOrRight(BossManager.instance.MissAnimDuration));
            }
            GameObject clone = Instantiate(AttackEffect);
            SoundManager.instance.SFXPlay("Attack", AttackSound);
            StartCoroutine(DeleteEffect(clone));
            yield return new WaitForSeconds(0.9f);
            if(StateManager.instance.NoLieStack == 8) //공격할 이유가 없다고 할 떄 공격하면 나타나는 반응
            {
                yield return new WaitForSeconds(0.7f);
                AttackBar.SetActive(false);
                Slide.SetActive(false);
                FirstTurnAct = false;
                isBossDialogue = true;
                StartCoroutine(AchievementManager.instance.AchiUIAppearence(10));
                zClick = 1;
                DialogueAdder("역시...");
                while (zClick <= 3){
                    if (Input.GetKeyDown(KeyCode.Z) && !StateManager.instance.Talking)
                    {
                        DialogueAdder("거짓말이었군.", "사라져라.");
                        ++zClick;
                    }
                    yield return null;
                }
                StateManager.instance.NoLieStack = 0;
                TalkBalloon.SetActive(false);
                LastState = "IDK_out";
            }
            //보스 체력 절반 이상 소모할 시 2페이즈 넘어감
            if (BossManager.instance.bossHP < BossManager.instance.MAX_BOSS_HP / 2 && BossManager.instance.bossHP != 1 && !StateManager.instance.Faze2)
            {
                Debug.Log("Faze 2(Normal)");
                StateManager.instance.NormalFaze2 = true;
                yield return new WaitForSeconds(1.2f);
                AttackBar.SetActive(false);
                Slide.SetActive(false);
                FirstTurnAct = false;
                isBossDialogue = true;
                zClick = 1;
                DialogueAdder("아주 제대로 날 죽이려고 드는군.");
                while (zClick < 7)
                {
                    if (zClick == 6)
                    {
                        BossManager.instance.ChangeSprite(4);
                    }
                    if (Input.GetKeyDown(KeyCode.Z) && !StateManager.instance.Talking)
                    {
                        DialogueAdder("사실 널 조금은 믿고 있었어.",
                            "너가 공격을 멈추고 항복하기를 바랬는데.",
                            "이렇게 보기 좋게 내 믿음을 져버리다니.",
                            "뭐, 아쉽게 된거지.",
                            "널 확실하게 죽여줄게.");
                        zClick++;
                    }
                    yield return null;
                }
                StartCoroutine(PatternManager.instance.Faze2PatternChange(3, 1.5f));
                yield return new WaitForSeconds(5f);
                BossManager.instance.BossHPHeal(BossManager.instance.MAX_BOSS_HP - BossManager.instance.bossHP);
                yield return new WaitForSeconds(1f);
                isBossDialogue = true;
                zClick = 1;
                DialogueAdder("어디 한번 해보자고.");
                while (zClick <= 2)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        zClick = 4;
                        DialogueAdder("");
                    }
                    yield return null;
                }
                Debug.Log("Faze 2 Start!");
                //2페이즈 시작 알림
                AttackSliding = false;
                StateManager.instance.Acting = false;
                StateManager.instance.NoKill = false;
                StateManager.instance.Faze2 = true;
                StateManager.instance.Starting = false;
                SoundManager.instance.StopBG();
                SoundManager.instance.BG2Play();
                Faze2Init();
                Stop = false;
                FirstTurnAct = false;
                yield return null;

            }
            else if (BossManager.instance.bossHP > 1) Invoke("FightAndAttack", 0.7f);
            else if (BossManager.instance.bossHP <= 0) Invoke("GameEnd", 0.7f);
            //2페이즈 조건문
            else
            {
                if (StateManager.instance.Faze2)
                {
                    yield return null;
                }
                StartCoroutine(AchievementManager.instance.AchiUIAppearence(4));
                Debug.Log("For 2 Faze...");
                StateManager.instance.BetrayalFaze2 = true;
                SoundManager.instance.StopBG();
                yield return new WaitForSeconds(1.2f);
                AttackBar.SetActive(false);
                Slide.SetActive(false);
                FirstTurnAct = false;
                isBossDialogue = true;
                zClick = 1;
                DialogueAdder(".....");
                while (zClick < 13)
                {
                    if (zClick == 4) BossManager.instance.ChangeSprite(1);
                    if (zClick == 10)
                    {
                        BossManager.instance.Boss.transform.localScale = new Vector3(2, 2, 2);
                        BossManager.instance.ChangeSprite(3);
                        BossManager.instance.StartMove();
                    }
                    if (Input.GetKeyDown(KeyCode.Z) && !StateManager.instance.Talking)
                    {
                        DialogueAdder("........",
                            "이렇게 당할 줄은 생각도 못했는데.",
                            ".............",
                            "나도 참 멍청하군.",
                            "저딴 너셕을 믿는 게 아니었어.",
                            "진작에 눈치챘어야 했는데.",
                            "...",
                            "....아니",
                            "여기서 죽을 수 없어.",
                            "막아야만 해.",
                            "무조건...");
                        ++zClick;
                    }
                    yield return null;
                }
                StartCoroutine(PatternManager.instance.Faze2PatternChange(3, 1.5f));
                yield return new WaitForSeconds(5f);
                BossManager.instance.BossHPHeal(BossManager.instance.MAX_BOSS_HP - BossManager.instance.bossHP);
                yield return new WaitForSeconds(1f);
                isBossDialogue = true;
                zClick = 1;
                DialogueAdder("이제 봐주지 않는다.");
                while (zClick <= 2)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        zClick = 4;
                        DialogueAdder("");
                    }
                    yield return null;
                }
                zClick = 1;
                Debug.Log("Faze 2 Start!");
                //2페이즈 시작 알림
                StateManager.instance.Acting = false;
                StateManager.instance.NoKill = false;
                StateManager.instance.Faze2 = true;
                StateManager.instance.Starting = false;
                AttackSliding = false;
                SoundManager.instance.BG2Play();
                Faze2Init();
                Stop = false;
                FirstTurnAct = false;
                yield return null;
            }
            yield return null;
            FirstTurnAct = false;
            Stop = false;
        }
        else
        {
            Slide.transform.position = SlideEndPos;
            Heart.SetActive(true);
            HeartPos = FightBtnPos;
            AttackBar.SetActive(false);
            Slide.SetActive(false);
            ImageChanger(FightBtn, SeleteFight);
            AttackSliding = false;
            StateManager.instance._Fighting = false;
            StateManager.instance.Starting = false;
            StopAttack();
        }
    }
    void StopAttack()
    {
        Ttext.text = "";
        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, StateManager.instance.DialogueChanger(StateManager.instance.TurnCount, Dialogue));
    }
    //보스 턴 끝날 시 초기화
    public void MyTurnBack()
    {
        HM.enabled = false;
        ItemLocate = 0;
        Page = 1;
        PageAdd = false;
        i = 0;
        j = 0;
        isActDialogue = false;
        isItemDialogue = false;
        isMercyDialogue = false;
        Once = false;
        isCountStarted = false;
        ++StateManager.instance.TurnCount;
        T1.enabled = false;
        T2.enabled = false;
        T_1.enabled = true;
        T_2.enabled = true;
        Ttext.gameObject.SetActive(true);
        Ttext.gameObject.GetComponent<TalkBox>().Talk(0.5f, StateManager.instance.DialogueChanger(StateManager.instance.TurnCount, Dialogue));
        Fight = true;
        Act = false;
        Item= false;
        Mercy = false;
        ImageChanger(FightBtn, SeleteFight);
        HeartPos = FightBtnPos;
    }
    IEnumerator DeleteEffect(GameObject Clone)
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(Clone);
        SliderAniamtor.SetBool("Blink", false);
        if (SpriteChangeEvent)
        {
            BossManager.instance.Boss.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
            BossManager.instance.ChangeSprite(0);
            BossManager.instance.StopMove();
        }
        AttackUIAppear(Damage);
        yield return null;
    }
    void AttackUIAppear(int Damage)
    {
        if(SpriteChangeEvent)
        {
            SoundManager.instance.SFXPlay("Damaged", DamagedSound);
            BossManager.instance.ChangeSprite(0);
            SpriteChangeEvent = false;
        }
        if (StateManager.instance.Faze2 && IsMiss && !StateManager.instance.Last)
        {
            BossManager.instance.BossMiss("MISS");
        }
        else
        {
            SoundManager.instance.SFXPlay("Damaged", DamagedSound);
            BossManager.instance.BossHPChanged(Damage);
        }
    }
    void FightAndAttack()
    {
        AttackBar.SetActive(false);
        AttackSliding = false;
        Slide.SetActive(false);
        StateManager.instance._Fighting = false;
        StateManager.instance.Starting = false;
        StateManager.instance.Fighting = true;
        //공격 시에만 추가해야 하기 때문에
        PatternManager.instance.Before = PatternManager.instance.PatternCountFaze2++;
        Ttext.text = "";
        T_1.enabled = false;
        T_2.enabled = false;
        T1.enabled = true;
        if (!T2.enabled)
        {
            T2.enabled = true;
            Heart.transform.position = HeartMidPos;
        }
        Heart.SetActive(true);
        StartCoroutine("EnablePlayer", 0.3f);
        if (!StateManager.instance.Faze2) StartCoroutine(PatternManager.instance.SeqPatternStart(StateManager.instance.TurnCount));
        else StartCoroutine(PatternManager.instance.SeqPatternStart2(PatternManager.instance.PatternCountFaze2));
    }
    IEnumerator EnablePlayer()
    {
        HM.enabled = true;
        yield return null;
    }
    void Faze2Init()
    {
        ItemLocate = 0;
        Page = 1;
        PageAdd = false;
        i = 0;
        j = 0;
        StateManager.instance._Fighting = false;
        StateManager.instance.Talking = false;
        Ttext.gameObject.SetActive(true);
        Ttext.gameObject.GetComponent<TalkBox>().Talk(0.5f, StateManager.instance.DialogueChanger(StateManager.instance.TurnCount, Dialogue));
        Fight = true;
        Act = false;
        Item = false;
        Mercy = false;
        isMercyCheck = 0;
        isAct1Check = 0;
        isAct2Check = 0;
        isAct3Check = 0;
        isAct4Check = 0;
        StateManager.instance.TurnCount = 0;
        PatternManager.instance.PatternCountFaze2 = 0;
        PatternManager.instance.RandomNum = -1;
        ImageChanger(FightBtn, SeleteFight);
        HeartPos = FightBtnPos;
        Heart.SetActive(true);
    }
    public IEnumerator BossAttack(float Delay, params string[] logs)
    {
        for(int i = 0; i < logs.Length; i++)
        {
            DuringLogAppear(logs[i]);
            yield return new WaitForSeconds(Delay);
        }
        TalkBalloon.SetActive(false);
    }
    ///로그 출력 마지막에만 사용할 것. 기타 다른 상황에서는 사용 금지.
    public IEnumerator BossSpriteChangeLog(int[] SpriteNumber, int[] logCount, params string[] logs)
    {
        TalkBalloonText.gameObject.GetComponent<TextMeshPro>().text = "";
        for (int i = 0; i < logs.Length; i++)
        {
            DuringLogAppear(logs[i]);
            for (int j = 0; j < logCount.Length; j++)
            {
                if (logCount[j] == i)
                {
                    BossManager.instance.ChangeSprite(SpriteNumber[j]);
                    break;
                }
            }
            yield return new WaitForSeconds((logs[i].Length - 3) * 0.2f + 1); 
        }
        TalkBalloon.SetActive(false);
        AtkPtnA10M.enabled = false;
        StateManager.instance.Fighting = false;
        MyTurnBack();
        yield break;
    }
    void DuringLogAppear(string log)
    {
        TalkBalloon.SetActive(true);
        TalkBalloonText.Talk(0.2f, log);
    }
    void ItemPageUdate()
    {
        Ttext.text = "";
        ItemAndActText.SetActive(true);
        int Sum = ItemList.FindAll(n => n != "").Count();
        if (Sum > 6)
        {
            PageAdd = true;
            while (ItemList.Count < 12)
            {
                ItemList.Add("");
            }
        }
        while (ItemList.Count < 6)
        {
            ItemList.Add("");
        }
        //페이지 내에 있는 아이템 출력
        if (Page == 1)
        {
            for (int n = 0; n < 6; n++)
            {
                if (string.IsNullOrEmpty(ItemList[n]))
                {
                    //아이템이 비어있을 시 조건 설정
                    switch (n)
                    {
                        case 0:
                            ListELement[0, 0] = true;
                            break;
                        case 1:
                            ListELement[1, 0] = true;
                            break;
                        case 2:
                            ListELement[0, 1] = true;
                            break;
                        case 3:
                            ListELement[1, 1] = true;
                            break;
                        case 4:
                            ListELement[0, 2] = true;
                            break;
                        case 5:
                            ListELement[1, 2] = true;
                            break;
                    }
                    GameObject.Find("Element" + (n + 1)).GetComponent<TMP_Text>().text = "";
                }
                else
                {
                    //비어있지 않는 요소는 false로 바꾸기
                    switch (n)
                    {
                        case 0:
                            ListELement[0, 0] = false;
                            break;
                        case 1:
                            ListELement[1, 0] = false;
                            break;
                        case 2:
                            ListELement[0, 1] = false;
                            break;
                        case 3:
                            ListELement[1, 1] = false;
                            break;
                        case 4:
                            ListELement[0, 2] = false;
                            break;
                        case 5:
                            ListELement[1, 2] = false;
                            break;
                    }
                    GameObject.Find("Element" + (n + 1)).GetComponent<TMP_Text>().text = "* " + ItemList[n];
                }
            }
        }
        else
        {
            for (int e = 6; e < 12; e++)
            {
                if (string.IsNullOrEmpty(ItemList[e]))
                {
                    //아이템이 비어있을 시 조건 설정
                    switch (e)
                    {
                        case 6:
                            ListELement[0, 0] = true;
                            break;
                        case 7:
                            ListELement[1, 0] = true;
                            break;
                        case 8:
                            ListELement[0, 1] = true;
                            break;
                        case 9:
                            ListELement[1, 1] = true;
                            break;
                        case 10:
                            ListELement[0, 2] = true;
                            break;
                        case 11:
                            ListELement[1, 2] = true;
                            break;
                    }
                    GameObject.Find("Element" + (e - 5)).GetComponent<TMP_Text>().text = "";
                }
                else
                {                                //비어있지 않는 요소는 false로 바꾸기
                    switch (e)
                    {
                        case 6:
                            ListELement[0, 0] = false;
                            break;
                        case 7:
                            ListELement[1, 0] = false;
                            break;
                        case 8:
                            ListELement[0, 1] = false;
                            break;
                        case 9:
                            ListELement[1, 1] = false;
                            break;
                        case 10:
                            ListELement[0, 2] = false;
                            break;
                        case 11:
                            ListELement[1, 2] = false;
                            break;
                    }
                    GameObject.Find("Element" + (e - 5)).GetComponent<TMP_Text>().text = "* " + ItemList[e];
                }
            }
        }
    }
    void GameEnd()
    {
        AttackBar.SetActive(false);
        AttackSliding = false;
        Slide.SetActive(false);
        BossManager.instance.BossDisappear();
        StateManager.instance.GameDone = true;
        StartCoroutine(EndScene());
    }
    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(5f);
        BlackFade.SetActive(true);
        StartCoroutine(SoundManager.instance.SoundFadeOut(SoundManager.instance.b1, 1f));
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndingScene");
        yield break;
    }
    void BackKey()
    {
        Ttext.color = new Color(255, 255, 255);
        StateManager.instance.Acting = true;
        StateManager.instance.Starting = false;
        ActState(false);
        ActCancel();
        Page = 1;
        PageAdd = false;
        ItemLocate = 0;
        Bf_i = 0;
        Bf_j = 0;
        i = 0;
        j = 0;
        Ttext.text = "";
        ItemAndActText.SetActive(false);
        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, StateManager.instance.DialogueChanger(StateManager.instance.TurnCount, Dialogue));
        SoundManager.instance.SFXPlay("Move", MoveSound);
    }
}
