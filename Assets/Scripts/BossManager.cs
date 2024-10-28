using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using System.Collections.Specialized;
using UnityEditor.Timeline;

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

    [Header("Sprites")]
    public Sprite bossHurt;
    public Sprite bossHurtTalk;
    public Sprite bossUlt;
    public Sprite bossHurtStand;

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
        StartCoroutine(Shake(Boss, 4f, 0.2f));
        Invoke("ReturnBoss", 0.8f);
    }
    void ReturnBoss()
    {
        DamageText.gameObject.SetActive(false);
        BossUI.transform.parent.gameObject.SetActive(false);
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
    /// </summary>
    /// <param name="index"></param>
    public void ChangeSprite(int index)
    {
        Debug.Log("asdf");
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
        Boss.GetComponent<SpriteRenderer>().sprite = bossUlt;
    }
    public void BossMoveToLeftOrRight(float Duration)
    {
        //true = left false = right
        bool leftOrRigh = (Random.value > 0.5f);
        Vector2 StartPos = new Vector2(0, 2.56f);
        Vector2 EndPos = new Vector2(leftOrRigh ? -2 : 2, 2.56f);

        float TimeElapsed = 0;

        Miss(TimeElapsed, Duration, StartPos, EndPos);
    }
    private float easeOutQuint(float x)
    {
        return 1 - Mathf.Pow(1 - x, 5);
    }
    private float easeInQuint(float x)
    {
        return x * x * x * x * x;
    }
    private void Miss(float TimeElapsed, float Duration, Vector2 StartPos, Vector2 EndPos)
    {
        while (TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration;
            t = easeOutQuint(t);
            transform.position = Vector2.Lerp(StartPos, EndPos, t);
        }
        TimeElapsed = 0;
        Vector2 temp = EndPos;
        EndPos = StartPos;
        StartPos = temp;
        while(TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed/Duration;
            t = easeInQuint(t);
            transform.position = Vector2.Lerp(EndPos, StartPos, t);
        }
    }
}
