using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHitDetection : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;

    public float attackRate = 2f;
    //private float setWaitTime = 1f;
    private float waitTime = 1f;

    // Update is called once per frame
    void Update()
    {
        
        waitTime -= Time.deltaTime;
        //print("waitTime" + waitTime);
        
        if (waitTime <= 0.0f)
        {
            //print("Can attack");
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                waitTime = 1f;
                print("Attack 1!");
                //nextAttackTime = Time.time + 1f / attackRate;
                Attack();
                
            }
        }
    }

    //This is a tad buggy, doesn't allways attack the enemy because it's only damaging when it hits a CapsuleCollider
    //but there is a sphere collider that keeps intercepting the hit!
    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            print("Enemy # "+ i + ": " + hitEnemies[i] + "\n");
        }

        

        foreach(Collider enemy in hitEnemies)
        {
            if (enemy is CapsuleCollider)
            {
                enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
                print("Attack 2!!   " + attackDamage);
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }


        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public int getDamage()
    {
        return attackDamage;
    }

    public void setDamage(int level)
    {
        attackDamage += level;
    }
}
