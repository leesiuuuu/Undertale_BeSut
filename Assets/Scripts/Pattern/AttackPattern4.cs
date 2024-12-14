using UnityEngine;

public class AttackPattern4 : MonoBehaviour
{
    float Timer1 = 0;
    public float EndTime;
    //이새끼도 똑같음.... 빌드할 때랑 디버깅 할 때랑 속도가 3 ~ 4배 차이남
    //ex) 50 -> 170
    //추가로 못찾을까봐 여기다 적는데 Shake 강도가 빌드에서는 너무 큼. 조정 필요
    public float Power;

    public AudioClip SD;

    private Vector3 direction;
    void Start()
    {
        direction = Vector3.zero;
        //이게 메인 코드 입니당
        //RotateSetting(StateManager.instance._10Ptn ? AttackPatternA15M.LastRotate : AttackPattern4M.LastRotate);
        RotateSetting(AttackPatternA15M.LastRotate);
    }
    void Update()
    {
        if(Timer1 < EndTime)
        {
            Timer1 += Time.deltaTime;
            transform.position += direction * 0.001f * Power;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RotateSetting(int LastRotate)
    {
        switch (LastRotate)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                direction = Vector3.up;
                transform.position = new Vector3(0, -6.86f, 0);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                direction = Vector3.down;
                transform.position = new Vector3(0, 6.86f, 0);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                direction = Vector3.left;
                transform.position = new Vector3(8.14f, -1.49f, 0);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                direction = Vector3.right;
                transform.position = new Vector3(-8.14f, -1.49f, 0);
                break;
        }
        //SoundManager.instance.SFXPlay("SD", SD);
    }
}
