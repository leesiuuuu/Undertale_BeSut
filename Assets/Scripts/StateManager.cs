using System.Diagnostics.Tracing;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    public bool Fighting = false;
    public bool Acting = false;
    public bool Starting = false;
    [Header("ActState")]
    public bool _Fighting = false;
    public bool _Acting = false;
    public bool _Iteming = false;
    public bool _Mercying = false;
    [Header("BossState")]
    public bool Talking = false;
    public int NoLieStack = 0;
    public int TurnCount = 0;
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
        switch (TurnCount)
        {
            case 1:
                Dialogue = "* ����� ��� ���� ����� ������.";
                break;
            case 2:
                Dialogue = "* ����� ���������� �����°� �Ⱦ���.";
                break;
            case 3:
                Dialogue = "* ����� �Ӹ��� �����ٴ� �� ������.";
                break;
            case 4:
                Dialogue = "* ����� �� �ڽ��� ���⿡ �ִ��� �����ߴ�.";
                break;
            case 5:
                Dialogue = "* ����� �ڽ��� ���� ���ڱ��� �ִٴ°� ���Ҵ�.";
                break;
            case 6:
                Dialogue = "* ����� �տ� �ִ� �����̸� ���ٵ�� �ʹٴ� ������ �ߴ�.";
                break;
            case 7:
                Dialogue = "* ����� �� �̻� ��� �������� �ʱ�� �ߴ�.";
                break;
        }
        return Dialogue;
    }
}