using System.Collections;
using UnityEngine;

public class AttackPattern3M : MonoBehaviour
{
    [SerializeField]
    private Vector2 BoxRange;
    [SerializeField]
    private GameObject Damager;
    [SerializeField]
    private float Delay;
    [SerializeField]
    private int repeatCount;

    public AudioClip AC;
    public AudioClip AC3;
    public UICode UC;

    public static bool isAllCreated;
    void OnEnable()
    {
        StartCoroutine(CreateDamager1());
    }
    IEnumerator CreateDamager1()
    {
        for (int j = 0; j < 4; j++)
        {
            isAllCreated = false;
            yield return new WaitForSeconds(0.7f);
            for (int i = 0; i < repeatCount; i++)
            {
                yield return new WaitForSeconds(Delay);
                float BoxX = Random.Range((BoxRange.x / 2) * -1, BoxRange.x / 2);
                float BoxY = Random.Range((BoxRange.y / 2) * -1, BoxRange.y / 2);
                Vector3 RandomPosition = transform.position + new Vector3(BoxX, BoxY, 0);
                SoundManager.instance.SFXPlay("Create", AC);
                Instantiate(Damager, RandomPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.2f);
            isAllCreated = true;
            SoundManager.instance.SFXPlay("Laser", AC3);
            yield return new WaitForSeconds(1.5f);
        }
        this.enabled = false;
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObject.transform.position, BoxRange);
    }
}
