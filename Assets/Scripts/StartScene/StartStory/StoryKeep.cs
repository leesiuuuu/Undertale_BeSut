using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryKeep : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer SR;
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(SoundManager.instance.SoundFadeOut(SoundManager.instance.sb, 1f));
            StartCoroutine(SceneFadeOut(SR, 1f));
        }
    }
    private IEnumerator SceneFadeOut(SpriteRenderer sr, float Duration)
    {
        float ElapsedTime = 0f;
        while(ElapsedTime < Duration)
        {
            ElapsedTime += Time.deltaTime;
            float t = ElapsedTime / Duration;
            sr.color = Color.Lerp(new Color(0f,0f,0f,0f), new Color(0f,0f,0f,1f), t);
            yield return null;
        }
        SoundManager.instance.StopStoryBGM();
        SceneManager.LoadScene("StartScene");
        yield break;
    }
}
