using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void LoadSinglePlayerGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LoadCoopMode()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
