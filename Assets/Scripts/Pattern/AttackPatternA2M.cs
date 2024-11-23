using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackPatternA2M : MonoBehaviour
{
    [SerializeField]
    private float X;
    [SerializeField]
    private Vector3 XRangePos;
    private float Y = -2.171f;

    [SerializeField]
    private Vector3 XRangePos1;
    [SerializeField] private float X1;

    public GameObject Warning;
    public Sprite _Warning;
    public GameObject AtkObj;
    public GameObject SafeZoneEffect;

    public AudioClip WarningS;
    public AudioClip OutSound;
    public UICode UC;

    public float MAX;
    public float MIN;

    float[] PosArray = new float[8];
    private void OnEnable()
    {
        for(int i = 0; i < PosArray.Length; i++)
        {
            PosArray[i] = -1.46f + 0.4f * i;
        }
        MAX = 1f;
        MIN = 0.2f;
        StartCoroutine(CreateDamager());
    }
    IEnumerator CreateDamager()
    {
        int RandomNum = Random.Range(15, 25);
        float i = 1.5f;
        //5패턴과 같음
        for (int n = 0; n < RandomNum; n++)
        {
            float RandomX = Random.Range((X / 2) * -1, X / 2);
            //위험 오브젝트 나타나게 하는 코드 추가
            Vector2 Pos = transform.position + new Vector3(RandomX, Y + 1.47f, 0);
            GameObject clone = CreateWarningBlink();
            clone.transform.position = Pos;
            StartCoroutine(Blink(WarningS, clone, true));
            yield return new WaitForSeconds(i);
            if (n % 2 != 0) i /= 1.2f;
        }
        yield return new WaitForSeconds(2.5f);

        GameObject Clone = new GameObject("WarningSprite");
        SpriteRenderer SR = Clone.AddComponent<SpriteRenderer>();
        SR.sprite = _Warning;
        Clone.transform.position = new Vector3(0, -1.32f, 0);
        Clone.transform.localScale = Vector3.one * 0.2f;
        //경고 알림 코드 삽입
        for(int _i = 1; _i <= 8; _i++)
        {
            SR.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(0.1f);
            SR.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.1f);
            Clone.transform.localScale = Vector3.one * _i / 2;
            //SoundManager.instance.SFXPlay("Warning!!", WarningS);
        }

        //더 어려워진 패턴 등장
        int HardPatternCount = Random.Range(4, 7);
        for (int m = 0; m < HardPatternCount; m++)
        {
            int ExPos = Random.Range(0, 8);
            GameObject Effect = Instantiate(SafeZoneEffect, new Vector3(PosArray[ExPos], XRangePos1.y), Quaternion.identity);
            Destroy(Effect, 0.2f);
            yield return new WaitForSeconds(1f);
            for(int __i = 0;  __i < 8; __i++)
            {
                Vector3 Pos = new Vector3(PosArray[__i], -0.7f);
                if (__i == ExPos)
                {
                    SoundManager.instance.SFXPlay("Up!", OutSound);
                    continue;
                }
                else Instantiate(AtkObj, Pos, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.75f);
        }
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(XRangePos, new Vector2(X, 0.1f));
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(XRangePos1, new Vector2(X1, 0.1f));
    }

    private GameObject CreateWarningBlink()
    {
        GameObject Clone = new GameObject("WarningClone");
        SpriteRenderer SR = Clone.AddComponent<SpriteRenderer>();
        Clone.transform.localScale = new Vector3(1.85f, 1.85f, 1.85f);
        SR.sprite = _Warning;
        return Clone;
    }
    IEnumerator Blink(AudioClip SF, GameObject clone, bool isSpawn = false)
    {
        for (int j = 0; j < 8; j++)
        {
            clone.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.04f);
            clone.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.04f);
            SoundManager.instance.SFXPlay("Warning", SF);
        }
        if (isSpawn)
        {
            Destroy(clone);
            GameObject Clone = Instantiate(AtkObj, clone.transform.position, Quaternion.identity);
            SoundManager.instance.SFXPlay("Up!", OutSound);
        }
        else
        {
            Destroy(clone);
        }
    }
}
