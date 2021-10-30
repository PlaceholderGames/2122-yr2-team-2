using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public Transform[] spawnPoints;

    public int spawnAmount;
    public int spawnCap;

    public bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnAmount == spawnCap)
        {
            canSpawn = false;
        }
        Spawn();
    }

    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        if(canSpawn == true)
        {
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            spawnAmount++;
        }
    }
}
