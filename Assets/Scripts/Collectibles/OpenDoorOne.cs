using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOne : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if(GameVariables.theScore == 1)
        {
            GameObject.Find("HordeRespawnPoint(1)").GetComponent<RespawnEnemy>().spawnEnemy();
            GameObject.Find("HordeRespawnPoint(2)").GetComponent<RespawnEnemy>().spawnEnemy();
            GameObject.Find("HordeRespawnPoint(3)").GetComponent<RespawnEnemy>().spawnEnemy();
            Destroy(gameObject);
        }
        
    }
}
