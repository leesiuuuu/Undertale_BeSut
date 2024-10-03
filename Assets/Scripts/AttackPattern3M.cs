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

    public AudioClip AC;
    public AudioClip AC3;
    public UICode UC;

    public static bool isAllCreated;
    private void Start()
    {
        StartCoroutine(CreateDamager1());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObject.transform.position, BoxRange);
    }
    IEnumerator CreateDamager1()
    {
        for(int j = 0; j < 4; j++)
        {
            isAllCreated = false;
            yield return new WaitForSeconds(0.7f);
            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(Delay);
                float BoxX = Random.Range((BoxRange.x / 2) * -1, BoxRange.x / 2);
                float BoxY = Random.Range((BoxRange.y / 2) * -1, BoxRange.y / 2);
                Vector3 RandomPosition = new Vector3(BoxX, BoxY, 0);
                SoundManager.instance.SFXPlay("Create", AC);
                Instantiate(Damager, RandomPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.2f);
            isAllCreated = true;
            SoundManager.instance.SFXPlay("Laser", AC3);
            yield return new WaitForSeconds(1.5f);
        }
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }
}
