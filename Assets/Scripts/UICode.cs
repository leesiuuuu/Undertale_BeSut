using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
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
    [Header("Scripts")]
    public TextBoxToFightBox T1;
    public TextBoxToFightBox T2;
    public HeartMove HM;
    public ItemUse IU;
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
    void Start()
    {
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

        //Act ����Ʈ ��� �߰�
        ActList.Add("�߿˰Ÿ���");
        ActList.Add("���ϱ�");
        ActList.Add("�׺��ϱ�");
        ActList.Add("�𸣴� ô �ϱ�");
        ActList.Add("");
        ActList.Add("");
        
        ItemAndActText.SetActive(false);

        SliderAniamtor = Slide.GetComponent<Animator>();
        T1.enabled = false;
        T2.enabled = false;
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
        Heart.SetActive(true);
        HeartPos = FightBtnPos;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StateManager.instance.TurnCount++;
    }

    // Update is called once per frame
    void Update()
    {
        //UI Ű �̵� �ڵ�
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
        //UI ���� �� Text ���� �ڵ�
        else if(StateManager.instance.Starting && !AttackSliding)
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
                    StartCoroutine(ActAttacking());
                }
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
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ����� ����Ʈ �ڸ�����\n  �߿˰ŷȴ�.");
                    }
                    else if (HeartPos == ListPos[1, 0])
                    {
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ����� ����Ʈ �ڸ�����\n  �ֿ��� �ھҴ�.");
                    }
                    else if (HeartPos == ListPos[0, 1])
                    {
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ����� ����Ʈ �ڸ�����\n  �׺��Ѵٰ� ���ߴ�.");
                    }
                    else if (HeartPos == ListPos[1, 1])
                    {
                        Heart.SetActive(false);
                        Ttext.text = "";
                        Ttext.gameObject.GetComponent<TalkBox>().Talk(0, "* ����� ����Ʈ �ڸ�����\n  �ƹ��͵� ����� �ȳ��ٰ� ���ߴ�.");
                    }
                    StateManager.instance._Acting = false;
                }
            }
            else if (StateManager.instance._Iteming)
            {
                if (!isItemDialogue)
                {
                    Ttext.text = "";
                    ItemAndActText.SetActive(true);
                    if(ItemList.Count > 6)
                    {
                        PageAdd = true;
                        while(ItemList.Count < 12)
                        {
                            ItemList.Add("");
                        }
                    }
                    while(ItemList.Count < 6)
                    {
                        ItemList.Add("");
                    }
                    //������ ���� �ִ� ������ ���
                    if(Page == 1)
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

                    InteractiveSelete_Item();
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    isItemDialogue = true;
                    ImageChanger(ItemBtn, NormalItem);
                    SoundManager.instance.SFXPlay("Selete", SeleteSound);
                    IU.ItemUsed(Ttext, ItemAndActText, ItemList, ItemList[ItemLocate], Heart, ItemLocate);
                    StateManager.instance._Iteming = false;
                }
            }
            else if (StateManager.instance._Mercying)
            {
                if (!isMercyDialogue)
                {
                    Ttext.text = "";
                    Ttext.text = "      * ����ֱ�";
                    HeartPos = ListPos[0, 0];
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    ImageChanger(MercyBtn, NormalMercy);
                    SoundManager.instance.SFXPlay("Selete", SeleteSound);
                    Heart.SetActive(false);
                    Ttext.text = "";
                    isMercyDialogue = true;
                    StateManager.instance.Acting = false;
                    StateManager.instance.Fighting = true;
                    StateManager.instance._Mercying = false;
                }
            }
        }
        //Ȯ�� �ڵ�
        if (Input.GetKeyDown(KeyCode.Z) && StateManager.instance.Acting && !StateManager.instance.Starting)
        {
            StateManager.instance.Acting = false;
            StateManager.instance.Starting = true;
            ActState(true);
            SoundManager.instance.SFXPlay("Selete", SeleteSound);
        }
        //��� �ڵ�
        if (Input.GetKeyDown(KeyCode.X) && !StateManager.instance.Acting && StateManager.instance.Starting && !AttackSliding)
        {
            StateManager.instance.Acting = true;
            StateManager.instance.Starting = false;
            ActState(false);
            ActCancel();
            Page = 1;
            PageAdd = false;
            i = 0;
            j = 0;
            Ttext.text = "";
            ItemAndActText.SetActive(false);
            Ttext.gameObject.GetComponent<TalkBox>().Talk(0, StateManager.instance.DialogueChanger(StateManager.instance.TurnCount, Dialogue));
            SoundManager.instance.SFXPlay("Move", MoveSound);
        }
        //���� ���� �� �Ѿ�� �ڵ�
        if(StateManager.instance.Starting && StateManager.instance.Acting)
        {
            StateManager.instance.Starting = false;
            StateManager.instance.Acting = false;
            Invoke("StartFightTurn", 0.5f);
        }
        //��Ʈ ������Ʈ ��ġ ���������� Ȯ��
        if (!StateManager.instance.Fighting) Heart.transform.position = HeartPos;
        //���� ���� �� ���ϱ� �ڵ�
        else
        {
            if (isItemDialogue)
            {
                Ttext.text = "";
                T1.enabled = true;
                if (!T2.enabled)
                {
                    T2.enabled = true;
                    Heart.transform.position = HeartMidPos;
                }
                Heart.SetActive(true);
                HM.enabled = true;
                //���� ���� �ڵ�(�Ƹ� �ٸ� �ڵ忡�� ����� ��)
            }
            else if(isActDialogue)
            {
                if (!Once)
                {
                    zClick = 0;
                    Once = true;
                    isBossDialogue = true;
                }
                if (Input.GetKeyDown(KeyCode.Z) && isBossDialogue)
                {
                    if (HeartPos == ListPos[0, 0] && !StateManager.instance.Talking)
                    {
                        ++zClick;
                        switch (zClick)
                        {
                            case 1:
                                Ttext.text = "";
                                TalkBalloon.SetActive(true);
                                TalkBalloonText.Talk(0.2f, "....���� ���ϴ� ����?");
                                break;
                            case 2:
                                TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                                TalkBalloonText.Talk(0.2f, "���� �峭���ڴ� �ǰ�?");
                                break;
                            case 3:
                                TalkBalloon.SetActive(false);
                                TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                                isBossDialogue = false;
                                break;
                        }
                    }
                    else if (HeartPos == ListPos[1, 0] && !StateManager.instance.Talking)
                    {
                        ++zClick;
                        switch (zClick)
                        {
                            case 1:
                                Ttext.text = "";
                                TalkBalloon.SetActive(true);
                                TalkBalloonText.Talk(0.2f, "......");
                                break;
                            case 2:
                                TalkBalloon.SetActive(false);
                                TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                                isBossDialogue = false;
                                break;
                        }
                    }
                    else if (HeartPos == ListPos[0, 1] && !StateManager.instance.Talking)
                    {
                        ++zClick;
                        switch (zClick)
                        {
                            case 1:
                                Ttext.text = "";
                                TalkBalloon.SetActive(true);
                                TalkBalloonText.Talk(0.2f, "�����ͼ� �׺��ϰڴٰ�?");
                                break;
                            case 2:
                                TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                                TalkBalloonText.Talk(0.2f, "�ʹ� �ʾ���.");
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
                                DialogueAdder("��..?", "�ƹ��͵� ����� �ȳ�..?", "��ģ ô �غ��� �����״� �ҿ����.");
                                break;
                            case 1:
                                DialogueAdder("...������ ���� ��.", "��� ����� �ȳ��ٰ� ��ġ�� ������ ������.", "������ �����.");
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
                    //���� ���� �ڵ�(�Ƹ� �ٸ� �ڵ忡�� ����� ��)
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
                if (Input.GetKeyDown(KeyCode.Z) && isBossDialogue)
                {
                    ++zClick;
                    switch (zClick)
                    {
                        case 1:
                            Ttext.text = "";
                            TalkBalloon.SetActive(true);
                            TalkBalloonText.Talk(0.2f, "�����ͼ� ����ְڴٰ�?");
                            break;
                        case 2:
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            TalkBalloonText.Talk(0.2f, "�ں�� ����.");
                            break;
                        case 3:
                            TalkBalloon.SetActive(false);
                            TalkBalloonText.gameObject.GetComponent<TMP_Text>().text = "";
                            isBossDialogue = false;
                            break;
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
                    //���� ���� �ڵ�(�Ƹ� �ٸ� �ڵ忡�� ����� ��)
                }
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
                    Page = 2;
                    i = 0;
                    ItemLocate += 5;
                    //����ִ� ��ҿ� ���� UI ����
                    switch (SecPage_Element)
                    {
                        case 1:
                            if(j == 2 || j == 1)
                            {
                                j = 0;
                                ItemLocate = 6;
                            }
                            break;
                        case 2:
                            if(j == 2)
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
                if (i == 0) i++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //������ �Ѿ �� ��� ���� �����ϰ� ����
            if (PageAdd)
            {
                if (Page == 2 && i == 0)
                {
                    Page = 1;
                    i = 1;
                    ItemLocate -= 5;
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
                if (i == 1) i--;
            }
        }
        if (ListELement[i, j])
        {
            if(Page == 1)
            {
                
            }
            else
            {
                HeartPos = Before;
                ItemLocate = BeforeItemLocate;
                i = Bf_i; j = Bf_j;
            }
        }
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
    private IEnumerator ActAttacking()
    {
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
                Debug.Log(DistanceCheck.DistancetoDamage(DistanceChecker.transform.position.x, Slide.transform.position.x));
            }
        }
        if (Stop)
        {
            //���� �� ������ UI ���� �� ���� â �Ѿ��
        }
        else
        {
            Slide.transform.position = SlideEndPos;
            Heart.SetActive(true);
            HeartPos = FightBtnPos;
            AttackBar.SetActive(false);
            Slide.SetActive(false);
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
    void MyTurnBack()
    {
        isActDialogue = false;
        isItemDialogue = false;
        Once = false;
    }
}
