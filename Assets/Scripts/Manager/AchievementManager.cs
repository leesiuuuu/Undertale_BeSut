using System.Collections;
using UnityEngine;

public class AchievementState
{
    public string key;
    public int isClear;
    public AchievementState(string key, int isClear)
    {
        this.key = key;
        this.isClear = isClear;
    }
}

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    private AchievementState[] achievementStates = new AchievementState[11];

    public AchievementSO[] achievementSOs;

    public GameObject UIClear;
    public GameObject UICanvas;

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(instance);
    }
    //StartScene���� ȣ���� �޼���
    public void InitAchi()
    {
        if (PlayerPrefs.GetInt("InitState") == 1 && PlayerPrefs.HasKey("InitState")) return;
        //PlayerPrefs ���� �� �ʱ�ȭ
        for(int i = 0; i < achievementSOs.Length; i++)
        {
            achievementStates[i] = new AchievementState(achievementSOs[i].key, 
                PlayerPrefs.HasKey(achievementSOs[i].key) ? PlayerPrefs.GetInt(achievementSOs[i].key) : 0);
            PlayerPrefs.SetInt(achievementStates[i].key, achievementStates[i].isClear);
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetInt("InitState", 1);
        PlayerPrefs.Save();
    }
    public IEnumerator AchiUIAppearence(int n)
    {
        if (PlayerPrefs.GetInt(achievementSOs[n].key) == 1) yield break;
        GameObject Clone = Instantiate(UIClear);
        GameObject canvas = Instantiate(UICanvas);
        DontDestroyOnLoad(canvas);
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        Clone.GetComponent<UIAppear>().ASO = achievementSOs[n];
        Clone.transform.SetParent(canvas.transform, false);
        yield return new WaitForSeconds(9);
        Destroy(canvas);
        yield break;
    }
    //������ �޼����� �� ������ ��������.
    //Ŭ��� 1�̸� ���� Ŭ����, �׷��� ������ ���� Ŭ���� X
    public void SaveAchi(string key, int clear)
    {
        if(PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, clear);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log($"Key {key} is not inited!");
        }
    }
    public AchievementState FindAchiWithKey(string key)
    {
        foreach(AchievementState state in achievementStates)
        {
            if (state.key == key) return state;
        }
        return null;
    }
}
