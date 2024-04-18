using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public EnemyData enemyData;
    NavMeshAgent agent;
    Transform target;
    Animator animator;

    public float health;
    float distanceToPlayer;
    bool targetInSight;
    bool targetInRange;
    bool canAttack;


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        canAttack = true;
        health = enemyData.health;
        setNavMeshStats();

    }
    //will want to automatically set navMeshAgent values from the StartMethod.

    void setNavMeshStats()
    {
        agent.speed = enemyData.speed;
        agent.angularSpeed = enemyData.angularSpeed;
        agent.acceleration = enemyData.acceleration;
        agent.stoppingDistance = enemyData.stoppingDistance;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        targetInSight = (distanceToPlayer <= enemyData.detectionRange);
        targetInRange = (distanceToPlayer <= enemyData.attackRange);
        if (!targetInSight && !targetInRange)
        {
            agent.SetDestination(transform.position);
            animator.SetBool("CHASING", false);
        }
        if (targetInSight && !targetInRange)
        {
            ChasePlayer();
        }

        if (targetInSight && targetInRange) Attack();
    }

    public void ChasePlayer()
    {
        animator.SetBool("CHASING", true);
        agent.SetDestination(target.position);
        if (distanceToPlayer <= agent.stoppingDistance)
            FaceTarget();   //maybe get rid of this and make stopping distance the same as attackrange?
    }

    public void Attack()
    {
        FaceTarget();
        if (canAttack)
        {
            animator.SetBool("ATTACKING", true) ;
            //Attack, apply damage to player.
            Debug.Log("Attacking!");
            canAttack = false;
            Invoke(nameof(ResetAttack), enemyData.timeBetweenAttacks);
        }
    }
    void FaceTarget()
    {
        if(health > 0)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void ResetAttack()
    {
        if(!targetInRange)
            animator.SetBool("ATTACKING", false);
        canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log("Health left: " + health);    //for debugging
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        agent.Stop();
        if (Random.value > 0.5f)
            animator.SetTrigger("DEATHFORWARD");
        else
            animator.SetTrigger("DEATHBACKWARD");
       // GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(gameObject, 6f);
    }
}
