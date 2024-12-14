using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPattern5_2 : MonoBehaviour
{
    IEnumerator Blink(GameObject AtkObj, float N, AudioClip SF)
    {
        for (int j = 8; j > 4; j--)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 2552, 255, 1);
            yield return new WaitForSeconds(0.05f);
            GetComponent<SpriteRenderer>().color = new Color(255, 2552, 255, 0);
            yield return new WaitForSeconds(0.05f);
            //SoundManager.instance.SFXPlay("Warning", SF);
        }
        GameObject Clone = Instantiate(AtkObj);
        Clone.GetComponent<AttackPattern5_1>().StartPos.x = N;
        Clone.GetComponent<AttackPattern5_1>().EndPos.x = N;
        Destroy(gameObject);
    }
    public void Nigg(float Random, GameObject AtkObj, AudioClip S)
    {
        StartCoroutine(Blink(AtkObj, Random, S));
    }
}
