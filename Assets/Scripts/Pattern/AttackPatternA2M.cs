using System.Collections;
using System;
using UnityEngine;

public class AttackPatternA2M : MonoBehaviour
{
    [SerializeField]
    private float X;
    [SerializeField]
    private Vector3 XRangePos;
    private float Y = -2.171f;

    public GameObject Warning;
    public GameObject AtkObj;

    public AudioClip WarningS;

    public GameObject PrefapLaser;

    private GameObject Clone;

    private GameObject Laser;

    public float MAX;
    public float MIN;
    private void OnEnable()
    {
        Instantiate(PrefapLaser);
        try
        {
            Laser = GameObject.Find("Laser_1");
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
        MAX = 1f;
        MIN = 0.2f;
        StartCoroutine(CreateDamager());
    }
    IEnumerator CreateDamager()
    {
        int RandomNum = UnityEngine.Random.Range(15, 30);
        for (int i = 0; i < RandomNum; i++)
        {
            float RandomX = UnityEngine.Random.Range((X / 2) * -1, X / 2);
            //위험 오브젝트 나타나게 하는 코드 추가
            Vector2 Pos = transform.position + new Vector3(RandomX, Y + 1.47f, 0);
            GameObject clone = Instantiate(Warning, Pos, Quaternion.identity);
            clone.GetComponent<AttackPattern5_2>().Nigg(RandomX, AtkObj, WarningS);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 1f));
        }
        yield return new WaitForSeconds(1);
        Laser.GetComponent<Laser_Ex>().isLaserExpanding = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(XRangePos, new Vector2(X, 0.1f));
    }
}
