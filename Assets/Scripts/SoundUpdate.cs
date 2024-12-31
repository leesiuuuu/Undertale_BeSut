using UnityEngine;

public class SoundUpdate : MonoBehaviour
{
    public void Start()
    {
        if (PlayerPrefs.HasKey("vol"))
        {
            SoundManager.instance.Volume(PlayerPrefs.GetFloat("vol"));
        }
    }
}