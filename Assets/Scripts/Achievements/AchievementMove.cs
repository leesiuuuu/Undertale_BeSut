using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AchievementMove : MonoBehaviour
{
    private AchievementUI[] AchiUIs;
    int index = 0;
    int before = 0;
    int MAX_INDEX_NUM = 11;
    bool once = false;
    void Start()
    {
        once = false;
        AchiUIs = new AchievementUI[MAX_INDEX_NUM];
        AchiUIs = GetComponentsInChildren<AchievementUI>();
    }

    float y = 0;
    void Update()
    {
        if (!once)
        {
            AchiUIs[0].StateSelete();
            once = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKeyDown(KeyCode.UpArrow))
        {
            y = Mathf.Clamp(y+=3f, 0, 30f);
            index = Mathf.Clamp(++index, 0, MAX_INDEX_NUM);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKeyDown(KeyCode.DownArrow))
        {
            y = Mathf.Clamp(y-=3f, 0, 30f);
            index = Mathf.Clamp(--index, 0, MAX_INDEX_NUM); 
        }
        else if (Input.GetKeyDown(KeyCode.Z) && index == 11)
        {
            SceneManager.LoadScene("StartScene");
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
