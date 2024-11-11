using UnityEngine;

public class RotateMagicJhin : MonoBehaviour
{
    public float Speed;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Speed));
    }
}
