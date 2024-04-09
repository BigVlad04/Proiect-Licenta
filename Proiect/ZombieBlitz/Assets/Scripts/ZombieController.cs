using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;

    public float detectionRange; //optional, if we want zombies to follow only when the player gets close

    private float health = 100;
    public float attackRange;
    public float timeBetweenAttacks;    //will need to change these with values taken from a scriptable object

    float distanceToPlayer;
    bool targetInSight; 
    bool targetInRange;
    bool canAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        canAttack = true;
    }
    //will want to automatically set navMeshAgent values from the StartMethod.

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        targetInSight = (distanceToPlayer <= detectionRange);
        targetInRange = (distanceToPlayer <= attackRange);
        if (targetInSight && !targetInRange) ChasePlayer();
        if (targetInSight && targetInRange) Attack();
    }

    public void ChasePlayer()
    {
        agent.SetDestination(target.position);
        if (distanceToPlayer <= agent.stoppingDistance)
            FaceTarget();   //maybe get rid of this and make stopping distance the same as attackrange?
    }

    public void Attack() {
        FaceTarget();
        if (canAttack) {
            //Attack, apply damage to player.
            Debug.Log("Attacking!");
            canAttack = false;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ResetAttack() {
        canAttack = true;
    }

    public void TakeDamage(float damage) //when shot.
    {
        health -= damage;
        //Debug.Log("Health left: " + health);    //for debugging
        if (health <= 0)
        {
            DestroyTarget();
        }
    }
    void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
