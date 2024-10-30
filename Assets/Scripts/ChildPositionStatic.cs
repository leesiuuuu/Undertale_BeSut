using UnityEngine;

public class ChildPositionStatic : MonoBehaviour
{
    private Vector3 worldPosition;
    private Vector3 leastparentPos;
    private Vector3 EffectPos = new Vector3(-0.055f, -0.165f, 0);
    private float fixedY;
    void Start()
    {
        worldPosition = transform.position;
        fixedY = EffectPos.y;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        leastparentPos = transform.parent.position;
        Vector3 parentPos = transform.parent.position;
        if(parentPos.x > leastparentPos.x)
        {
            transform.position = new Vector3(
                worldPosition.x + parentPos.x,
                worldPosition.y - parentPos.y,
                0);
        }
        else if(parentPos.x < leastparentPos.x)
        {
            transform.position = new Vector3(
                worldPosition.x - parentPos.x,
                worldPosition.y + parentPos.y,
                0);
        }
        else
        {
            transform.position = EffectPos;
        }
    }
}
