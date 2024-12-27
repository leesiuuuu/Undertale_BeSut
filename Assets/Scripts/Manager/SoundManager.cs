using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioMixer audioMixer;
    public AudioClip BG_1;
    public AudioClip BG_2;
    public AudioClip StartMenu;
    public AudioClip DeathMenu;

    [HideInInspector]
    public AudioSource b1;
    [HideInInspector]
    public AudioSource b2;
    [HideInInspector]
    public AudioSource sm;
    [HideInInspector]
    public AudioSource dm;
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
    public void BGPlay()
    {
        GameObject S1 = new GameObject("BG1");
        b1 = S1.AddComponent<AudioSource>();
        b1.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGSound")[0];
        b1.clip = BG_1;
        b1.loop = true;
        b1.volume = 1f;
        b1.Play();
    }
    public void BG2Play()
    {
        GameObject S2 = new GameObject("BG2");
        b2 = S2.AddComponent<AudioSource>();
        b2.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGSound")[0];
        b2.clip = BG_2;
        b2.loop = true;
        b2.volume = 1f;
        b2.Play();
    }
    public void StartMenuPlay()
    {
        GameObject S1 = new GameObject("StartMenu");
        DontDestroyOnLoad(S1);
        sm = S1.AddComponent<AudioSource>();
        sm.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGSound")[0];
        sm.clip = StartMenu;
        sm.loop = true;
        sm.volume = 1f;
        sm.Play();
    }
    public void DeathMenuPlay()
    {
        GameObject S1 = new GameObject("DeathMenu");
        dm = S1.AddComponent<AudioSource>();
        dm.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGSound")[0];
        dm.clip = DeathMenu;
        dm.loop = true;
        dm.volume = 1f;
        dm.Play();
    }
    public void Volume(float val)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(val) * 20);
    }
    public void SFXVolume(float val)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(val) * 20);
    }
    public void BGMVolume(float val)
    {
        audioMixer.SetFloat("BGSoundVolume", Mathf.Log10(val) * 20);
    }
    public void StopBG()
    {
        b1.Stop();
    }
    public void StopBG2()
    {
        b2.Stop();
    }
    public void StopStartMenu()
    {
        sm.Stop();
    }
    public void StopDeathMenu()
    {
        dm.Stop();
    }
    public IEnumerator SoundFadeOut(AudioSource AS, float Duration)
    {
        float ElapsedTime = 0f;
        while(ElapsedTime < Duration)
        {
            ElapsedTime += Time.deltaTime;
            float t = ElapsedTime / Duration;
            AS.volume = 1-t;
            yield return null;
        }
        AS.Stop();
        yield break;
    }
}
