using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script manages information about the player
/// </summary>
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

    public void takeDamage(float damage)
    {
        gameObject.GetComponent<PlayerSounds>().playerHitSound();
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die() {
        if(playerAlive)     //this is to make sure we don't call GameOver more than once.
        {
            playerAlive = false;
            StartCoroutine(FindObjectOfType<GameManager>().GameOver());
        }
    }

    public float getHealth()
    {
        return currentHealth;
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
