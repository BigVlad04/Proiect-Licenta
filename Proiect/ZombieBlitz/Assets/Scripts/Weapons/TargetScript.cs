using UnityEngine;
/// <summary>
/// Simple target script
/// </summary>
public class TargetScript : MonoBehaviour
{
    public float health = 30f;
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
