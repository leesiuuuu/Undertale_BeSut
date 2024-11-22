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

    public GameObject Warning;
    public Sprite _Warning;
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
        float i = 1.5f;
        //5���ϰ� ����
        for (int n = 0; n < RandomNum; n++)
        {
            float RandomX = Random.Range((X / 2) * -1, X / 2);
            //���� ������Ʈ ��Ÿ���� �ϴ� �ڵ� �߰�
            Vector2 Pos = transform.position + new Vector3(RandomX, Y + 1.47f, 0);
            GameObject clone = CreateWarningBlink();
            clone.transform.position = Pos;
            StartCoroutine(Blink(WarningS, clone, true));
            yield return new WaitForSeconds(i);
            if (n % 2 != 0) i /= 1.2f;
        }
        yield return new WaitForSeconds(1.2f);

        GameObject Clone = new GameObject("WarningSprite");
        SpriteRenderer SR = Clone.AddComponent<SpriteRenderer>();
        SR.sprite = _Warning;
        Clone.transform.position = new Vector3(0, -1.32f, 0);
        Clone.transform.localScale = new Vector3(1.85f, 1.85f, 1.85f);
        //��� �˸� �ڵ� ����


        //�� ������� ���� ����
        for (int m = 0; m < 4; m++)
        {
            
        }
        StateManager.instance.Fighting = false;
        //C.MyTurnBack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(XRangePos, new Vector2(X, 0.1f));
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
        for (int j = 0; j < 6; j++)
        {
            clone.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.08f);
            clone.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.08f);
            //SoundManager.instance.SFXPlay("Warning", SF);
        }
        if (isSpawn)
        {
            Destroy(clone);
            GameObject Clone = Instantiate(AtkObj, clone.transform.position, Quaternion.identity);
        }
        else
        {
            Destroy(clone);
        }
    }
}
