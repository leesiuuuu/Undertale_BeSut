using UnityEngine;

public class AttackPattern2M : MonoBehaviour
{
    public GameObject AttackObject;

    protected int CopyedCount = 0;

    protected Vector3 leftPoint = new Vector3(9.4f, -2.99f, 0f);
    protected Vector3 rightPoint = new Vector3(-9.17f, 0.5f, 0f);
    protected Vector3 downPoint = new Vector3(1.8f, 6f, 0f);
    protected Vector3 upPoint = new Vector3(-1.71f, -6.21f, 0f);
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
    private void Update()
    {
        if (!GameObject.FindWithTag("AttackSprite") && CopyedCount != 10)
        {
            GameObject clone = Instantiate(AttackObject);
        }
    }
}
