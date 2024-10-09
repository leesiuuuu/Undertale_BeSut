using Unity.VisualScripting;
using UnityEngine;

public class AttackPattern2M : MonoBehaviour
{
    public GameObject AttackObject;
    public UICode UC;

    protected int CopyedCount = 0;
    private int MAX_COPY_COUNT;

    private int LastRotate;

    protected Vector3 leftPoint = new Vector3(9.4f, -2.99f, 0f);
    protected Vector3 rightPoint = new Vector3(-9.17f, 0.5f, 0f);
    protected Vector3 downPoint = new Vector3(1.8f, 6f, 0f);
    protected Vector3 upPoint = new Vector3(-1.71f, -6.21f, 0f);

    private GameObject Arrow;


    private void OnEnable()
    {
        Arrow = PatternManager.instance.Arrow;
        Arrow.SetActive(true);
        MAX_COPY_COUNT = (int)Random.Range(6, 10);
        CopyedCount = 0;
        Debug.Log(CopyedCount);
    }
    protected void Posinit(GameObject go, int Num)
    {
        switch (Num)
        {
            case 0: go.transform.position = leftPoint; break;
            case 1: go.transform.position = rightPoint; break;
            case 2: go.transform.position = upPoint; break;
            case 3: go.transform.position = downPoint; break;
        }
    }
    protected void RotateInit()
    {
        switch (LastRotate)
        {
            case 0:
                Arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                Arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 2:
                Arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 3:
                Arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }
    private void Update()
    {
        if (!GameObject.FindWithTag("AttackSprite") && CopyedCount <= MAX_COPY_COUNT)
        {
            ++CopyedCount;
            if (CopyedCount > MAX_COPY_COUNT)
            {
                StateManager.instance.Fighting = false;
                Arrow.SetActive(false);
                UC.MyTurnBack();
            }
            else
            {
                GameObject clone = Instantiate(AttackObject);
            }
        }
    }
    public void ReturnRandomValue(int value)
    {
        LastRotate = value;
    }
}
