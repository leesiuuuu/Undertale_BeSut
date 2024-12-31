using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    [SerializeField]
    private GameObject blackfade;
    public IEnumerator MainSceneMove()
    {
        Transition1 t1 = blackfade.AddComponent<Transition1>();
        t1.FadeIn = true;
        t1.Duration = 1.5f;
        StartCoroutine(SoundManager.instance.SoundFadeOut(SoundManager.instance.dm, 1.5f));
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.StopDeathMenu();
        SceneManager.LoadScene("StartScene");
        yield break;
    }
    public IEnumerator RestartGame()
    {
        Transition1 t1 = blackfade.AddComponent<Transition1>();
        t1.FadeIn = true;
        t1.Duration = 1.5f;
        StartCoroutine(SoundManager.instance.SoundFadeOut(SoundManager.instance.dm, 1.5f));
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.StopDeathMenu();
        SceneManager.LoadScene("SampleScene");
    }
}
