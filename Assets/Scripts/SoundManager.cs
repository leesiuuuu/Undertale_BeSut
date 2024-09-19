using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioMixer audioMixer;
    public AudioSource BGSound;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(go, clip.length);
    }
    public void BGPlay(AudioClip clip)
    {
        BGSound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGSound")[0];
        BGSound.clip = clip;
        BGSound.loop = true;
        BGSound.volume = 0.1f;
        BGSound.Play();
    }
    public void Volume(float val)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(val) * 20);
    }
}
