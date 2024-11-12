using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    public static BtnManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartGame() 
    {
        Debug.Log("Game Start!");
    }
    public void TutorialGame() 
    {
        Debug.Log("Game Tutorial!");
    }
    public void SettingGame() 
    {
        Debug.Log("Game Setting!");
    }
    public void ExitGame() 
    {
        Debug.Log("Game Exit!");
    }
}
