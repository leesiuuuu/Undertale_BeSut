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
    public AchievementState[] achievementStates;

    public AchievementSO[] achievementSOs;

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else Destroy(instance);
    }
    public void InitAchi()
    {
        if (PlayerPrefs.GetInt("InitState", 0) == 1) return;
        //PlayerPrefs 내부 값 초기화
        for(int i = 0; i < achievementStates.Length; i++)
        {
            achievementStates[i] = new AchievementState(achievementSOs[i].key, 
                PlayerPrefs.HasKey(achievementSOs[i].key) ? PlayerPrefs.GetInt(achievementSOs[i].key) : 0);
            PlayerPrefs.SetInt(achievementStates[i].key, achievementStates[i].isClear);
        }
        PlayerPrefs.SetInt("InitState", 1);
        PlayerPrefs.Save();
    }
    //업적을 달성했을 때 업적을 저장해줌.
    //클리어가 1이면 업적 클리어, 그렇지 않으면 업적 클리어 X
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
    //업적 달성 여부를 로드, 만약 값을 찾을 수 없으면 -1을 반환함.
    public int LoadAchi(string Key)
    {
        if (PlayerPrefs.HasKey(Key))
        {
            return PlayerPrefs.GetInt(Key);
        }
        else
        {
            return -1;
        }
    }
    private void DeleteAllAchi()
    {

    }
}
