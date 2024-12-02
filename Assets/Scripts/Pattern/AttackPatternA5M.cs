using System.Collections;
using UnityEngine;

public class AttackPatternA5M : MonoBehaviour
{
    public GameObject AtkObj;
    public UICode UC;
    private void OnEnable()
    {
        StartCoroutine(Pattern());
    }
    IEnumerator Pattern()
    {
        yield return new WaitForSeconds(0.7f);
        Vector3 Pos = new Vector3(-0.78f, 0, 0);
        //패턴 1
        for(int i = 0; i < 3; i++)
        {
            GameObject Clone = Instantiate(AtkObj, Pos, Quaternion.identity);
            Clone.GetComponent<AttackPatternA5>().Rotate = 90f;
            yield return new WaitForSeconds(0.2f);
            Pos.x += 0.9f;
        }
        yield return new WaitForSeconds(2f);
        Vector3 Pos2 = new Vector3(1.13f, -1.18f, 0);
        //패턴 2
        for(int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0: break;
                case 1: Pos2.y = -2.05f; break;
                case 2: Pos2.x = -0.38f;
                    Pos2.y = -1.79f;
                    break;
                case 3: Pos2.x = 0.99f; break;
            }
            GameObject clone = Instantiate(AtkObj, Pos2, Quaternion.identity);
            if (i == 2 || i == 3) clone.GetComponent<AttackPatternA5>().Rotate = 90f;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2f);
        //패턴 3
        Vector3 Pos3 = new Vector3(1.13f, -1.92f, 0);
        for(int i  = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0: break;
                case 1: Pos3.y = -0.54f; break;
                case 2: Pos3.x = -1.03f;
                    Pos3.y = -0.54f;
                    break;
                default:
                    Pos3.x += 1; break;
            }
            GameObject clone = Instantiate(AtkObj, Pos3, Quaternion.identity);
            if (i > 1) clone.GetComponent<AttackPatternA5>().Rotate = 90;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2.4f);
        //턴 전환 코드
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }
}
