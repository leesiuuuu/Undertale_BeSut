using System.Collections;
using UnityEngine;

public class AttackPatternA14M : MonoBehaviour
{

    [SerializeField]
    private GameObject LaserPortal;

    [SerializeField]
    private GameObject Left;
    [SerializeField]
    private GameObject Right;
    [SerializeField]
    private GameObject Up;
    [SerializeField]
    private GameObject Down;

    [SerializeField]
    private GameObject Ques;

    private Vector3[] Pos = new Vector3[4];

    private bool EndTime = false;

    public UICode UC;

    private void OnEnable()
    {
        EndTime = false;
        Pos[0] = new Vector3(4.6f, -1.3f, 0); //왼쪽
        Pos[1] = new Vector3(-4.6f, -1.3f, 0); //오른쪽
        Pos[2] = new Vector3(0, 2.4f, 0); //위
        Pos[3] = new Vector3(0, -3.9f, 0); //아래
        StartCoroutine(Pattern14());
    }
    IEnumerator Pattern14()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject Clone = Instantiate(Ques);
        StartCoroutine(LaserBoom());
        StartCoroutine(Attacker());
        yield return new WaitForSeconds(15f);
        EndTime = true;
        yield return new WaitForSeconds(4f);
        this.enabled = false;
        Destroy(Clone);
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
            while (RandomNum == Before)
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
    IEnumerator Attacker()
    {
        int before = -1;
        int RandomNum = Random.Range(0, 4);
        while (!EndTime)
        {
            while(RandomNum == before)
            {
                RandomNum = Random.Range(0, 4);
            }
            switch (RandomNum)
            {
                case 1: Instantiate(Left); break;
                case 2: Instantiate(Right); break;
                case 3: Instantiate(Up); break;
                case 4: Instantiate(Down); break;
            }
            before = RandomNum;
            yield return new WaitForSeconds(Random.Range(0.4f, 0.7f));
        }
    }
}
