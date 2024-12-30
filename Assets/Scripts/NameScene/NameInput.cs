using TMPro;
using UnityEngine;

public class NameInput : MonoBehaviour
{
    private TMP_InputField ip;
    private bool isActive = false;

    private void Start()
    {
        ip = GetComponent<TMP_InputField>();
        ip.readOnly = true; // 기본적으로 마우스 입력 비활성화
    }

    private void Update()
    {
        // Z 키로 입력 필드 활성화
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ActivateInputField();
        }

        // ESC 키로 입력 필드 비활성화
        if (Input.GetKeyDown(KeyCode.Escape) && isActive)
        {
            DeactivateInputField();
        }
    }

    private void ActivateInputField()
    {
        isActive = true;
        ip.readOnly = false; // 입력 가능 상태로 변경
        ip.text = "";        // 초기화 (선택 사항)
        ip.caretPosition = 0;
        ip.selectionStringFocusPosition = 0; // 커서 위치 초기화
        ip.selectionStringAnchorPosition = 0;
    }

    private void DeactivateInputField()
    {
        isActive = false;
        ip.readOnly = true; // 다시 입력 불가능 상태로 설정
    }
}
