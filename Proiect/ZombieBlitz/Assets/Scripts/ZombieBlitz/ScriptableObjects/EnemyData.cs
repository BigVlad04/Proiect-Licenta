using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Zombie")]
public class EnemyData : ScriptableObject
{
    //Zombie properties
    public string zombieName;
    public float health;
    public float damage;
    public float detectionRange;
    public float attackRange;
    public float attackDuration;

    //NavMesh properties
    public float speed;
    public float angularSpeed;
    public float acceleration;
    public float stoppingDistance;
}
