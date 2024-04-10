using UnityEngine;
/// <summary>
/// Simple target script
/// </summary>
public class TargetScript : MonoBehaviour
{
    public EnemyData enemyData;
    public float health;

    void Start() {
        health = enemyData.health;
    }
    public void TakeDamage(float damage)
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
