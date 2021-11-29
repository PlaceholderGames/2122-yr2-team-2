using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHitDetection : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    private float setWaitTime = 1f;
    private float waitTime = 1f;
    private int attackCount = 0;

    // Update is called once per frame
    void Update()
    {
        
        waitTime -= Time.deltaTime;
        //print("waitTime" + waitTime);
        
        if (waitTime <= 0.0f)
        {
            attackCount = 0;
            //print("Can attack");
            if (Input.GetKeyDown(KeyCode.Mouse0) && waitTime <= 0.0f && attackCount < 1)
            {
                attackCount++;
                print("attack count: " + attackCount);
                waitTime = 1f;
                print("Attack 1!");
                nextAttackTime = Time.time + 1f / attackRate;
                Attack();
                
            }
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
            print("Attack 2!!   " + attackDamage);
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
}
