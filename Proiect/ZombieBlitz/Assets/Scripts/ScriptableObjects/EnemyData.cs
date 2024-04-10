
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Zombie")]
public class EnemyData : ScriptableObject
{
    public string zombieName;
    public float health;
    public float detectionRange;
    public float attackRange;
    public float timeBetweenAttacks;
}
