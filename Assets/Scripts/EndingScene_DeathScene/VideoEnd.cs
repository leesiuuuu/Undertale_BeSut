using UnityEngine;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer VP;

    private void Start()
    {
        VP.loopPointReached += OnVideoEnd;
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        Destroy(gameObject);
        SoundManager.instance.DeathMenuPlay();
    }
}