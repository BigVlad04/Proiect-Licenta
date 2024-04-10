using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public EnemyData enemyData;
    NavMeshAgent agent;
    Transform target;
    Animator animator;

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
            //Attack, apply damage to player.
            Debug.Log("Attacking!");
            canAttack = false;
            Invoke(nameof(ResetAttack), enemyData.timeBetweenAttacks);
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    public void TakeDamage(float damage) //when shot.
    {
        enemyData.health -= damage;
        //Debug.Log("Health left: " + health);    //for debugging
        if (enemyData.health <= 0)
        {
            DestroyTarget();
        }
    }
    void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
