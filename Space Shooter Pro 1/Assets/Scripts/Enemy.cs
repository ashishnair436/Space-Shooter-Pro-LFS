using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemyspeed = 4.0f;

    [SerializeField]
    private GameObject _laserPrefab;

    private Player _player;

    private Animator _anim;

    private AudioSource _audioSource;

    private float _fireRate = 3.0f;
    private float _canFire = -1;


    void Start()
    {

        _player = GameObject.Find("Player1").GetComponent<Player>();

        if (_player == null)
         {
            Debug.LogError("Player Component in enemy script is null");
         }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("Animator is null");
        }

        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.LogError("Audio Source of explosion in enemy is Null.");
        }
        
    }

    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
             GameObject enemyLaser =    Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            LaserMovement[] lasers = enemyLaser.GetComponentsInChildren<LaserMovement>();

            for(int i=0;i< lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();  
            }
            
        }

    }


    void CalculateMovement()
    {

        transform.Translate(Vector3.down * _enemyspeed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            float RandomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(RandomX, 7f, 0);
        }
    } 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _enemyspeed = 0;

            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject , 2.5f);
        }


            if (other.tag == "Laser")
            {
                Destroy(other.gameObject);

                if (_player != null)
                {
                
                   _player.AddScore(100);
                }

            _anim.SetTrigger("OnEnemyDeath");

            _enemyspeed = 0;
            _audioSource.Play(); 
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject , 2.5f );
            }
        
        }
}
