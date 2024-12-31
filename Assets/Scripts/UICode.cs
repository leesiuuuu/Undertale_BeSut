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

    //��� ���
    private string Dialogue;

    //���� �����̴� Blink �ִϸ��̼�
    private Animator SliderAniamtor;

    //Itemâ���� ���� ���� ��ġ
    private Vector3 Before;
    private int BeforeItemLocate;
    private int Bf_i;
    private int Bf_j;

    /// <summary>
    /// �� ��° ������ ����ִ� ��� ����
    /// 1 : 1 ~ 2���� ��� ����
    /// 2 : 3 ~ 4���� ��� ����
    /// 3 : 5 ~ 6���� ��� ����
    /// </summary>
    private int SecPage_Element = 0;

    private int Damage;

    private int MAX_ITEM_LOCATE;
    private int ItemLocate = 0;

    private Vector3 HeartPos;
    //UI ��Ʈ ��ġ
    private Vector3 FightBtnPos = new Vector3(-5.53f, -4.23f, 0f);
    private Vector3 ActBtnPos = new Vector3(-2.28f, -4.23f, 0f);
    private Vector3 ItemBtnPos = new Vector3(0.86f, -4.23f, 0f);
    private Vector3 MercyBtnPos = new Vector3(4.07f, -4.23f, 0f);
    //���� ��Ʈ ��ġ
    private Vector3 HeartMidPos = new Vector3(0f, -1.35f, 0f);
    //������ �� ��ȣ�ۿ� ���� ��Ʈ ��ġ
    private Vector3[ , ] ListPos = new Vector3[2, 3];
    //������ ��� �ִ��� ������ Ȯ��

    private bool[ , ] ListELement = new bool[2, 3];

    private int i = 0, j = 0;
    //���� Act ��Ȳ �޽��� ��� ������ Ȯ��
    private bool isActDialogue = false;
    //���� Item ��Ȳ �޽��� ��� ������ Ȯ��
    private bool isItemDialogue = false;
    //���� Mercy ��Ȳ �޽��� ��� ������ Ȯ��
    private bool isMercyDialogue = false;
    //���� ��ȭ Ŭ�� Ƚ��
    private int zClick = 0;
    //�����̴� �� ��ġ
    private Vector3 SlideEndPos = new Vector3(5.39f, -1.49f, 432.4628f);
    //�����̴� ���� ��ġ
    private Vector3 SlideStartPos = new Vector3(-5.39f, -1.49f, 432.4628f);
    //Attack ���� �� ���� �� �Ǻ� bool
    private bool AttackSliding = false;
    //�ѹ��� �۵��� bool
    private bool Once = false;
    private bool isBossDialogue = false;
    private bool isCountStarted = false;
    private bool GameDoneLogAdd = false;
    //�� �ൿ Ƚ���� ���� ��ȭ �α� ����
    private int isMercyCheck = 0;
    private int isAct1Check = 0;
    private int isAct2Check = 0;
    private int isAct3Check = 0;
    private int isAct4Check = 0;
    private int Faze2ActCheck = 0;
    /// <summary>
    /// Meow = �߿˰Ÿ���
    /// Surnen = �׺��ϱ�
    /// Swear = �ֿ�
    /// IDK = ��
    /// IDK_out = ��(�һ쿡�� ��ַ� �Ѿ ��)
    /// Mercy = �ں�
    /// None = 2������
    /// </summary>
    private string LastState;
    //ù���� �Ͽ��� �𸣴� ô �ϱ⸦ ���� �� ����
    public bool FirstTurnAct = false;
    //�һ� 2������ ��Ʈ�� ���� �� ����
    private bool SpriteChangeEvent = false;
    //���Ұ��� �Ǵ�
    private bool IsMiss;

    private bool isOnce123 = false;
    private void Awake()
    {
        StateManager.instance.GameDone = false;
        scalemove.enabled = true;
        //�����ִ� ��� Ȯ��
        if(ItemList.Count > 0 ) ItemList.Clear();
        //����Ʈ ��� �߰�
        ItemList.Add("�ڿ��� Ŀ��");
        ItemList.Add("���� ������ũ");
        ItemList.Add("���� �Ҵ�");
        ItemList.Add("���� �Ҵ�");
        ItemList.Add("���� �Ҵ�");
        ItemList.Add("���� �꽺");
        ItemList.Add("���� �꽺");
        ItemList.Add("���� �꽺");
        ItemList.Add("Ư�� ��-��ũ");
        MAX_ITEM_LOCATE = ItemList.Count;

        //Act ����Ʈ ��� �߰�
        if(ActList.Count > 0) ActList.Clear();
        ActList.Add("�߿˰Ÿ���");
        ActList.Add("���ϱ�");
        ActList.Add("�׺��ϱ�");
        ActList.Add("�𸣴� ô �ϱ�");
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
        //�迭 �ʱ�ȭ
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
        //UI Ű �̵� �ڵ�
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
        //���������� ���ݸ� �� �� �ְ� ��
        else if (StateManager.instance.Last && StateManager.instance.logAppear)
        {
            ActBtn.color = new Color(1, 1, 1, 0.3f);
            ItemBtn.color = new Color(1, 1, 1, 0.3f);
            MercyBtn.color = new Color(1, 1, 1, 0.3f);
        }
        //UI ���� �� Text ���� �ڵ�
        else if(StateManager.instance.Starting && !AttackSliding && !GameDoneLogAdd)
        {
            if (StateManager.instance._Fighting)
            {
                Ttext.text = "";
                Ttext.text = "      * ����Ʈ �ڸ�";
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
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ����� ����Ʈ �ڸ�����\n  �߿˰ŷȴ�.");
                    }
                    else if (HeartPos == ListPos[1, 0])
                    {
                        FirstTurnAct = false;
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ����� ����Ʈ �ڸ�����\n  �ֿ��� �ھҴ�.");
                    }
                    else if (HeartPos == ListPos[0, 1])
                    {
                        FirstTurnAct = false;
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ����� ����Ʈ �ڸ�����\n  �׺��Ѵٰ� ���ߴ�.");
                    }
                    else if (HeartPos == ListPos[1, 1])
                    {
                        Heart.SetActive(false);
                        if (StateManager.instance.TurnCount == 1) FirstTurnAct = true;
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ����� ����Ʈ �ڸ�����\n  �ƹ��͵� ����� �ȳ��ٰ� ���ߴ�.");
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
                        Ttext.text = "      * ����ֱ�";
                    }
                    else
                    {
                        Ttext.text = "";
                        Ttext.text = "      * ����ֱ�";
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
            //Ȯ�� �ڵ�
            if (Input.GetKeyDown(KeyCode.Z) && StateManager.instance.Acting && !StateManager.instance.Starting)
            {
                StateManager.instance.Acting = false;
                StateManager.instance.Starting = true;
                ActState(true);
                SoundManager.instance.SFXPlay("Selete", SeleteSound);
            }
            //��� �ڵ�
        }
        //���� ���� �� �Ѿ�� �ڵ�
        if(StateManager.instance.Starting && StateManager.instance.Acting && !StateManager.instance.GameDone)
        {
            if(BossManager.instance.bossHP > 1)
            {
                StateManager.instance.Starting = false;
                StateManager.instance.Acting = false;
                Invoke("StartFightTurn", 0.5f);
            }
        }
        //��Ʈ ������Ʈ ��ġ ���������� Ȯ��
        if (!StateManager.instance.Fighting && !GameDoneLogAdd)
        {
            Heart.transform.position = HeartPos;
        }
        //���� ���� �� ���ϱ� �ڵ�
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
                                DialogueAdder("���� �ൿ���� �� ������ �ְ���.", "���� ������.");
                            }
                            else
                            {
                                switch (isAct1Check)
                                {
                                    case 0:
                                        DialogueAdder("���� ���ϴ� ����?", "���� �峭���ڴ� �ǰ�?");
                                        break;
                                    case 1:
                                        DialogueAdder(".....��ģ�ų�?", "¡�׷��� �װڱ�.");
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
                                DialogueAdder("�̷� �ൿ�� �ϴ� �� �� ������ �ְ���.", "���� ���� �ͱ�.");
                            }
                            else
                            {
                                switch (isAct2Check)
                                {
                                    case 0:
                                        DialogueAdder(".......", "�װ� �ٳ�?");
                                        break;
                                    case 1:
                                        DialogueAdder("�ѽ��ϱ�...");
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
                                DialogueAdder("�̷� �ൿ�� �ϴ� �� �� ������ �ְ���.", "���� �̷��� ��Ű�� ����?", "���� ���� �ͱ�.");
                            }
                            else
                            {
                                if (FirstTurnAct) FirstTurnAct = false;
                                switch (isAct3Check)
                                {
                                    case 0:
                                        DialogueAdder("�����ͼ� �׺��ϰڴٰ�?", "�ʹ� �ʾ���.");
                                        break;
                                    case 1:
                                        DialogueAdder("�� �׺��ϴ� ����?", "���� �����?");
                                        break;
                                    case 2:
                                        DialogueAdder("�׷��� ���⼭ ����� �ͳ�?", "����.", "�ִ��� ������ ��������."); ;
                                        break;
                                    case 3:
                                        DialogueAdder("��ġ���� �ʴ±�.");
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
                                DialogueAdder("�׷�, ��¥ �ƹ� ��ﵵ �ȳ��� �� ������.", "�׷��� ���� ������.");
                                LastState = "IDK";
                            }
                            else
                            {
                                if (FirstTurnAct)
                                {
                                    switch (StateManager.instance.NoLieStack)
                                    {
                                        case 0:
                                            DialogueAdder("��..?", "�ƹ��͵� ����� �ȳ�..?", "��ģ ô �غ��� �����״� �ҿ����.");
                                            break;
                                        case 1:
                                            DialogueAdder("...������ ���� ��.", "��� ����� �ȳ��ٰ� ��ġ�� ������ ������.", "������ �����.");
                                            break;
                                        case 2:
                                            DialogueAdder("�����̳�?", "���� ������ ����� �ȳ��ٸ�...", "......�ƴ�, �ƴϾ�.", "�׷����� ����.");
                                            break;
                                        case 3:
                                            DialogueAdder("�ƹ��͵� ����� �ȳ�..?",
                                                "������?",
                                                "�������� �ʴ� �� ������....",
                                                "��¼�� ����� ���� �ְڱ�.");
                                            break;
                                        case 4:
                                            DialogueAdder("�׷� ���� ������ ���� �ڱ���..",
                                                "������ �� �ֳ�?",
                                                "�ƴϸ�, ���� ���� ��Ȳ������ �𸣸鼭 �ο�� ������ �ǰ�?",
                                                "��....");
                                            break;
                                        case 5:
                                            DialogueAdder("������ �� ������ ������� �׿���.",
                                                "�װ� ���� �뼭 �� �� ����.",
                                                "�ƹ��� ����� �� ���ٰ� �ص�..",
                                                "�� �ᱹ���� �װ� �ɰŴ�.");
                                            break;
                                        case 6:
                                            DialogueAdder("���� �����ⱺ.",
                                                "�� ���� ������ �ʶ� �Ȱ���.",
                                                ".....");
                                            break;
                                        case 7:
                                            DialogueAdder("������ �ʸ� �Ϻ��ϰ� �� �ϰھ�.",
                                                "����ؼ� ���� �ϰ� �غ�.",
                                                "���� ���� ����� �ȳ��ٸ�,",
                                                "�� ���� ������ ������ ����.");
                                            break;
                                        case 8:
                                            DialogueAdder("Ȥ�� ����...",
                                                "���������� �������ϰ� �ֳ�?",
                                                "��, �ƴϴ�.",
                                                "������ �� ���� �ƴϾ�.");
                                            break;
                                        case 9:
                                            DialogueAdder("����.",
                                                "�������� ������� �ŷ��� �� �ϱ�.",
                                                "�ʿ��� �ں� ��Ǯ���ָ�.");
                                            break;
                                    }
                                    LastState = "IDK";
                                }
                                else
                                {
                                    switch (isAct4Check)
                                    {
                                        case 0:
                                            DialogueAdder("�� ���� ��ü�� �˰� �ִ�.", "�׷� �������� ������.");
                                            break;
                                        case 1:
                                            DialogueAdder("�ҿ����.", "�� �̻� �ǹ� ����.");
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
                                    DialogueAdder("���̻��� ������ ����.", "��� ��䵵 ���� ���� �Ŵ�.");
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
                                DialogueAdder("�����ͼ� ����ְڴٰ�?", "�ں�� ����.");
                                break;
                            case 1:
                                DialogueAdder("���� �׿�����, �����ͼ� ����شٰ�?", "�ѽ��ϱ� ¦�� ����.");
                                break;
                            case 2:
                                DialogueAdder("������ �ϱ� �ȱ�.", "�׾��.");
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
                                DialogueAdder("������ ����� �����ϴٴ�.", "�׷��� ��� �ͳ�?");
                                break;
                            default:
                                DialogueAdder("�� �̻� ����� ������ ����.");
                                break;
                        }
                    }
                    else
                    {
                        DialogueAdder("�ڼ��� �̾߱�� �� ���鼭 ������ ����.");
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
                    //�һ� ��Ʈ ���� �ڵ�(���� ����)
                    if (StateManager.instance.NoKill && !GameDoneLogAdd)
                    {
                        StateManager.instance.GameDone = true;
                        Debug.Log("GameDone");
                        isCountStarted = true;
                    }
                    //GameDone ���� �ݺ��� �� ��Ÿ���� ������ ������
                    else if (StateManager.instance.NoKill && GameDoneLogAdd)
                    {
                        Debug.Log("GameEnding...");
                    }
                    //��Ÿ
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
        //������ ����Ǿ����� Ȯ��
        if (StateManager.instance.GameDone && !GameDoneLogAdd)
        {
            Ttext.text = "";
            Ttext.color = new Color(255, 255, 255);
            if(StateManager.instance.Last) Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ���� ����!\n* ����� 128xp�� 999��带 �����!");
            else Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ���� ����!\n* ����� 0xp�� 0��带 �����!");
            StateManager.instance.GameDone = false;
            GameDoneLogAdd = true;
            if(!StateManager.instance.NormalFaze2 && !StateManager.instance.BetrayalFaze2) StateManager.instance.MercyEnd = true;
            StartCoroutine(EndScene());
        }
        if (PlayerManager.instance.HP <= 0) PlayerManager.instance.Death();
        //���� ���� �ڵ�
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitGame());
        }
    }
    //���Ŀ� �� �ٷ� ����
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
            //������ �Ѿ �� ��� ���� �����ϰ� ����
            if (PageAdd)
            {
                if (Page == 1 && i == 1)
                {
                    Page = 2;
                    //���� ����ִ� ��� ���� ����
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
                    //����ִ� ��ҿ� ���� UI ����
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
            //������ �Ѿ �� ��� ���� �����ϰ� ����
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
        //��Ұ� �������� ���� �� �߻�
        if (ListELement[i, j])
        {
            Debug.Log("Befored");
            HeartPos = Before;
            ItemLocate = BeforeItemLocate;
            i = Bf_i; j = Bf_j;
        }
        //��Ұ� ������ �� ����
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
    //��縦 �ް� �������
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
            //���� �� ������ UI ���� �� ���� â �Ѿ��
            if (StateManager.instance.Faze2 && IsMiss && !StateManager.instance.Last)
            {
                StartCoroutine(BossManager.instance.BossMoveToLeftOrRight(BossManager.instance.MissAnimDuration));
            }
            GameObject clone = Instantiate(AttackEffect);
            SoundManager.instance.SFXPlay("Attack", AttackSound);
            StartCoroutine(DeleteEffect(clone));
            yield return new WaitForSeconds(0.9f);
            if(StateManager.instance.NoLieStack == 8) //������ ������ ���ٰ� �� �� �����ϸ� ��Ÿ���� ����
            {
                yield return new WaitForSeconds(0.7f);
                AttackBar.SetActive(false);
                Slide.SetActive(false);
                FirstTurnAct = false;
                isBossDialogue = true;
                StartCoroutine(AchievementManager.instance.AchiUIAppearence(10));
                zClick = 1;
                DialogueAdder("����...");
                while (zClick <= 3){
                    if (Input.GetKeyDown(KeyCode.Z) && !StateManager.instance.Talking)
                    {
                        DialogueAdder("�������̾���.", "�������.");
                        ++zClick;
                    }
                    yield return null;
                }
                StateManager.instance.NoLieStack = 0;
                TalkBalloon.SetActive(false);
                LastState = "IDK_out";
            }
            //���� ü�� ���� �̻� �Ҹ��� �� 2������ �Ѿ
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
                DialogueAdder("���� ����� �� ���̷��� ��±�.");
                while (zClick < 7)
                {
                    if (zClick == 6)
                    {
                        BossManager.instance.ChangeSprite(4);
                    }
                    if (Input.GetKeyDown(KeyCode.Z) && !StateManager.instance.Talking)
                    {
                        DialogueAdder("��� �� ������ �ϰ� �־���.",
                            "�ʰ� ������ ���߰� �׺��ϱ⸦ �ٷ��µ�.",
                            "�̷��� ���� ���� �� ������ �������ٴ�.",
                            "��, �ƽ��� �Ȱ���.",
                            "�� Ȯ���ϰ� �׿��ٰ�.");
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
                DialogueAdder("��� �ѹ� �غ��ڰ�.");
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
                //2������ ���� �˸�
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
            //2������ ���ǹ�
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
                            "�̷��� ���� ���� ������ ���ߴµ�.",
                            ".............",
                            "���� �� ��û�ϱ�.",
                            "���� �ʼ��� �ϴ� �� �ƴϾ���.",
                            "���ۿ� ��ġë��� �ߴµ�.",
                            "...",
                            "....�ƴ�",
                            "���⼭ ���� �� ����.",
                            "���ƾ߸� ��.",
                            "������...");
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
                DialogueAdder("���� ������ �ʴ´�.");
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
                //2������ ���� �˸�
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
    //���� �� ���� �� �ʱ�ȭ
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
        //���� �ÿ��� �߰��ؾ� �ϱ� ������
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
    ///�α� ��� ���������� ����� ��. ��Ÿ �ٸ� ��Ȳ������ ��� ����.
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
        //������ ���� �ִ� ������ ���
        if (Page == 1)
        {
            for (int n = 0; n < 6; n++)
            {
                if (string.IsNullOrEmpty(ItemList[n]))
                {
                    //�������� ������� �� ���� ����
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
                    //������� �ʴ� ��Ҵ� false�� �ٲٱ�
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
                    //�������� ������� �� ���� ����
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
                {                                //������� �ʴ� ��Ҵ� false�� �ٲٱ�
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
