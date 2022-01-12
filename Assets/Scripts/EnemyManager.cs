using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    private float waitTime = 15f;
    private bool spawning = false;


    // Start is called before the first frame update
    void Start()
    {
        //SpawnNewEnemy();
        EnemyAI.OnEnemyKilled += enemyDied;
    }

    private void Update()
    {
        
        if (spawning)
        {
            waitTime -= Time.deltaTime;
            print(waitTime);
        }
        
        if (waitTime <= 0.0f)
        {
            if (spawning)
            {
                print("Spawning enemy");
                spawning = false;
                waitTime = 15f;
                //Instantiate(enemyPrefab, spawnPoints[0].transform.position, Quaternion.identity);
                SpawnNewEnemy();
                EnemyAI.OnEnemyKilled += enemyDied;
                
            }
        }
    }

    void OnEnable()
    {
        
    }

    void enemyDied()
    {
        spawning = true;
    }
    void SpawnNewEnemy()
    {
        Instantiate(enemyPrefab, spawnPoints[0].transform.position, Quaternion.identity);
    }
}
