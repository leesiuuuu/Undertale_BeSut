using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkBox : MonoBehaviour
{
    public string dialogue;
    public float talkWaitTime;
    public float Delay;
    public TMP_Text TMPtext;
    void Start()
    {
        StartCoroutine(TalkStart(dialogue, talkWaitTime, Delay));
    }
    public void Talk(float DelayTime)
    {
        StartCoroutine(TalkStart(dialogue, talkWaitTime, DelayTime));
    }
    IEnumerator TalkStart(string dialogue, float talkWaitTime, float Delay = 0f)
    {
        yield return new WaitForSeconds(Delay);
        StateManager.instance.Acting = false;
        TMPtext.text = null;
        for(int i = 0; i < dialogue.Length; i++)
        {
            TMPtext.text += dialogue[i];
            yield return new WaitForSeconds(talkWaitTime);
        }
        StateManager.instance.Acting = true;
    }
}
