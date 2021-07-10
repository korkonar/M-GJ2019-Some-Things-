using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayMenu : MonoBehaviour
{
    public GameObject modalPanelObject;

    public void Resume()
    {
        modalPanelObject.SetActive(false);
        PossessableObjectsClickManager.menuOpen = false;
    }

    public void Restart()
    {
        modalPanelObject.SetActive(false);
        PossessableObjectsClickManager.menuOpen = false;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        modalPanelObject.SetActive(false);
        PossessableObjectsClickManager.menuOpen = false;
    }
}
