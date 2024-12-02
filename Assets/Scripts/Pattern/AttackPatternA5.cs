using System.Collections;
using UnityEngine;

public class AttackPatternA5 : MonoBehaviour
{
    public float Rotate;

    private bool isUp = false;
    private void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        //기본 값
        //2, 1
        //빌드 기본값
        //0.8, 1
        StartCoroutine(Shake(gameObject, 0.8f, 1));
        Quaternion RotateSum = Quaternion.Euler(0, 0, Rotate);
        gameObject.transform.rotation = RotateSum;
    }
    private void Update()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        if (!isUp)
        {
            StartCoroutine(ScaleUp(gameObject, 1, 1));
        }
        else
        {
            StartCoroutine(ScaleDown(gameObject, 1, 0.5f));
        }
    }
    IEnumerator Shake(GameObject obj, float Duration = 1f, float Power = 1f)
    {
        Vector3 origin = obj.transform.position;
        while (Duration > 0f)
        {
            Duration -= 0.05f;
            obj.transform.position = origin + (Vector3)Random.insideUnitCircle * Power * Duration;
            yield return null;
        }
        obj.transform.position = origin;
    }
    IEnumerator ScaleUp(GameObject obj, float Duration, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        Vector3 EndVector = new Vector3(25.1195f, 0.6f, 1);
        while (Duration > 0f)
        {
            Duration -= 0.05f;
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, EndVector, Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.7f);
        isUp = true;
    }
    IEnumerator ScaleDown(GameObject obj, float Duration, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        Vector3 EndVector = new Vector3(25.1195f, 0, 1);
        while (Duration > 0f)
        {
            Duration -= 0.05f;
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, EndVector, Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
        isUp = false;
    }
}
