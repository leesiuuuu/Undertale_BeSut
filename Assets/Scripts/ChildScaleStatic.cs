using UnityEngine;

public class ChildScaleStatic : MonoBehaviour
{
    private Vector3 lastScale;
    void Start()
    {
        lastScale = transform.localScale;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localScale = lastScale;
    }
}
