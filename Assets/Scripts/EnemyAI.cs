
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Damage the enemy does
    [SerializeField] public int damageDone = 50;

    //Time between attacks
    //[SerializeField] private float timerToAttack = 3f; //Random Timer.
    //private float time = 1f;
    //private bool canAttack = false;
    //private bool playerTouched = false;

    [SerializeField] private float setWaitTime = 3f;
    private float waitTime = 3f;

    GameObject rb = null;

    public float lockPos = 0;

    private void Awake()
    {

        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();


    }

    
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);

    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            rb.GetComponent<Animator>().SetBool("isWalking", false);
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            rb.GetComponent<Animator>().SetBool("isWalking", true);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            rb.GetComponent<Animator>().SetBool("isWalking", false);
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        rb.GetComponent<Animator>().SetBool("isWalking", true);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            
            

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void OnTriggerStay(Collider other)
    {

        if (other.transform.name == "Player")
        {

            waitTime -= Time.deltaTime;

            if (waitTime < 0)
            {
                other.GetComponent<PlayerController>().takeDamage(damageDone);

                waitTime = setWaitTime;
            }
        }
    }


}
