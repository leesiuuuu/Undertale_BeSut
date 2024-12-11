using UnityEngine;
using System.Collections;

public class HeartMove : MonoBehaviour
{
    //Shake 기본 값
    //0.5f, 1f
    //빌드 기본값
    //0.5f, 0.5f
    private Vector3 MoveVelocity;
    public float MoveSpeed;
    public GameObject ShieldObj;
    public AudioClip HurtClip;
    public AudioClip ShieldSound;
    public AudioClip SheldBreak;
    private bool isInvin = false;
    private Animator animator;
    private bool Shield = false;
    public bool NoCool = false;
    private float CoolTime;
    private int damage = 8;
    public float MAX_COOLTIME = 3f;
    private bool NotStartFaze2 = true;
    [SerializeField]
    private ShieldMove SM;
    private bool OnceToogle = false;
    [SerializeField]
    private GameObject ShieldBarr;

    bool _Once = false;

    [HideInInspector]
    public bool Pattern3Start;
    private void OnEnable()
    {
        if (Shield)
        {
            ShieldObj.SetActive(true);
        }
        if (OnceToogle && StateManager.instance._10Ptn)
        {
            ShieldBarr.SetActive(true);
        }
    }
    private void Start()
    {
        CoolTime = 0f;
        ShieldObj = transform.GetChild(0).gameObject;
        ShieldObj.SetActive(false);
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (StateManager.instance.Faze2 && NotStartFaze2)
        {
            MAX_COOLTIME = MAX_COOLTIME / 2;
            NotStartFaze2 = false;
        }
        if (!Shield && !NoCool)
        {
            if(CoolTime <= 0)
            {
                CoolTime = 0;
            }
            else
            {
                CoolTime -= Time.deltaTime;
            }
        }
        MoveVelocity = Vector3.zero;
        //레이저 방어막 생성 패턴
        if (Input.GetKeyDown(KeyCode.V) && StateManager.instance._10Ptn)
        {
            //Off
            if (OnceToogle)
            {
                ShieldBarr.SetActive(false);
                SM.enabled = false;
                OnceToogle = false;
            }
            //On
            else
            {
                ShieldBarr.SetActive(true);
                SM.enabled = true;
                OnceToogle = true;
            }
        }
        //2페이즈 4패턴 실행 시 움직임 방식 변경
        if (PatternManager.instance.isSpeicalMove)
        {
            if (!_Once)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    -1.37f,
                    transform.position.z);
                _Once = true;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < -0.67f)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y + 0.7f,
                    transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -2.07f)
            {
                transform.position = new Vector3(
                transform.position.x,
                transform.position.y - 0.7f,
                transform.position.z);
            }
        }
        else
        {
            _Once = false;
            if (Input.GetKey(KeyCode.UpArrow)) MoveVelocity += Vector3.up;
            if (Input.GetKey(KeyCode.DownArrow)) MoveVelocity += Vector3.down;
        }
        //변경되지 않는 설정들
        if (Input.GetKey(KeyCode.LeftArrow)) MoveVelocity += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) MoveVelocity += Vector3.right;
        if (Input.GetKeyDown(KeyCode.C) && (CoolTime <= 0 || NoCool))
        {
            SoundManager.instance.SFXPlay("Shield", ShieldSound);
            if (Pattern3Start)
            {
                if (!Shield)
                {
                    ShieldObj.SetActive(true);
                    MoveSpeed -= 2f;
                    if (!NoCool) CoolTime = MAX_COOLTIME;
                    Shield = true;
                }
                else
                {
                    ShieldObj.SetActive(false);
                    MoveSpeed += 2f;
                    Shield = false;
                }
            }
            else if (!Shield)
            {
                ShieldObj.SetActive(true);
                MoveSpeed -= 2f;
                if (!NoCool) CoolTime = MAX_COOLTIME;
                Shield = true;
            }
        }
        MoveVelocity = MoveVelocity.normalized;
        transform.position += MoveVelocity * MoveSpeed * Time.deltaTime;
        
    }
    private void OnTriggerEnter2D(Collider2D cols)
    {
        if (cols.gameObject.CompareTag("AttackSprite"))
        {
            if (!isInvin && !Shield)
            {
                Damaged();
            }
            else if (Shield)
            {
                Shield = false;
                ShieldObj.SetActive(false);
                MoveSpeed += 2f;
                SoundManager.instance.SFXPlay("ShieldBreak", SheldBreak);
            }
        }
    }
    void ReturnInvin()
    {
        isInvin = false;
        animator.SetBool("Hurted", false);
    }
    private void OnDisable()
    {
        ShieldObj.SetActive(false);
        ShieldBarr.SetActive(false);
    }
    IEnumerator Shake(GameObject obj, float Duration = 1f, float Power = 1f)
    {
        Vector3 origin = obj.transform.position;
        while (Duration > 0f)
        {
            Duration -= 0.05f;
            obj.transform.position = origin + (Vector3)Random.insideUnitCircle * Power * Duration;
            yield return null;
        }
        obj.transform.position = origin;
    }
    public void Damaged(int LaserDamage = 0)
    {
        if(LaserDamage == 0)
        {
            PlayerManager.instance.HP -= StateManager.instance.BetrayalFaze2 ? damage * 2 : StateManager.instance.NormalFaze2 ? (int)(damage * 1.5f) : damage;
        }
        else
        {
            if(!isInvin) PlayerManager.instance.HP -= LaserDamage;
        }
        SoundManager.instance.SFXPlay("Hurt", HurtClip);
        if(LaserDamage == 0)
        {
            StartCoroutine(Shake(gameObject, 0.5f, 1f));
            isInvin = true;
            PlayerManager.instance.HPChanged();
            animator.SetBool("Hurted", true);
            Debug.Log(PlayerManager.instance.HP);
            Invoke("ReturnInvin", 0.7f);
        }
        else
        {
            PlayerManager.instance.HPChanged();
            //animator.SetBool("Hurted", true);
        }
    }
}
