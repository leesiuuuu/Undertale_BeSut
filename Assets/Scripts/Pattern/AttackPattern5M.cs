using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPattern5M : MonoBehaviour
{
    [SerializeField]
    private float X;
    [SerializeField]
    private Vector3 XRangePos;
    private float Y = -2.171f;

    public GameObject Warning;
    public GameObject AtkObj;

    public AudioClip WarningS;
    public UICode UC;

    public float MAX;
    public float MIN;
    private void OnEnable()
    {
        MAX = 1f;
        MIN = 0.2f;
        StartCoroutine(CreateDamager());
    }
    IEnumerator CreateDamager()
    {
        int RandomNum = Random.Range(15, 30);
        for (int i = 0; i < RandomNum; i++)
        {
            float RandomX = Random.Range((X / 2) * -1, X / 2);
            //위험 오브젝트 나타나게 하는 코드 추가
            Vector2 Pos = transform.position + new Vector3(RandomX, Y+ 1.47f, 0);
            GameObject clone = Instantiate(Warning, Pos, Quaternion.identity);
            clone.GetComponent<AttackPattern5_2>().Nigg(RandomX, AtkObj, WarningS);
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        }
        this.enabled = false;
        yield return new WaitForSeconds(1);
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(XRangePos, new Vector2(X, 0.1f));
    }
}
