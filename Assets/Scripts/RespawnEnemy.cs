using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    public bool Death;
    public float Timer;
    public float Cooldown;
    public GameObject Enemy;
    public string EnemyName;
    GameObject LastEnemy;
    [SerializeField] int spawnPoint;

    // Use this for initialization
    void Start()
    {
        //If you want, add this line:
        Death = false;
        //this.gameObject.name = EnemyName + "spawn point";
        if(this.EnemyName != "HordeDemon")
        {
            spawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Death == true)
        {
            //If my enemy is death, a timer will start.
            Timer += Time.deltaTime;

        }
        //If the timer is bigger than cooldown.
        if (Timer >= Cooldown)
        {
            spawnEnemy();
        }
    }

    public void spawnEnemy()
    {
        //It will create a new Enemy of the same class, at this position.
        Enemy.transform.position = transform.position;

        Instantiate(Enemy, Enemy.transform.position, Enemy.transform.rotation);

        //L.F Finds the newly spawned demon, then renames it based on where it spawned
        //L.F then it finds the enemy and sets it's spawn point to whatever spawnpoint this script is attached to
        LastEnemy = GameObject.Find(Enemy.name + "(Clone)");
        if(this.EnemyName != "HordeDemon")
        {
            LastEnemy.name = "DemonSP" + spawnPoint;
            LastEnemy.GetComponent<EnemyAI>().setSpawnPoint(spawnPoint);
        }
        else
        {
            LastEnemy.name = "HordeDemonSP" + spawnPoint;
            LastEnemy.GetComponent<EnemyAI>().setSpawnPoint(spawnPoint);
            LastEnemy.GetComponent<EnemyAI>().setDemonType(true);
        }
        
        //My enemy won't be dead anymore.
        Death = false;
        //Timer will restart.
        Timer = 0;
    }
}
