using System.Collections;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// This script controlls the behaviour and functionality of a zombie
/// </summary>
public class ZombieController : MonoBehaviour
{
    public EnemyData enemyData;
    NavMeshAgent agent;
    Animator animator;
    Transform target;
    private float health;
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
        if (!targetInSight && !targetInRange)       //this will be disabled in the final game, the zombies will always chase the player
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
            target.GetComponent<PlayerData>().takeDamage(enemyData.damage);
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
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        agent.isStopped = true;
        BoxCollider[] colliders = gameObject.GetComponentsInChildren<BoxCollider>();  
        /*foreach (BoxCollider collider in colliders)     //disable collider so that player can't shoot the zombie while dead. Optional
        { 
            collider.enabled = false;
        }*/
        if (Random.value > 0.5f)    //randomly fall forward or backwards
            animator.SetTrigger("DEATHFORWARD");
        else
            animator.SetTrigger("DEATHBACKWARD");
        Destroy(gameObject, 6f);    //destroy the zombie after 6 seconds.
    }

    public float GetHealth()
    {
        return health;
    }
}
