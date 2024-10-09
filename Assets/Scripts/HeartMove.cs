using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HeartMove : MonoBehaviour
{
    //Shake 기본 값
    //0.5f, 1f
    //빌드 기본값
    //0.5f, 0.5f
    private Vector3 MoveVelocity;
    public float MoveSpeed;
    public GameObject ShieldObj;
    public AudioClip HurtClip;
    public AudioClip ShieldSound;
    public AudioClip SheldBreak;
    private bool isInvin = false;
    private Animator animator;
    private bool Shield = false;
    private void OnEnable()
    {
        if (Shield)
        {
            ShieldObj.SetActive(true);
        }
    }
    private void Start()
    {
        ShieldObj = transform.GetChild(0).gameObject;
        ShieldObj.SetActive(false);
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        MoveVelocity = Vector3.zero;
        if(Input.GetKey(KeyCode.LeftArrow)) MoveVelocity += Vector3.left;
        if(Input.GetKey(KeyCode.RightArrow)) MoveVelocity += Vector3.right;
        if(Input.GetKey(KeyCode.UpArrow)) MoveVelocity += Vector3.up;
        if(Input.GetKey(KeyCode.DownArrow)) MoveVelocity += Vector3.down;
        if (Input.GetKeyDown(KeyCode.C) && !Shield)
        {
            SoundManager.instance.SFXPlay("Shield", ShieldSound);
            ShieldObj.SetActive(true);
            MoveSpeed -= 2f;
            Shield = true;
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
                PlayerManager.instance.HP -= 8;
                SoundManager.instance.SFXPlay("Hurt", HurtClip);
                StartCoroutine(Shake(gameObject, 0.5f, 1f));
                PlayerManager.instance.HPChanged();
                isInvin = true;
                animator.SetBool("Hurted", true);
                Debug.Log(PlayerManager.instance.HP);
                Invoke("ReturnInvin", 0.7f);
            }
            else if (Shield)
            {
                Shield = false;
                ShieldObj.SetActive(false);
                MoveSpeed += 2f;
                SoundManager.instance.SFXPlay("ShieldBreak", SheldBreak);
            }
        }
    }
    void ReturnInvin()
    {
        isInvin = false;
        animator.SetBool("Hurted", false);
    }
    private void OnDisable()
    {
        ShieldObj.SetActive(false);
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
