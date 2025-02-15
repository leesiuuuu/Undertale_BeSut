using System.Collections;
using TMPro;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager instance;
    public int PatternCount = 1;
    public int PatternCountFaze2 = -1;
    public int Before = 0;
    [Header("Faze1 AtkPtn Attributes")]
    public AttackPattern2M AtkPtn2M;
    public AttackPattern3M AtkPtn3M;
    public AttackPattern1M AtkPtn1M;
    public AttackPattern4M AtkPtn4M;
    public AttackPattern5M AtkPtn5M;
    [Space(20)]
    [Header("Faze 2 AtkPtn Attributes")]
    public AttackPatternA1M AtkPtnA1M;
    public AttackPatternA2M AtkPtnA2M;
    public AttackPatternA3M AtkPtnA3M;
    public AttackPatternA4M AtkPtnA4M;
    public AttackPatternA5M AtkPtnA5M;
    public AttackPatternA6M AtkPtnA6M;
    public AttackPatternA7M AtkPtnA7M;
    public AttackPatternA8M AtkPtnA8M;
    public AttackPatternA9M AtkPtnA9M;
    public AttackPatternA10M AtkPtnA10M;
    public AttackPatternA11M AtkPtnA11M;
    public AttackPatternA12M AtkPtnA12M;
    public AttackPatternA13M AtkPtnA13M;
    public AttackPatternA14M AtkPtnA14M;
    public AttackPatternA15M AtkPtnA15M;

    [Header("FadeInAnimation")]
    public GameObject WhiteFade;
    //public AudioClip WhiteSound;
    private float TimeElapsed = 0f;

    public int RandomNum = -1;
    [HideInInspector]
    public bool isSpeicalMove = false;

    private int beforePatter = 1;

    public GameObject Arrow;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Arrow.SetActive(false);
    }   
    public IEnumerator SeqPatternStart(int TurnCount)
    {
        yield return new WaitForSeconds(0.5f);
        if (!StateManager.instance.Faze2)
        {
            switch (TurnCount)
            {
                case 1:
                    AtkPtn2M.enabled = true;
                    break;
                case 2:
                    AtkPtn3M.enabled = true;
                    break;
                case 3:
                    AtkPtn1M.enabled = true;
                    break;
                case 4:
                    AtkPtn4M.enabled = true;
                    break;
                case 5:
                    AtkPtn5M.enabled = true;
                    break;
                default:
                    int randomPattern = beforeCheck(beforePatter);
                    beforePatter = randomPattern;
                    switch (randomPattern)
                    {
                        case 1:
                            AtkPtn1M.enabled = true;
                            AtkPtn1M.Duration = Random.Range(AtkPtn1M.Duration - 2, AtkPtn1M.Duration + 5);
                            AtkPtn1M.MAX = Random.Range(AtkPtn1M.MIN, AtkPtn1M.MAX - 0.3f);
                            break;
                        case 2:
                            AtkPtn2M.enabled = true;
                            break;
                        case 3:
                            AtkPtn3M.enabled = true;
                            break;
                        case 4:
                            AtkPtn4M.enabled = true;
                            AtkPtn4M.RepeatCount = Random.Range(2, 7);
                            break;
                        case 5:
                            AtkPtn5M.enabled = true;
                            AtkPtn5M.MAX = Random.Range(AtkPtn5M.MIN + 0.1f, AtkPtn5M.MAX - 0.5f);
                            break;
                    }
                    break;
            }
            PatternCount++;
            yield return null;
        }
        yield break;
    }
    public IEnumerator SeqPatternStart2(int TurnCount)
    {
        yield return new WaitForSeconds(0.5f);
        if(TurnCount == Before && TurnCount != 0)
        {
            int TC = TurnCount;
            if (TurnCount == -1) TC = 0;
            RandomNum = Random.Range(0, TC + 1);
            switch (RandomNum)
            {
                case 0:
                    AtkPtnA1M.enabled = true;
                    AtkPtnA1M.LoopTime1 = Random.Range(6, 10);
                    AtkPtnA1M.SpawnDelay = Random.Range(0.1f, 0.25f);
                    break;
                case 1:
                    AtkPtnA2M.enabled = true;
                    break;
                case 2:
                    AtkPtnA3M.enabled = true;
                    break;
                case 3:
                    isSpeicalMove = true;
                    AtkPtnA4M.enabled = true;
                    break;
                case 4:
                    AtkPtnA5M.enabled = true;
                    break;
                case 5:
                    AtkPtnA6M.enabled = true;
                    break;
                case 6:
                    isSpeicalMove = true;
                    AtkPtnA7M.enabled = true;
                    break;
                case 7:
                    AtkPtnA8M.enabled = true;
                    break;
                case 8:
                    isSpeicalMove = true;
                    AtkPtnA9M.enabled = true;
                    break;
            }
        }
        else
        {
            switch (TurnCount)
            {
                case 0:
                    AtkPtnA1M.enabled = true;
                    AtkPtnA1M.LoopTime1 = Random.Range(6, 10);
                    AtkPtnA1M.SpawnDelay = Random.Range(0.1f, 0.25f);
                    break;
                case 1:
                    AtkPtnA2M.enabled = true;
                    break;
                case 2:
                    AtkPtnA3M.enabled = true;
                    break;
                case 3:
                    isSpeicalMove = true;
                    AtkPtnA4M.enabled = true;
                    break;
                case 4:
                    AtkPtnA5M.enabled = true;
                    break;
                case 5:
                    AtkPtnA6M.enabled = true;
                    break;
                case 6:
                    isSpeicalMove = true;
                    AtkPtnA7M.enabled = true;
                    break;
                case 7:
                    AtkPtnA8M.enabled = true;
                    break;
                case 8:
                    isSpeicalMove = true;
                    AtkPtnA9M.enabled = true;
                    break;
                case 9:
                    AtkPtnA10M.enabled = true;
                    break;
                case 10:
                    AtkPtnA11M.enabled = true;
                    break;
                case 11:
                    isSpeicalMove = true;
                    AtkPtnA12M.enabled = true;
                    break;
                case 12:
                    isSpeicalMove = true;
                    AtkPtnA13M.enabled = true;
                    break;
                case 13:
                    AtkPtnA14M.enabled = true;
                    break;
                case 14:
                    AtkPtnA15M.enabled = true;
                    break;
            }
            Before = PatternCountFaze2;
        }
        yield break;
    }
    private int beforeCheck(int before)
    {
        int randomValue;
        do
        {
            randomValue = Random.Range(1, 6);
        } while (randomValue == before);

        return randomValue;
    }
    //씬 전환을 위한 하얀 배경 페이드인 애니메이션 재생
    public IEnumerator Faze2PatternChange(float StartDuration, float EndDuration)
    {
        Color startColor = new Color(1, 1, 1, 0);  // 초기 색상 (투명)
        Color endColor = new Color(1, 1, 1, 1);    // 최종 색상 (불투명)
        TimeElapsed = 0;

        //SoundManager.instance.SFXPlay("FazeToStarting", WhiteSound);
        while (TimeElapsed < StartDuration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / StartDuration;
            t = Linear(t);  // Linear 보간 적용
            WhiteFade.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, t);

            yield return null; // 매 프레임 대기
        }

        // 최종 색상 설정 (안정적으로 불투명한 상태 유지)
        WhiteFade.GetComponent<SpriteRenderer>().color = endColor;
        BossManager.instance.Faze2Change();
        yield return new WaitForSeconds(0.5f);
        TimeElapsed = 0;
        while (TimeElapsed < EndDuration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / EndDuration;
            t = Linear(t);  // Linear 보간 적용
            WhiteFade.GetComponent<SpriteRenderer>().color = Color.Lerp(endColor, startColor, t);

            yield return null; // 매 프레임 대기
        }
        StateManager.instance.TurnCount = 0;
        PatternCount = 0;
        yield break;
    }
    private float Linear(float x)
    {
        return x;
    }
}
