using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioMixer audioMixer;
    public AudioSource BGSound;
    public AudioSource BG2Sound;
    public AudioSource StartMenu;
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
    public void BG2Play()
    {
        BG2Sound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGSound")[0];
        BG2Sound.loop = true;
        BG2Sound.volume = 1f;
        BG2Sound.Play();
    }
    public void StartMenuPlay()
    {
        StartMenu.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGSound")[0];
        StartMenu.loop = true;
        StartMenu.volume = 1f;
        StartMenu.Play();
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
        BGSound.Stop();
    }
    public void StopBG2()
    {
        BG2Sound.Stop();
    }
    public void StopStartMenu()
    {
        StartMenu.Stop();
    }
}
