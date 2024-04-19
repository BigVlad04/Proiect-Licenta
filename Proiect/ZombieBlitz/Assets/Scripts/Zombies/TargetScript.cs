using UnityEngine;
/// <summary>
/// Simple target script. !!Not used anymore!!
/// </summary>
public class TargetScript : MonoBehaviour
{
    public EnemyData enemyData;
    public float health;
    public Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        health = enemyData.health;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        //Debug.Log("Health left: " + health);    //for debugging
        if (health <= 0)
        {
            if (Random.value > 0.5f)
                animator.SetTrigger("DEATHFORWARD");
            else
                animator.SetTrigger("DEATHBACKWARD");
            DestroyTarget();
        }
    }
    void DestroyTarget()
    {
        Destroy(gameObject,4f);
    }
}
