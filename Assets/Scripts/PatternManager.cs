using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public static PatternManager instance;
    public int PatternCount;
    public AttackPattern2 AtkPtn2;
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
    }
    public void SeqPatternStart(int TurnCount)
    {
        switch (TurnCount)
        {
            case 0:
                AtkPtn2.enabled = true;
                break;
            case 1:
                break;
        }
    }
}
