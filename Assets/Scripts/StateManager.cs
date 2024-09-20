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
}
