using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int MAX_HP;
    public int HP;

    public TMP_Text Text;
    public Image UI;

    public bool isDead = false;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        HP = MAX_HP;
    }
    public void HPChanged()
    {
        if(HP > MAX_HP)
        {
            HP = MAX_HP;
        }
        Text.text = "";
        Text.text = HP + " / 92";
        float HPDetail1 = (PlayerManager.instance.HP / (float)PlayerManager.instance.MAX_HP);
        UI.fillAmount = HPDetail1;
    }
}