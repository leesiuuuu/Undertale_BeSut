using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoMain : MonoBehaviour
{
    [SerializeField]
    private GameObject BlackFade;
    public IEnumerator GoMainScene()
    {
        Transition1 t1 = BlackFade.AddComponent<Transition1>();
        t1.Duration = 1f;
        t1.FadeIn = true;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("StartScene");
    }
}
