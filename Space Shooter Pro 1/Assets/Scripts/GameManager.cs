using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool _isCoopMode = false;



    [SerializeField]
    private bool _isGameOver;


    [SerializeField]
    private GameObject _pauseMenuPanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }

    }


    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _pauseMenuPanel.SetActive(false);

    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
