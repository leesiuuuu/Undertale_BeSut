using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
    public static int DistancetoDamage(float Mid, float Slider)
    {
        float distance = Mathf.Abs(Mid - Slider);
        return DamageReturn(distance);
    }
    public static int DamageReturn(float Distance)
    {
        return (int)(10f / Distance);
    }
}
