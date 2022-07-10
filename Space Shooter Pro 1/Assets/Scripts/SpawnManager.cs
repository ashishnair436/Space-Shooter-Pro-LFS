using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _seconds = 3.5f;
    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] _powerups;
    void Start()
    {
       
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 7f, 0);
           GameObject newEnemy = Instantiate(_enemyprefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_seconds);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(2f);
        while (_stopSpawning ==false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9f, 9f), 7f, 0);
            int randompowerup = Random.Range(0, 3);
            Instantiate(_powerups[randompowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(4, 9));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;

    }
}
