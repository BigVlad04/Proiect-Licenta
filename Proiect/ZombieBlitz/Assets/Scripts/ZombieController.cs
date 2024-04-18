using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public EnemyData enemyData;
    NavMeshAgent agent;
    Transform target;
    Animator animator;

    public float health;        //should make private with a getter
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
        else if (targetInSight && !targetInRange)
        {
            ChasePlayer();
        }
        else if (targetInSight && targetInRange) Attack();
    }

    public void ChasePlayer()
    {
        animator.SetBool("CHASING", true);
        animator.SetBool("ATTACKING", false);
        agent.SetDestination(target.position);
    }

    public void Attack()
    {
        FaceTarget();
        animator.SetBool("ATTACKING", true);
        if (canAttack)
        {
            //Attack, apply damage to player.
            canAttack = false;
            Invoke(nameof(ResetAttack), enemyData.timeBetweenAttacks);
        }
    }
    void FaceTarget()
    {
        if(health > 0)      //won't face target while dead
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
        BoxCollider[] colliders = gameObject.GetComponentsInChildren<BoxCollider>();  
        foreach (BoxCollider collider in colliders)
        { 
            collider.enabled = false;
        }
        if (Random.value > 0.5f)
            animator.SetTrigger("DEATHFORWARD");
        else
            animator.SetTrigger("DEATHBACKWARD");
       // GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(gameObject, 6f);
    }
}
