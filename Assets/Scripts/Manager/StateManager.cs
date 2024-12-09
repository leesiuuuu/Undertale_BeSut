using UnityEditor.PackageManager;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public bool Fighting = false;
    public bool Acting = false;
    public bool Starting = false;
    public bool logAppear = false;
    [Header("ActState")]
    public bool _Fighting = false;
    public bool _Acting = false;
    public bool _Iteming = false;
    public bool _Mercying = false;
    [Header("BossState")]
    public bool Talking = false;
    public int NoLieStack = 0;
    public int TurnCount = 0;
    //완전한 불살 루트를 거쳤을 때 나타남.
    //그러나, 공격을 할 시 2페이즈 켜짐
    public bool NoKill = false;
    public bool Faze2 = false;
    [Space(20)]
    //2페이즈 넘어가는 루트
    public bool NormalFaze2 = false;
    public bool BetrayalFaze2 = false;
    [Space(20)]
    public bool GameDone = false;
    public bool _10Ptn = false;
    void Awake()
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
    }
    public string DialogueChanger(int TurnCount, string Dialogue)
    {
        if (!Faze2 && !NoKill)
        {
            switch (TurnCount)
            {
                case 1:
                    Dialogue = "* 당신은 어딘가 싸한 기분을 느꼈다.";
                    break;
                case 2:
                    Dialogue = "* 당신은 고양이털이 날리는게 싫었다.";
                    break;
                case 3:
                    Dialogue = "* 당신은 머리가 아프다는 걸 느꼈다.";
                    break;
                case 4:
                    Dialogue = "* 당신은 왜 자신이 여기에 있는지 생각했다.";
                    break;
                case 5:
                    Dialogue = "* 당신은 자신의 몸에 핏자국이 있다는걸 \n  보았다.";
                    break;
                case 6:
                    Dialogue = "* 당신은 앞에 있는 고양이를 쓰다듬고 싶다는\n  생각을 했다.";
                    break;
                case 7:
                    Dialogue = "* 당신은 더 이상 길게 생각하지 않기로 했다.";
                    break;
                default:
                    Dialogue = "* 당신은 자신의 의지에 따른 선택을\n  하기로 했다.";
                    break;
            }
        }
        else if (NoKill)
        {
            Dialogue = "* 네이트 코릴은 당신에게 자비를 배풀었다!";
        }
        else
        {
            if (NormalFaze2)
            {
                switch (TurnCount)
                {
                    case 1:
                        Dialogue = "* 네이트 코릴은 당신을 죽이고 싶어 하는\n  것처럼 보인다."; break;
                    case 2:
                        Dialogue = "* 당신은 싸우기 싫었다."; break;
                    case 3:
                        Dialogue = "* 당신은 잘못된 선택을 한 것 같았다."; break;
                    default:
                        Dialogue = "* 당신은 빠르게 끝내고 싶다고 생각했다."; break;
                }
            }
            else
            {
                Dialogue = "* 거짓에는 항상 책임이 따른다.";
            }
        }
        return Dialogue;
    }

    public void SaveBetrayalFaze2()
    {
        PlayerPrefs.SetInt("BetrayalFaze2", BetrayalFaze2 ? 1 : 0);
    }
    public void DeleteBetrayalFaze2()
    {
        try
        {
            PlayerPrefs.DeleteKey("BetrayalFaze2");
        } catch (System.Exception e)
        {
            Debug.Log("Error Appeared! : " + e);
            return;
        }
    }
}
