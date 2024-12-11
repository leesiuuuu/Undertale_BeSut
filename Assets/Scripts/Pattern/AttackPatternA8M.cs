using System.Collections;
using UnityEngine;

public class AttackPatternA8M : MonoBehaviour
{
    [SerializeField]
    private Sprite Warning;
    [SerializeField]
    private int Count;
    [SerializeField]
    private GameObject Shaw;
    public UICode UC;

    public AudioClip WarningSound;
    public AudioClip ShawSound;
    private void OnEnable()
    {
        StartCoroutine(Pattern8());
    }
    IEnumerator Pattern8()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < Count; i++)
        {
            bool[] dir = new bool[4];
            for (int j = 0; j < dir.Length; j++)
            {
                bool LR = Random.value > 0.5f;
                GameObject wr = new GameObject();
                wr.AddComponent<SpriteRenderer>();
                wr.transform.localScale = new Vector3(3.3f, 3.3f, 3.3f);
                wr.GetComponent<SpriteRenderer>().sprite = Warning;
                if (LR)
                {
                    wr.transform.position = new Vector3(-1.05f, -1.89f, 0);
                    SoundManager.instance.SFXPlay("Warning", WarningSound);
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    wr.transform.position = new Vector3(1.05f, -1.89f, 0f);
                    SoundManager.instance.SFXPlay("Warning", WarningSound);
                    yield return new WaitForSeconds(0.2f);
                }
                dir[j] = LR;
                Destroy(wr);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            float y = -2.54f;
            for (int l = 0; l < dir.Length; l++)
            {
                float x = 0;
                if (dir[l] == true) { x = -1.5f; }
                else { x = 1.5f; }
                SoundManager.instance.SFXPlay("Shaw", ShawSound);
                for (int k = 0; k < 4; k++)
                {
                    Instantiate(Shaw, new Vector3(x, y, 0f), Quaternion.identity);
                    if (dir[l]) { x += 0.4f; }
                    else { x -= 0.4f; }
                }
                yield return new WaitForSeconds(1f);
            }
        }
        this.enabled = false;
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }
}
