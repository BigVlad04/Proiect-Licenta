using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    float startHealth= 100;
    float currentHealth;
    int zombiesKilled= 0;
    bool playerAlive = true;
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
        gameObject.GetComponent<PlayerSounds>().playerHitSound();
        currentHealth -= damage;
        Debug.Log("You took " + damage + " damage. Health remaining: " + currentHealth);
        if (currentHealth < 0)
        {
            Die();
        }
    }
    void Die() {
        Debug.Log("You Died!");
        if(playerAlive)
        {
            playerAlive = false;
            StartCoroutine(FindObjectOfType<GameManager>().GameOver());
        }
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
