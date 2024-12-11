using System.Collections;
using UnityEngine;

public class AttackPatternA11M : MonoBehaviour
{
    [SerializeField]
    private GameObject LaserPortal;

    private Vector3[] Pos = new Vector3[4];

    private bool EndTime = false;

    [SerializeField]
    private Vector2 BoxRange;
    [SerializeField]
    private GameObject Damager;

    public AudioClip AC;

    public UICode UC;

    private void OnEnable()
    {
        EndTime = false;
        Pos[0] = new Vector3(4.6f, -1.3f, 0); //왼쪽
        Pos[1] = new Vector3(-4.6f, -1.3f, 0); //오른쪽
        Pos[2] = new Vector3(0, 2.4f, 0); //위
        Pos[3] = new Vector3(0, -3.9f, 0); //아래
        StartCoroutine(Pattern11());
    }
    IEnumerator Pattern11()
    {
        yield return new WaitForSeconds(0.5f);
        int RandomNum = Random.Range(0, 5);
        StartCoroutine(LaserBoom());
        StartCoroutine(SquareAppearence());
        yield return new WaitForSeconds(15f);
        EndTime = true;
        yield return new WaitForSeconds(4f);
        this.enabled = false;
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
        yield break;

    }
    IEnumerator LaserBoom()
    {
        int Before = -1;
        int RandomNum = Random.Range(0, 4);
        while (!EndTime)
        {
            while(RandomNum == Before)
            {
                RandomNum = Random.Range(0, 4);
            }
            Quaternion Rotate = Quaternion.Euler(0, 0, 0);
            switch (RandomNum)
            {
                case 0:
                    Rotate = Quaternion.Euler(0, 0, 0);
                    break;
                case 1:
                    Rotate = Quaternion.Euler(0, 0, 180);
                    break;
                case 2:
                    Rotate = Quaternion.Euler(0, 0, 90);
                    break;
                case 3:
                    Rotate = Quaternion.Euler(0, 0, -90);
                    break;
            }
            GameObject Clone = Instantiate(LaserPortal, Pos[RandomNum], Rotate);

            Laser_Ex LE = Clone.GetComponentInChildren<Laser_Ex>();
            switch (RandomNum)
            {
                case 0: LE.XYCheck = 1; break;
                case 1: LE.XYCheck = 1; break;
                case 2: LE.XYCheck = 0; break;
                case 3: LE.XYCheck = 0; break;
            }
            yield return new WaitForSeconds(3.5f);
            Transition t = Clone.transform.Find("Laser").gameObject.AddComponent<Transition>();
            t.Duration = 1f;
            Destroy(Clone, 1f);
            Before = RandomNum;
        }
    }
    IEnumerator SquareAppearence()
    {
        while (!EndTime)
        {
            float BoxX = Random.Range((BoxRange.x / 2) * -1, BoxRange.x / 2);
            float BoxY = Random.Range((BoxRange.y / 2) * -1, BoxRange.y / 2);
            Vector3 RandomPosition = transform.position + new Vector3(BoxX, BoxY, 0);
            SoundManager.instance.SFXPlay("Create", AC);
            GameObject Clone = Instantiate(Damager, RandomPosition, Quaternion.identity);
            Clone.GetComponent<AttackPatternA5>().Rotate = Random.Range(0f, 360f);
            Clone.GetComponent<AttackPatternA5>().SpawnTime = 0.3f;
            Clone.GetComponent<AttackPatternA5>().DeSpawnTime = 0.3f;
            Clone.GetComponent<AttackPatternA5>().MaintainTime = 0.3f;
            yield return new WaitForSeconds(1.5f);
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(gameObject.transform.position, BoxRange);
    }
}
