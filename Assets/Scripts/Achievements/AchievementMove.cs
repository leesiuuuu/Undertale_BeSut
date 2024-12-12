using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class AchievementMove : MonoBehaviour
{
    private AchievementUI[] AchiUIs;
    int index = 0;
    int before = 0;
    int MAX_INDEX_NUM = 10;
    void Start()
    {
        AchiUIs = GetComponentsInChildren<AchievementUI>();
    }

    float y = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            y = Mathf.Clamp(y+=3f, 0, 27f);
            index = Mathf.Clamp(++index, 0, MAX_INDEX_NUM);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            y = Mathf.Clamp(y-=3f, 0, 27f);
            index = Mathf.Clamp(--index, 0, MAX_INDEX_NUM); 
        }
        UpdateState();
        before = index;
    }
    void UpdateState()
    {
        if(before == index)
        {
            return;
        }
        else
        {
            StartCoroutine(SmoothMove(y));
            AchiUIs[before].StateDeSelete();
            AchiUIs[index].StateSelete();
            return;
        }
    }
    IEnumerator SmoothMove(float y)
    {
        float elapsedTime = 0f;
        float duration = 0.2f;
        Vector3 a = transform.position;
        Vector3 b = new Vector3(
            transform.position.x,
            y, 0);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = easeOutSine(t);
            transform.position = Vector3.Lerp(a, b, t);
            yield return null;
        }
        yield break;
    }
    float easeOutSine(float t)
    {
        return Mathf.Sin((t * Mathf.PI)/ 2);
    }
}
