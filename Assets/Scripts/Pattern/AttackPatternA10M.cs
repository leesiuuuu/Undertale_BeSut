using System.Collections;
using TMPro;
using UnityEngine;

public class AttackPatternA10M : MonoBehaviour
{
    [SerializeField]
    private GameObject BigLaser;

    private UICode UC;
    public GameObject Player;

    [SerializeField]
    private SmoothCameraFollow CF;
    private Camera _Camera;

    private void OnEnable()
    {
        _Camera = CF.gameObject.GetComponent<Camera>();
        StartCoroutine(Pattern10());
    }
    IEnumerator Pattern10()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject Clone = Instantiate(BigLaser);
        Clone.GetComponentInChildren<Laser_Ex>()._LaserDelay = 3;
        //StartCoroutine(UC.BossAttack("���� ���̴�.", "�� �̻� ����� ������ ����.", "�׾��."));
        _Camera.gameObject.transform.position = new Vector3(0, 0, -50f);
        Vector3 startpos = _Camera.gameObject.transform.position;
        Vector3 endpos = new Vector3(0, -1.31f, -50f);
        _Camera.orthographicSize = 5f;
        float startsize = _Camera.orthographicSize;
        float endsize = 1.9f;
        float elapsed = 0f;
        float duration = 1f;
        yield return new WaitForSeconds(0.5f);
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = easeOutQuint(t);
            // ��ġ�� ũ�� Lerp ����
            _Camera.gameObject.transform.position = Vector3.Lerp(startpos, endpos, t);
            _Camera.orthographicSize = Mathf.Lerp(startsize, endsize, t);

            yield return null; // ���� �����ӱ��� ���
        }
        yield return new WaitForSeconds(1.52f);
        SetTimeScale(0.005f);
    }
    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
        Time.fixedDeltaTime = time;
    }
    public float easeOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }
}
