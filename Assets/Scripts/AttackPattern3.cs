using System.Collections;
using UnityEngine;

public class AttackPattern3 : AttackPattern3M
{
    private bool isUp = false;
    private float RandomRotate;
    private void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        //기본 값
        //2, 1
        //빌드 기본값
        //0.5, 1
        StartCoroutine(Shake(gameObject, 2f, 1));
        RandomRotate = Random.Range(0f, 360f);
        Quaternion RandomRotate1 = Quaternion.Euler(0, 0, RandomRotate);
        gameObject.transform.rotation = RandomRotate1;
    }
    private void Update()
    {
        if (isAllCreated)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            if (!isUp)
            {
                StartCoroutine(ScaleUp(gameObject, 1));
            }
            else
            {
                StartCoroutine(ScaleDown(gameObject, 1));
            }
        }
    }
    IEnumerator Shake(GameObject obj, float Duration = 1f, float Power = 1f)
    {
        Vector3 origin = obj.transform.position;
        while(Duration > 0f)
        {
            Duration -= 0.05f;
            obj.transform.position = origin + (Vector3)Random.insideUnitCircle * Power * Duration;
            yield return null;
        }
        obj.transform.position = origin;
    }
    IEnumerator ScaleUp(GameObject obj, float Duration)
    {
        Vector3 EndVector = new Vector3(25.1195f, 0.6f, 1);
        while(Duration > 0f)
        {
            Duration -= 0.05f;
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, EndVector, Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.7f);
        isUp = true;
    }
    IEnumerator ScaleDown(GameObject obj, float Duration)
    {
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
