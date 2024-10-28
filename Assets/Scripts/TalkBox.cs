using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkBox : MonoBehaviour
{
    private string dialogue;
    public float talkWaitTime;
    public float Delay;
    public TMP_Text TMPtext;
    public AudioClip Log;
    void Start()
    {
        Talk(Delay, StateManager.instance.DialogueChanger(StateManager.instance.TurnCount + 1, dialogue));
    }
    public void Talk(float DelayTime, string dialogue1)
    {
        StartCoroutine(TalkStart(dialogue1, talkWaitTime, DelayTime));
    }
    IEnumerator TalkStart(string dialogue, float talkWaitTime, float Delay = 0f)
    {
        yield return new WaitForSeconds(Delay);
        StateManager.instance.Acting = false;
        StateManager.instance.logAppear = true;
        TMPtext.text = null;
        for(int i = 0; i < dialogue.Length; i++)
        {
            TMPtext.text += dialogue[i];
            SoundManager.instance.SFXPlay("Log", Log);
            yield return new WaitForSeconds(talkWaitTime);
        }
        StateManager.instance.Acting = true;
        StateManager.instance.logAppear = false;
    }
}
