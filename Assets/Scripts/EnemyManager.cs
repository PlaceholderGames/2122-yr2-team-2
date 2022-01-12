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
        SpawnNewEnemy();
    }

    private void Update()
    {
        if(spawning)
        {
            waitTime -= Time.deltaTime;
        }

        if(waitTime <= 0.0f)
        {
            if (spawning)
            {
                EnemyAI.OnEnemyKilled += SpawnNewEnemy;
                spawning = false;
                waitTime = 15f;
            }
        }
    }

    void OnEnable()
    {
        
    }

    void SpawnNewEnemy()
    {
        Instantiate(enemyPrefab, spawnPoints[0].transform.position, Quaternion.identity);
        spawning = true;
    }
}
