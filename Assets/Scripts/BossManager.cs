using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Data.Common;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public int bossHP;
    public int MAX_BOSS_HP = 1000;

    public TMP_Text DamageText;
    public Image BossUI;
    private void Awake()
    {
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
        DamageText.gameObject.SetActive(true);
        DamageText.text = "";
        DamageText.text = Damage.ToString();
        bossHP -= Damage;
        float HPDetail1 = bossHP / (float)MAX_BOSS_HP;
        BossUI.fillAmount = HPDetail1;
        BossUI.transform.parent.gameObject.SetActive(true);
        Invoke("ReturnBoss", 0.8f);
    }
    void ReturnBoss()
    {
        DamageText.gameObject.SetActive(false);
        BossUI.transform.parent.gameObject.SetActive(false);
    }
}
