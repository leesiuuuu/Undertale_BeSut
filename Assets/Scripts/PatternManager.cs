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
                //번째 패턴 코드 활성화
                break;
            /// ......
        }
        PatternCount++;
        yield return null;
    }
    public async Task SeqPatternStart1(int TurnCount)
    {

    }
}
