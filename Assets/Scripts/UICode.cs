using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    //출력 대사
    private string Dialogue;

    //공격 슬라이더 Blink 애니메이션
    private Animator SliderAniamtor;

    //Item창에서 이전 선택 위치
    private Vector3 Before;
    private int Bf_i;
    private int Bf_j;

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
    void Start()
    {
        //리스트 요소 추가
        ItemList.Add("자연산 커피");
        ItemList.Add("마법 롤케이크");
        ItemList.Add("마법 소다");
        ItemList.Add("마법 소다");
        ItemList.Add("마법 소다");
        /*
        ItemList.Add("뮤즈 룰스");
        ItemList.Add("뮤즈 룰스");
        ItemList.Add("뮤즈 룰스");
        ItemList.Add("특제 케-이크");*/

        //Act 리스트 요소 추가
        ActList.Add("야옹거리기");
        ActList.Add("욕하기");
        ActList.Add("항복하기");
        ActList.Add("모르는 척 하기");
        ActList.Add("");
        ActList.Add("");
        
        ItemAndActText.SetActive(false);

        SliderAniamtor = Slide.GetComponent<Animator>();
        T1.enabled = false;
        T2.enabled = false;
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
        Heart.SetActive(true);
        HeartPos = FightBtnPos;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StateManager.instance.TurnCount++;
    }

    // Update is called once per frame
    void Update()
    {
        //UI 키 이동 코드
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
        //UI 선택 시 Text 변경 코드
        else if(StateManager.instance.Starting && !AttackSliding)
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
            else if (StateManager.instance._Iteming)
            {
                if (!isItemDialogue)
                {
                    Ttext.text = "";
                    ItemAndActText.SetActive(true);
                    if(ItemList.Count > 6)
                    {
                        PageAdd = true;
                    }
                    while(ItemList.Count < 6)
                    {
                        ItemList.Add("");
                    }
                    //아이템 출력 코드
                    for(int n = 0; n < 6; n++)
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
                            GameObject.Find("Element" + (n + 1)).GetComponent<TMP_Text>().text = "* " + ItemList[n];
                        }
                    } 
                    InteractiveSelete_Item();
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
        if (Input.GetKeyDown(KeyCode.X) && !StateManager.instance.Acting && StateManager.instance.Starting && !AttackSliding)
        {
            StateManager.instance.Acting = true;
            StateManager.instance.Starting = false;
            ActState(false);
            ActCancel();
            i = 0;
            j = 0;
            Ttext.text = "";
            ItemAndActText.SetActive(false);
            Ttext.gameObject.GetComponent<TalkBox>().Talk(0, StateManager.instance.DialogueChanger(StateManager.instance.TurnCount, Dialogue));
            SoundManager.instance.SFXPlay("Move", MoveSound);
        }
        //보스 전투 턴 넘어가는 코드
        if(StateManager.instance.Starting && StateManager.instance.Acting)
        {
            StateManager.instance.Starting = false;
            StateManager.instance.Acting = false;
            Invoke("StartFightTurn", 0.5f);
        }
        //하트 오브젝트 위치 지속적으로 확인
        if (!StateManager.instance.Fighting) Heart.transform.position = HeartPos;
        //보스 전투 및 말하기 코드
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
                            TalkBalloonText.Talk(0.2f, "......");
                            break;
                        case 2:
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
    void InteractiveSelete_Item()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (j >= 0 && j < 2) j++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (j > 0 && j < 3) j--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (i == 0) i++;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (i == 1) i--;
        }
        Debug.Log("ListElement[" + i + ", " + j + "] = " + ListELement[i, j]);
        if (ListELement[i, j])
        {
            HeartPos = Before;
            i = Bf_i; j = Bf_j;
        }
        else
        {
            HeartPos = ListPos[i, j];
            Before = ListPos[i, j];
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
            Slide.transform.Translate(Vector3.right * 5 * Time.deltaTime);
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
            //공격 후 데미지 UI 생성 및 전투 창 넘어가기
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
}
