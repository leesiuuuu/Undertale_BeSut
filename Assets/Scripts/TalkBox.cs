using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkBox : MonoBehaviour
{
    public string dialogue;
    public float talkWaitTime;
    public TMP_Text TMPtext;
    void Start()
    {
        StartCoroutine(TalkStart(dialogue, talkWaitTime));
    }
    IEnumerator TalkStart(string dialogue, float talkWaitTime)
    {
        TMPtext.text = null;
        for(int i = 0; i < dialogue.Length; i++)
        {
            TMPtext.text += dialogue[i];
            yield return new WaitForSeconds(talkWaitTime);
        }
    }
}
