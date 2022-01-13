using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorThree : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (GameVariables.theScore == 3)
        {
            GameObject.Find("HordeRespawnPoint(7)").GetComponent<RespawnEnemy>().spawnEnemy();
            GameObject.Find("HordeRespawnPoint(8)").GetComponent<RespawnEnemy>().spawnEnemy();
            GameObject.Find("HordeRespawnPoint(9)").GetComponent<RespawnEnemy>().spawnEnemy();
            Destroy(gameObject);
        }

    }
}
