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
    float health;
    float distanceToPlayer;
    bool targetInSight;
    bool targetInRange;
    bool attackStarted;
    float attackEndTime;
    bool isAlive;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        health = enemyData.health;
        isAlive = true;
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
        BehaviourAlwaysChase();
        //BehaviourChaseWhenPlayerInSight();
    }

    void BehaviourAlwaysChase()     //zombies always chase the player
    {
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        targetInRange = (distanceToPlayer <= enemyData.attackRange);
        if (health > 0)
        {
            if (!targetInRange)
            {
                ChasePlayer();
            }
            else
            {
                Attack();
            }
        }
    }

    void BehaviourChaseWhenPlayerInSight()      //zombies chase only when the player is within a certain distance
    {
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        targetInSight = (distanceToPlayer <= enemyData.detectionRange);
        targetInRange = (distanceToPlayer <= enemyData.attackRange);
        if (health > 0)
        {
            if (!targetInSight && !targetInRange)       //this will be disabled in the final game, the zombies will always chase the player
            {
                agent.SetDestination(transform.position);
                animator.SetBool("CHASING", false);
            }
            else if (targetInSight && !targetInRange)
            {
                ChasePlayer();
            }
            else if (targetInSight && targetInRange)
            {
                Attack();
            }
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(target.position);
        animator.SetBool("CHASING", true);
        animator.SetBool("ATTACKING", false);
        StartCoroutine(GetComponent<ZombieSounds>().footstepSound());
        attackStarted = false;
    }

    void Attack()
    {
        if (attackStarted == false)
        {
            attackStarted = true;
            animator.SetBool("ATTACKING", true);
            attackEndTime = Time.time + enemyData.attackDuration;
        }
        else
        {
            if (Time.time > attackEndTime)
            {
                target.GetComponent<PlayerData>().takeDamage(enemyData.damage);
                attackStarted=false;
            }
        }
        FaceTarget();
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        gameObject.GetComponent<ZombieSounds>().playZombieHitSound();
        if (health <= 0)
        {
            if (isAlive)    //the zombie is only deleted a few seconds after health reaches 0, time in which the player can still shoot the zombie, so we need isAlive to make sure we only call Death() once
            {
                Death();
                isAlive = false;
            }

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
        target.GetComponent<PlayerData>().increaseZombieKilled();
    }

    public float GetHealth()
    {
        return health;
    }
}
