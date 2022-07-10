using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float _firerate = 0.5f;
    [SerializeField]
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
  
    private bool _isTripleShotActive = false;
   
    private bool _isSpeedBoostActive = false;

    private bool _isShieldsActive = false;

    public bool _isPlayerOne = false;
    public bool _isPlayerTwo = false;



    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _rightEngine , _leftEngine;

    [SerializeField]
    private int _score;

    private UIManager _uiManager;

    private GameManager _gameManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
   
    private AudioSource _audioSource;
                                         
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager._isCoopMode == false)
        {
      
            transform.position = new Vector3(0, 0, 0);

        }




        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _audioSource = GetComponent<AudioSource>();


        if (_spawnManager ==null)
        {
            Debug.LogError("The spawnmanager has not been found by the player.");
        }

        if(_uiManager ==null)
        {
            Debug.LogError("The UIManager is null .");

        }

        if(_audioSource == null)
        {
            Debug.LogError("The audiosource on the player is null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    void Update()
    {
        if (_isPlayerOne == true)
        {

             PlayerOneMovement();

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))  && Time.time > _canfire && _isPlayerOne == true)
            {
                FireLaserPlayerOne();

            }
        }


        if(_isPlayerTwo == true)
        {
            PlayerTwoMovement();


            if (Input.GetKeyDown(KeyCode.Keypad0) && Time.time > _canfire && _isPlayerTwo == true)
            {
                FireLaserPlayerOne();

            }

        }
        
    }

    void PlayerOneMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        
            transform.Translate(direction * _speed * Time.deltaTime);
        
       /* if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        else if (transform.position.y <= -4.96f)
        {
            transform.position = new Vector3(transform.position.x, -4.96f, 0);
        }*/

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.96f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void PlayerTwoMovement()
    {

        if (Input.GetKey(KeyCode.Keypad8))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Keypad6))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.Keypad5))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Keypad4))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.96f, 0), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaserPlayerOne()
    {
        _canfire = _firerate + Time.time;
        if (_isTripleShotActive == true)
        {
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
         Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }


        _lives--;

        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if(_lives <1)
        {
            _spawnManager.OnPlayerDeath();
            _uiManager.CheckForBestScore(_score);
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);

    }

    public void AddScore ( int points)
    {
        _score = _score+ points;
        _uiManager.UpdateScore(_score);
       
    }
}
