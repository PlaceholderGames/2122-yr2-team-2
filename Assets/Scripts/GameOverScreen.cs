using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
