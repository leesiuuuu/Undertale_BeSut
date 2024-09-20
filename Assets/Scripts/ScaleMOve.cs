using UnityEngine;

public class ScaleMOve : MonoBehaviour
{
    public Vector3 StartScale; //���� ��ġ
    public Vector3 EndScale; //���� ��ġ 
    public float Duration; //�Ⱓ

    private float TimeElapsed = 0f; //���� �ð�
    void Start()
    {
        
    }

    void Update()
    {
        if (TimeElapsed < Duration)
        {
            TimeElapsed += Time.deltaTime;
            float t = TimeElapsed / Duration; // 0���� 1������ �ð� ������ ����ϴ� �ڵ�
            t = easeInOutSine(t);
            transform.localScale = Vector3.Lerp(StartScale, EndScale, t); //��ġ�� ���������Լ��� �ڿ������� �̵�
        }
    }
    //�ε巯�� ������ �Լ�
    float easeInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }
}
