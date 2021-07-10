using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void Play()
    {
        // Debug.Log("load game level");
        SceneManager.LoadScene(2);
    }

    public void Credits()
    {
        // Debug.Log("load credits scene");
        SceneManager.LoadScene(1);    
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
