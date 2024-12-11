using System.Collections;
using UnityEngine;

public class AttackPatternA10M : MonoBehaviour
{
    [SerializeField]
    private GameObject BigLaser;

    public UICode UC;
    public GameObject Player;

    [SerializeField]
    private GameObject _Camera;
    private Camera _camera;

    private void OnEnable()
    {
        _camera = _Camera.GetComponent<Camera>();
        StartCoroutine(Pattern10());
    }
    IEnumerator Pattern10()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UC.BossAttack(2, "���� ���̴�.", "�� �̻� ����� ������ ����.", "�׾��."));
        yield return new WaitForSeconds(3.5f);
        GameObject Clone = Instantiate(BigLaser);
        PosMove ClonePM = Clone.GetComponent<PosMove>();
        Laser_Ex LE = Clone.GetComponentInChildren<Laser_Ex>();
        LE._LaserDelay = 5f;
        ClonePM.Delay = 2f;
        yield return new WaitForSeconds(2f);
        _Camera.gameObject.transform.position = new Vector3(0, 0, -50f);
        //ī�޶� ���� ���� ����
        Vector3 startpos = _Camera.gameObject.transform.position;
        Vector3 endpos = new Vector3(0, -1.31f, -50f);
        _camera.orthographicSize = 5f;
        float startsize = _camera.orthographicSize;
        float endsize = 1.9f;
        float elapsed = 0f;
        float duration = 1f;
        yield return new WaitForSeconds(1f);
        //���� ���� �̵�
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = easeOutQuint(t);
            // ��ġ�� ũ�� Lerp ����
            _camera.gameObject.transform.position = Vector3.Lerp(startpos, endpos, t);
            _camera.orthographicSize = Mathf.Lerp(startsize, endsize, t);

            yield return null; // ���� �����ӱ��� ���
        }
        yield return new WaitForSeconds(1.02f);
        SetTimeScale(0.005f);
        StateManager.instance._10Ptn = true;
        while (true)
        {

            //vŰ �Է� ���� ��
            if (Input.GetKeyDown(KeyCode.V))
            {
                SetTimeScale(1);
                elapsed = 0;
                //���� ���� �̵�
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    float t = elapsed / duration;
                    t = easeOutQuint(t);
                    // ��ġ�� ũ�� Lerp ����
                    _camera.gameObject.transform.position = Vector3.Lerp(endpos, startpos, t);
                    _camera.orthographicSize = Mathf.Lerp(endsize, startsize, t);
                    yield return null;
                }
                //
                break;
            }
            else
            {
                yield return null;
            }
        }
        yield return new WaitForSeconds(1.5f);
        if (StateManager.instance.NormalFaze2)
        {
            int[] SpriteNum = { 6, 5, 8, 2};
            int[] logCount = { 0, 3, 6, 7};
            StartCoroutine(UC.BossSpriteChangeLog(SpriteNum, logCount, "���....?",
                "�������� ���� �� �ִ� ������ ��ȯ�ϴٴ�...",
                "...����",
                "�̹��� ���� �������� �ǰڱ�."));
        }
        else
        {
            int[] SpriteNum = { 8, 7, 8, 2 };
            int[] logCount = { 0, 3 ,5, 6};
            StartCoroutine(UC.BossSpriteChangeLog(SpriteNum, logCount, "���....?",
                "�������� ���� �� �ִ� ������ ��ȯ�ϴٴ�...",
                "...",
                ".....���� �Ѱ谡 ������...",
                "���� �� ��ƿ �� ����.",
                "�� �� �ϳ��� �װ� �ǰ���.",
                "������ ���� ������ �����غ��ڰ�."));
        }
        yield break;
    }
    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }
    public float easeOutQuint(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }
}
