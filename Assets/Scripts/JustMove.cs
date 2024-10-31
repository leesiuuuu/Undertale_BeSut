using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class JustMove : MonoBehaviour
{
    //Shake 기본 값
    //0.5f, 1f
    //빌드 기본값
    //0.5f, 0.5f
    private Vector3 MoveVelocity;
    public float MoveSpeed;
    private bool isInvin = false;
    private Animator animator;
    private bool Shield = false;
    public bool NoCool = false;
    private float CoolTime;
    private int damage = 8;
    public bool isBarrierOn = false;
    public GameObject Barrier;
    private ShieldMove SM;
    private void Start()
    {
        Barrier.SetActive(false);
        SM = GetComponent<ShieldMove>();
        CoolTime = 0f;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        MoveVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) MoveVelocity += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) MoveVelocity += Vector3.right;
        if (Input.GetKey(KeyCode.UpArrow)) MoveVelocity += Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow)) MoveVelocity += Vector3.down;
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!isBarrierOn)
            {
                Barrier.SetActive(true);
                isBarrierOn = true;
            }
            else
            {
                Barrier.SetActive(false);
                isBarrierOn = false;
            }
        }
        MoveVelocity = MoveVelocity.normalized;
        transform.position += MoveVelocity * MoveSpeed * Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D cols)
    {
        if (cols.gameObject.CompareTag("AttackSprite"))
        {
            if (!isInvin && !Shield)
            {
                StartCoroutine(Shake(gameObject, 0.5f, 1f));
                PlayerManager.instance.HPChanged();
                isInvin = true;
                animator.SetBool("Hurted", true);
                Invoke("ReturnInvin", 0.7f);
            }
        }
    }
    void ReturnInvin()
    {
        isInvin = false;
        animator.SetBool("Hurted", false);
    }
    IEnumerator Shake(GameObject obj, float Duration = 1f, float Power = 1f)
    {
        Vector3 origin = obj.transform.position;
        while (Duration > 0f)
        {
            Duration -= 0.05f;
            obj.transform.position = origin + (Vector3)Random.insideUnitCircle * Power * Duration;
            yield return null;
        }
        obj.transform.position = origin;
    }
}
