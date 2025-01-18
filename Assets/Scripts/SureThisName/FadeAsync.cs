using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeAsync : MonoBehaviour
{
    private SpriteRenderer White;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public IEnumerator fade(SpriteRenderer White, float Duration, Color none, Color white)
    {
        float ElapsedTime = 0f;
        while (ElapsedTime < Duration)
        {
            ElapsedTime += Time.deltaTime;
            float t = ElapsedTime / Duration;
            White.color = Color.Lerp(none, white, t);
            yield return null;
        }
        SceneManager.LoadScene("SampleScene");
        yield break;
    }
    private IEnumerator fadeout(float Duration, SpriteRenderer White)
    {
        float ElapsedTime = 0f;
        Duration = 1f;
        while (ElapsedTime < Duration)
        {
            ElapsedTime += Time.deltaTime;
            float t = ElapsedTime / Duration;
            White.color = Color.Lerp(new Color(1f,1f,1f,1f), new Color(1f,1f,1f,0f), t);
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("SampleScene"))
        {
            White = GetComponent<SpriteRenderer>();
            StartCoroutine(fadeout(1f, White));
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
