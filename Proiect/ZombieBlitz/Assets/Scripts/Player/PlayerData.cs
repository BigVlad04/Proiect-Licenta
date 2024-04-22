using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    float startHealth= 100;
    float currentHealth;
    int zombiesKilled= 0;
    void Start()
    {
        currentHealth= startHealth;
    }

    public float getHealth() 
    { 
        return currentHealth;
    }
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("You took " + damage + " damage. Health remaining: " + currentHealth);
        if (currentHealth < 0)
        {
            Die();
        }
    }
    void Die() {
        Debug.Log("You Died!");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public int getZombiesKilled()
    {
        return zombiesKilled;
    }
    public void increaseZombieKilled()
    {
        zombiesKilled++;
    }
}
