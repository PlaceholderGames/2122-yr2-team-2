using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject collectible;

    public Transform[] spawnPoints;

    public int spawnAmount;
    public int spawnCap;

    public bool canSpawn;

    void Start()
    {
        canSpawn = true;  
    }

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
            Instantiate(collectible, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            spawnAmount++;
        }
    }
}
