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

    public bool shakeing;

    public TMP_Text DamageText;
    public Image BossUI;
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
}
