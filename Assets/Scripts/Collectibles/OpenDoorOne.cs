using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOne : MonoBehaviour
{
    [SerializeField] GameObject door;
    void OnTriggerEnter(Collider other)
    {
        door = GameObject.Find("DoorOne");
        if (GameVariables.theScore == 1)
        {
            GameObject.Find("HordeRespawnPoint(1)").GetComponent<RespawnEnemy>().spawnEnemy();
            GameObject.Find("HordeRespawnPoint(2)").GetComponent<RespawnEnemy>().spawnEnemy();
            GameObject.Find("HordeRespawnPoint(3)").GetComponent<RespawnEnemy>().spawnEnemy();
            Destroy(door);
            //Destroy(gameObject);
        }
        
    }
}
