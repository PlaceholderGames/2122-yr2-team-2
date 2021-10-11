using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questItemSpawner : MonoBehaviour
{
    // GameObject class for the item to be spawned
    public GameObject questItem;

    // This will let us choose physical spawn points for our objects from the editor
    public Transform[] spawnPoints;

    // Integer variable for tracking the amount of objects to spawn / that have spawned
    public int spawnAmount, spawnCap;

    // boolean variable to check wether we can spawn objects or not
    public bool canSpawn;

    // Integer variable to choose a random spawn location from the spawn points provided.
    public int spawnPointIndex;

    void Start()
    {
        canSpawn = true; // No reason objects shouldn't be able to spawn at the start of the game
    }

    void Update()
    {
        if (spawnAmount == spawnCap) // Checks if the amount of objects spawned has reached the limit
        {
            canSpawn = false; // Stops more objects spawning if the limit has been reached
        }

        spawnPointIndex = Random.Range(0, spawnPoints.Length); // Assigns the spawnPointIndex a random number between 0 and the amount of potential spawn points we have.
                                                               // This allows quest items to potentially be in a different place each time.

        if (canSpawn == true) // Checks if the canSpawn boolean is true
        {
            Instantiate(questItem, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation); // Spawns the questItem at the spawn point listed by the index

            spawnAmount++; // Increases the amount of spawned objects by 1
        }
    }
}
