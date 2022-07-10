using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;

    public int bestScore;

    public Text _bestText;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;
    
    void Start()
    {
        _scoreText.text = "Score :" + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is Null");
        }

        bestScore = PlayerPrefs.GetInt("HighScore" , 0);

        _bestText.text = "Best :" + bestScore;
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score :" + playerScore;
    }


    public void CheckForBestScore(int playerScore)
    {
        if( playerScore > bestScore)
        {
            bestScore = playerScore;
            PlayerPrefs.SetInt("HighScore", bestScore);
            _bestText.text = "Best :" + bestScore;
        }
    }

    public void UpdateLives(int _currentlives)
    {
        _livesImg.sprite = _liveSprites[_currentlives];

        if(_currentlives == 0)
        {
            GameOverSequence();
        }
    }


    public void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        //GameManager gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _gameManager.ResumeGame();
        //gm.ResumeGame();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
