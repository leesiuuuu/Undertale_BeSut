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
    }
    public void ExitGame() 
    {
        Debug.Log("Game Exit!");
    }
    public void AchiGame()
    {
        Debug.Log("Achi Window!");
    }
    public void MoveTutorial()
    {

    }
    public void SeleteTutorial() { }
    public void CancelTutorial() { }
    public void ShieldTutorial() { }
    public void QQQTutorial() { }
    public void ExitTutorial() { }
}
