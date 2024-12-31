using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    private Slider slider;

    private bool seleted;
    private bool GoMainSeleted;
    private GameObject bg;

    [SerializeField]
    private TMP_Text GoMain;

    private void Awake()
    {
        seleted = false;
        GoMainSeleted = false;
        GoMain.color = Color.white;
        slider = GetComponent<Slider>();
        bg = GameObject.Find("Background");
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("vol"))
        {
            slider.value = PlayerPrefs.GetFloat("vol");
            SoundManager.instance.Volume(PlayerPrefs.GetFloat("vol"));
        }
    }
    void Update()
    {
        if (seleted)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    slider.value += 0.005f;
                }
                else
                {
                    slider.value += 0.001f;
                }
                Mathf.Clamp(slider.value, 0f, 1f);
                SoundManager.instance.Volume(slider.value);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    slider.value -= 0.005f;
                }
                else
                {
                    slider.value -= 0.001f;
                }
                Mathf.Clamp(slider.value, 0f, 1f);
                SoundManager.instance.Volume(slider.value);
            }
        }


        if(Input.GetKeyDown(KeyCode.Z))
        {
            if (seleted)
            {
                seleted = false;
                bg.GetComponent<Image>().color = Color.white;
            }
            else if (GoMainSeleted)
            {
                SaveVolume(slider.value);
                SceneManager.LoadScene("StartScene");
            }
            else
            {
                seleted = true;
                bg.GetComponent<Image>().color = Color.yellow;
            }
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (seleted)
            {
                seleted = false;
                bg.GetComponent<Image>().color = Color.white;
            }
            else if (GoMainSeleted)
            {
                GoMain.color = Color.white;
                GoMainSeleted = false;
            }
            else
            {
                SaveVolume(slider.value);
                SceneManager.LoadScene("StartScene");
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !GoMainSeleted)
        {
            bg.GetComponent<Image>().color = Color.white;
            GoMain.color = Color.yellow;
            GoMainSeleted = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GoMainSeleted = false;
            seleted = true;
            bg.GetComponent<Image>().color = Color.yellow;
            GoMain.color = Color.white;
        }
    }
    public void SaveVolume(float vol)
    {
        PlayerPrefs.SetFloat("vol", vol);
        PlayerPrefs.Save();
    }
}
