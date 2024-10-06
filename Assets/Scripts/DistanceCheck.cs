using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
    public static int DistancetoDamage(float Mid, float Slider)
    {
        float distance = Mathf.Abs(Mid - Slider);
        return DamageReturn((int)distance);
    }
    public static int DamageReturn(int Distance)
    {
        if (Distance < 0.2 && Distance > -0.2) return Random.Range(50, 89);
        return (int)(40f / Distance);
    }
}
