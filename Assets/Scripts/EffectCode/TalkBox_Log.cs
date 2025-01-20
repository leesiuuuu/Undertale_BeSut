using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TalkBox_Log : MonoBehaviour
{
    private string dialogue;
    public float talkWaitTime;
    public float Delay;
    public TMP_Text TMPtext;
    public AudioClip Log;
    public string[] logs;

    public Sprite[] images;
    public Image Image;

    void Start()
    {
        SoundManager.instance.StoryBGMPlay();
        Talk(Delay, logs);
    }
    public void Talk(float DelayTime, string[] dialogue1)
    {
        StartCoroutine(TalkStart(talkWaitTime, DelayTime, dialogue1));
    }
    IEnumerator TalkStart(float talkWaitTime, float Delay = 0f, params string[] dialogue)
    {
        for(int j = 0; j < dialogue.Length; j++)
        {
            TMPtext.text = null;
            Image.sprite = images[j];
            for (int i = 0; i < dialogue[j].Length; i++)
            {
                TMPtext.text += dialogue[j][i];
                SoundManager.instance.SFXPlay("Log", Log);
                yield return new WaitForSeconds(talkWaitTime);
            }
            yield return new WaitForSeconds(Delay);
        }
    }
}
