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

    // Use this for initialization
    void Start()
    {
        //If you want, add this line:
        Death = false;
        this.gameObject.name = EnemyName + "spawn point";
        spawnEnemy();
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

    void spawnEnemy()
    {
        //It will create a new Enemy of the same class, at this position.
        Enemy.transform.position = transform.position;

        Instantiate(Enemy);
        LastEnemy = GameObject.Find(Enemy.name + "(Clone)");
        LastEnemy.name = EnemyName;
        //My enemy won't be dead anymore.
        Death = false;
        //Timer will restart.
        Timer = 0;
    }
}
