using TMPro;
using UnityEngine;

public class MaterialSet : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro _textMeshPro;

    private void Start()
    {
        var material = new Material(_textMeshPro.fontMaterial);
        material.SetFloat("_StencilComp", 3.0f);

        _textMeshPro.fontMaterial = material;
    }
}
