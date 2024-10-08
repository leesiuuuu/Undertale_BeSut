using System.Collections;
using UnityEngine;

public class AttackPattern4M : MonoBehaviour
{
    public GameObject Effect;
    public GameObject AttackObj;
    public UICode UC;
    public int RepeatCount;

    public AudioClip Obj4Se;

    //이펙트 생성 위치
    Vector3 UpPoint = new Vector3(0,0,0);
    Vector3 DownPoint = new Vector3(0,-2.6f,0);
    Vector3 LeftPoint = new Vector3(-1.8f, -1.3f, 0);
    Vector3 RightPoint = new Vector3(1.8f, -1.3f, 0);

    int LoopTime;
    float[] Times;
    int[] dir;

    //생성 시 랜덤한 수 확인
    public static int LastRotate;

    protected int dirNum = 0;
    
    void RotateInit()
    {
        dirNum = (int)Random.Range(0, 4);
        GameObject Clone;
        switch (dirNum)
        {
            case 0:
                Clone = Instantiate(Effect, UpPoint, Quaternion.identity);
                SoundManager.instance.SFXPlay("Obj4Se", Obj4Se);
                Destroy(Clone, 0.2f);
                break;
            case 1:
                Clone = Instantiate(Effect, DownPoint, Quaternion.identity);
                SoundManager.instance.SFXPlay("Obj4Se", Obj4Se);
                Destroy(Clone, 0.2f);
                break;
            case 2:
                Clone = Instantiate(Effect, LeftPoint, Quaternion.identity);
                SoundManager.instance.SFXPlay("Obj4Se", Obj4Se);
                Destroy(Clone, 0.2f);
                break;
            case 3:
                Clone = Instantiate(Effect, RightPoint, Quaternion.identity);
                SoundManager.instance.SFXPlay("Obj4Se", Obj4Se);
                Destroy(Clone, 0.2f);
                break;
        }
    }

    private void Start()
    {
        StartCoroutine(AtkPtn41());
    }
    IEnumerator AtkPtn41()
    {
        LoopTime = (int)Random.Range(3, 6);
        Times = new float[LoopTime];
        dir = new int[LoopTime];
        for(int n = 0; n <RepeatCount; n++)
        {
            for (int i = 0; i < LoopTime; i++)
            {
                float RandomTime = Random.Range(0.3f, 0.7f);
                Times[i] = RandomTime;
                RotateInit();
                dir[i] = dirNum;
                yield return new WaitForSeconds(RandomTime);
            }
            for (int j = 0; j < Times.Length; j++)
            {
                LastRotate = dir[j];
                Instantiate(AttackObj);
                yield return new WaitForSeconds(Times[j]);
            }
            yield return new WaitForSeconds(1.2f);
        }
        StateManager.instance.Fighting = false;
        UC.MyTurnBack();
    }
}
