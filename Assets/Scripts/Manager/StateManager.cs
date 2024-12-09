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
    //������ �һ� ��Ʈ�� ������ �� ��Ÿ��.
    //�׷���, ������ �� �� 2������ ����
    public bool NoKill = false;
    public bool Faze2 = false;
    [Space(20)]
    //2������ �Ѿ�� ��Ʈ
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
                    Dialogue = "* ����� ��� ���� ����� ������.";
                    break;
                case 2:
                    Dialogue = "* ����� ��������� �����°� �Ⱦ���.";
                    break;
                case 3:
                    Dialogue = "* ����� �Ӹ��� �����ٴ� �� ������.";
                    break;
                case 4:
                    Dialogue = "* ����� �� �ڽ��� ���⿡ �ִ��� �����ߴ�.";
                    break;
                case 5:
                    Dialogue = "* ����� �ڽ��� ���� ���ڱ��� �ִٴ°� \n  ���Ҵ�.";
                    break;
                case 6:
                    Dialogue = "* ����� �տ� �ִ� ����̸� ���ٵ�� �ʹٴ�\n  ������ �ߴ�.";
                    break;
                case 7:
                    Dialogue = "* ����� �� �̻� ��� �������� �ʱ�� �ߴ�.";
                    break;
                default:
                    Dialogue = "* ����� �ڽ��� ������ ���� ������\n  �ϱ�� �ߴ�.";
                    break;
            }
        }
        else if (NoKill)
        {
            Dialogue = "* ����Ʈ �ڸ��� ��ſ��� �ں� ��Ǯ����!";
        }
        else
        {
            if (NormalFaze2)
            {
                switch (TurnCount)
                {
                    case 1:
                        Dialogue = "* ����Ʈ �ڸ��� ����� ���̰� �;� �ϴ�\n  ��ó�� ���δ�."; break;
                    case 2:
                        Dialogue = "* ����� �ο�� �Ⱦ���."; break;
                    case 3:
                        Dialogue = "* ����� �߸��� ������ �� �� ���Ҵ�."; break;
                    default:
                        Dialogue = "* ����� ������ ������ �ʹٰ� �����ߴ�."; break;
                }
            }
            else
            {
                Dialogue = "* �������� �׻� å���� ������.";
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
