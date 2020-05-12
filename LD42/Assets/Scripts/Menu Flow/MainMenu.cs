using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Shop_backup");
    }

    public void ExitGame ()
    {
        Application.Quit();
    }
}
