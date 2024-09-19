using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossTalkCode : MonoBehaviour
{
    public string dialogue;
    public float talkWaitTime;
    public float Delay;
    public TMP_Text TMPtext;
    //public AudioClip BossTalk;
    public void Talk(float DelayTime, string dialogue1)
    {
        StartCoroutine(TalkStart(dialogue1, talkWaitTime, DelayTime));
    }
    IEnumerator TalkStart(string dialogue, float talkWaitTime, float Delay = 0f)
    {
        yield return new WaitForSeconds(Delay);
        TMPtext.text = null;
        for (int i = 0; i < dialogue.Length; i++)
        {
            TMPtext.text += dialogue[i];
            //SoundManager.instance.SFXPlay("Boss", BossTalk);
            yield return new WaitForSeconds(talkWaitTime);
        }
    }
}
