using System.Collections;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager instance;
    public int PatternCount = 1;
    [Header("AtkPtn2 Attributes")]
    public AttackPattern2M AtkPtn2M;
    public AttackPattern3M AtkPtn3M;
    public AttackPattern1M AtkPtn1M;
    public AttackPattern4M AtkPtn4M;
    public AttackPattern5M AtkPtn5M;

    private int beforePatter = 1;

    public GameObject Arrow;
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
        Arrow.SetActive(false);
    }
    public IEnumerator SeqPatternStart(int TurnCount)
    {
        yield return new WaitForSeconds(0.5f);
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
                        AtkPtn1M.MAX = Random.Range(AtkPtn1M.MIN - 0.2f, AtkPtn1M.MAX - 0.7f);
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
    private int beforeCheck(int before)
    {
        int randomValue;

        do
        {
            randomValue = Random.Range(1, 6);
        } while (randomValue == before);

        return randomValue;
    }

}
