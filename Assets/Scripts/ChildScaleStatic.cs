using UnityEngine;

public class ChildScaleStatic : MonoBehaviour
{
    private Vector3 WorldScale;
    void Start()
    {
        WorldScale = transform.lossyScale;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 parentScale = transform.parent.lossyScale;
        transform.localScale = new Vector3(
            WorldScale.x /  parentScale.x,
            WorldScale.y / parentScale.y,
            WorldScale.z / parentScale.z
            );
    }
}
