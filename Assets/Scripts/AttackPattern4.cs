using JetBrains.Annotations;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackPattern4 : MonoBehaviour
{
    float Timer1 = 0;
    public float EndTime;
    public float Power;

    public AudioClip SD;

    private Vector3 direction;
    void Start()
    {
        direction = Vector3.zero;
        switch (AttackPattern4M.LastRotate)
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
        SoundManager.instance.SFXPlay("SD", SD);
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
}
