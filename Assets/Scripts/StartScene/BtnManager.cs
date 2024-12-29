using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public static BtnManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SoundManager.instance.StartMenuPlay();
    }
    public void StartGame() 
    {
        Debug.Log("Game Start!");
        //씬 이동 로직 추가
        Destroy(gameObject);
    }
    public void TutorialGame() 
    {
        Debug.Log("Game Tutorial!");
        SceneManager.LoadScene("TutorialScene");
    }
    public void SettingGame() 
    {
        Debug.Log("Game Setting!");
        SceneManager.LoadScene("SettingScene");
    }
    public void ExitGame() 
    {
        PlayerPrefs.DeleteKey("CurrentNum");
        Debug.Log("Game Exit!");
    }
    public void AchiGame()
    {
        Debug.Log("Achi Window!");
        SceneManager.LoadScene("AchiScene");
    }
    public void MoveTutorial()
    {

    }
    public void SeleteTutorial() { }
    public void CancelTutorial() { }
    public void ShieldTutorial() { }
    public void QQQTutorial() { }
    public void ExitTutorial()
    {
        SceneManager.LoadScene("StartScene");
    }
}
