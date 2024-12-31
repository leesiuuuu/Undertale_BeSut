using UnityEngine;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer VP;

    private void Start()
    {
        VP.SetDirectAudioVolume(0, PlayerPrefs.HasKey("vol") ? PlayerPrefs.GetFloat("vol") : 1f);
        VP.loopPointReached += OnVideoEnd;
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        Destroy(gameObject);
        SoundManager.instance.DeathMenuPlay();
    }
}
