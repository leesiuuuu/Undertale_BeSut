using TMPro;
using UnityEngine;

public class NameInput : MonoBehaviour
{
    private TMP_InputField ip;
    private bool isActive = false;

    private void Awake()
    {
        ip = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        // Z Ű�� �Է� �ʵ� Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ActivateInputField();
        }

        // ESC Ű�� �Է� �ʵ� ��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape) && isActive)
        {
            DeactivateInputField();
        }
    }

    private void ActivateInputField()
    {
        isActive = true;
        ip.readOnly = false; // �Է� ���� ���·� ����
        ip.text = "";        // �ʱ�ȭ (���� ����)
        ip.caretPosition = 0;
        ip.selectionStringFocusPosition = 0; // Ŀ�� ��ġ �ʱ�ȭ
        ip.selectionStringAnchorPosition = 0;
    }

    private void DeactivateInputField()
    {
        isActive = false;
        ip.readOnly = true; // �ٽ� �Է� �Ұ��� ���·� ����
    }
}
