using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{ 
    [SerializeField]
      private float _speed = 3.0f;

    [SerializeField] // powerup ids are tripleshot =0, speed =1 , shields = 2
    private int _PowerupID;

    [SerializeField]
    private AudioClip _clip;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {

            Player _player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

                if (_player != null)
            {
                switch(_PowerupID)
                {
                    case 0:
                        _player.TripleShotActive();
                        break;

                    case 1:
                        _player.SpeedBoostActive();
                        break;

                    case 2:
                        _player.ShieldsActive();
                        break;


                    default:
                        Debug.Log("Nothing is collected default ");
                        break;

                }
            
            }

            Destroy(this.gameObject);
        }
    }
}
