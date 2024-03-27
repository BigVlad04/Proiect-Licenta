using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    public float health = 30f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Health left: " + health);    //for debugging
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
