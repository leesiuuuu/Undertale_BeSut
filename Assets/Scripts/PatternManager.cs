using System.Collections;
using System.Threading.Tasks;
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
        }
        PatternCount++;
        yield return null;
    }
}
