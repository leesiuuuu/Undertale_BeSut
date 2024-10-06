using System.Collections;
using UnityEngine;
public class AttackPattern1 : MonoBehaviour
{
    public int rotateSpeed;
    public Transform target;
    public float Power;
    public AudioClip Fire;
    private float DelayTime;
    private bool isRotate = false;
    private Vector2 direction1;
    private Rigidbody2D rb;
    private bool once = false;
    private bool ONnce1 = false;

    private void Start()
    {
        DelayTime = Random.Range(1.2f, 1.8f);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (target != null && !isRotate)
        {
            Vector2 direction = new Vector2(
                transform.position.x - target.position.x,
                transform.position.y - target.position.y
            );
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);
            transform.rotation = rotation;
            StartCoroutine(StopTimer());
        }
        if (isRotate)
        {
            rb.gravityScale = 0;
            if (!ONnce1)
            {
                SoundManager.instance.SFXPlay("Fire", Fire);
                ONnce1 = true;
            }
            rb.AddForce(direction1 * Power, ForceMode2D.Impulse);
        }
    }
    IEnumerator StopTimer()
    {
        yield return new WaitForSeconds(DelayTime);
        isRotate = true;
        if(!once)
        {
            direction1 = (target.transform.position - transform.position).normalized;
            once = true;
        }
        Destroy(gameObject, 0.75f);
    }
}
