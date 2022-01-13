using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTwo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (GameVariables.theScore == 2)
        {
            GameObject.Find("HordeRespawnPoint(4)").GetComponent<RespawnEnemy>().spawnEnemy();
            GameObject.Find("HordeRespawnPoint(5)").GetComponent<RespawnEnemy>().spawnEnemy();
            GameObject.Find("HordeRespawnPoint(6)").GetComponent<RespawnEnemy>().spawnEnemy();
            Destroy(gameObject);
        }

    }
}
