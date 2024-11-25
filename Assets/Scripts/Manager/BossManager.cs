using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;
public class BossManager : MonoBehaviour
{
    //shake 기본 값
    //4, 0.2f
    //빌드 기본값
    //1, 0.1f
    public static BossManager instance;
    public int bossHP;
    public int MAX_BOSS_HP = 1000;

    public GameObject Boss;
    private SpriteRenderer BSR;

    public bool shakeing;

    public BossMove BM;

    public TMP_Text DamageText;
    public Image BossUI;

    public float MissAnimDuration;

    [Header("Sprites")]
    public Sprite bossHurt;
    public Sprite bossHurtTalk;
    public Sprite bossUlt;
    public Sprite bossHurtStand;
    [Header("NormalSprite")]
    public Sprite NormalBossStand;
    public Sprite NormalBossUlt;

    private void Awake()
    {
        shakeing = false;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        bossHP = MAX_BOSS_HP;
        BSR = Boss.GetComponent<SpriteRenderer>();
    }
    public void BossHPHeal(int Cure)
    {
        DamageText.gameObject.SetActive(true);
        DamageText.text = "";
        DamageText.outlineColor = Color.green;
        DamageText.text = Cure.ToString();
        bossHP += Cure;
        float HPDetail1 = bossHP / (float)MAX_BOSS_HP;
        BossUI.fillAmount = HPDetail1;
        Invoke("ReturnBoss", 0.8f);
    }
    public void BossHPChanged(int Damage)
    {
        if(bossHP - Damage < 0)
        {
            bossHP = 0;
        }
        DamageText.gameObject.SetActive(true);
        DamageText.text = "";
        DamageText.text = Damage.ToString();
        bossHP -= Damage;
        float HPDetail1 = bossHP / (float)MAX_BOSS_HP;
        BossUI.fillAmount = HPDetail1;
        BossUI.transform.parent.gameObject.SetActive(true);
        shakeing = true;
        StartCoroutine(Shake(Boss, 1.1f, 0.3f));
        Invoke("ReturnBoss", 0.8f);
    }
    public void BossMiss(string Miss)
    {
        DamageText.gameObject.SetActive(true);
        DamageText.text = "";
        DamageText.outlineColor = Color.gray;
        DamageText.text = Miss;
        BossUI.transform.parent.gameObject.SetActive(true);
        Invoke("ReturnBoss", 0.8f);
    }
    void ReturnBoss()
    {
        DamageText.gameObject.SetActive(false);
        BossUI.transform.parent.gameObject.SetActive(false);
        DamageText.outlineColor = Color.red;
        shakeing = false;
    }
    public IEnumerator Shake(GameObject obj, float Duration = 1f, float Power = 1f)
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

    /// <summary>
    /// 0 = bossHurt
    /// 1 = bossHurtTalk
    /// 2 = bossUlt
    /// 3 = bossHurtStand
    /// 4 = NoramlBossStand
    /// 5 = NormalBossUlt;
    /// </summary>
    /// <param name="index"></param>
    public void ChangeSprite(int index)
    {
        switch (index)
        {
            case 0:
                BSR.sprite = bossHurt; break;
            case 1:
                BSR.sprite = bossHurtTalk; break;
            case 2:
                BSR.sprite = bossUlt; break;
            case 3:
                BSR.sprite = bossHurtStand; break;
            case 4:
                BSR.sprite = NormalBossStand; break;
            case 5:
                BSR.sprite = NormalBossUlt; break;
        }
    }
    public void StopMove()
    {
        BM.enabled = false;
        Boss.transform.position = new Vector3 (0, 2.56f, 0);
    }
    public void StartMove()
    {
        if (!BM.enabled)
        {
            BM.enabled = true;
            BM.EndPosition = new Vector2(0, 2.8f);
        }
    }
    public void Faze2Change()
    {
        if (StateManager.instance.BetrayalFaze2)
        {
            Boss.GetComponent<SpriteRenderer>().sprite = bossUlt;
        }
        else
        {
            Boss.GetComponent<SpriteRenderer>().sprite = NormalBossUlt;
        }
        
    }
    public IEnumerator BossMoveToLeftOrRight(float Duration)
    {
        //true = left false = right
        bool leftOrRigh = (Random.value > 0.5f);
        Vector2 StartPos = new Vector2(0, 2.56f);
        Vector2 EndPos = new Vector2(leftOrRigh ? -2 : 2, 2.56f);

        float TimeElapsed = 0;

        yield return StartCoroutine(Miss(TimeElapsed, Duration, StartPos, EndPos));
    }
    private IEnumerator Miss(float TimeElapsed, float Duration, Vector2 StartPos, Vector2 EndPos)
    {
        BM.enabled = false;
        while (TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration;
            t = easeOutQuint(t);
            Boss.transform.position = Vector2.Lerp(StartPos, EndPos, t);
            yield return null;
        }
        TimeElapsed = 0;
        Vector2 temp = EndPos;
        EndPos = StartPos;
        StartPos = temp;
        while(TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration;
            t = easeInQuint(t);
            Boss.transform.position = Vector2.Lerp(StartPos, EndPos, t);
            yield return null;
        }
        BM.enabled = true;
    }
    private float easeOutQuint(float x)
    {
        return 1 - Mathf.Pow(1 - x, 5);
    }
    private float easeInQuint(float x)
    {
        return x * x * x * x * x;
    }
}
