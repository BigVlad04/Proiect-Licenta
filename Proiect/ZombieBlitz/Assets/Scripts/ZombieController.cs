using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public float detectionRange;
    Transform target;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;

    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= detectionRange)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
                FaceTarget();
        }
    }

    void FaceTarget() { 
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime*5f); 
    }
}
