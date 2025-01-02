using System.Collections;
using UnityEngine;
using TMPro;

public class TalkBox_Log : MonoBehaviour
{
    private string dialogue;
    public float talkWaitTime;
    public float Delay;
    public TMP_Text TMPtext;
    public AudioClip Log;
    public string[] logs;
    public int n = 0;
    void Start()
    {
        Talk(Delay, logs[n]);
    }
    public void Talk(float DelayTime, string dialogue1)
    {
        StartCoroutine(TalkStart(dialogue1, talkWaitTime, DelayTime));
    }
    IEnumerator TalkStart(string dialogue, float talkWaitTime, float Delay = 0f)
    {
        yield return new WaitForSeconds(Delay);
        TMPtext.text = null;
        for(int i = 0; i < dialogue.Length; i++)
        {
            TMPtext.text += dialogue[i];
            SoundManager.instance.SFXPlay("Log", Log);
            yield return new WaitForSeconds(talkWaitTime);
        }
    }
}
