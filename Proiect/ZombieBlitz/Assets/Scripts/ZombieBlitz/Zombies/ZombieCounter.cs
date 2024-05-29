using System.Collections;
using UnityEngine;
/// <summary>
/// Used for counting the number of ALIVE zombies in the game
/// </summary>

public class ZombieCounter : MonoBehaviour
{
    int zombiesLeft;
    bool completedCount = false;
    private void Start()
    {
        StartCoroutine(CountZombies());
    }

    void Update()
    {
        if (completedCount)
            return;
        else
        {
            StartCoroutine(CountZombies());
            return;
        }
    }
    public int getZombiesLeft()
    {
        return zombiesLeft;
    }

    IEnumerator CountZombies()     //count the zombies every .3 seconds
    {
        int count = 0;
        ZombieController[] zombieControllers = GetComponentsInChildren<ZombieController>();  //get only objects which have a zombie controller script
        foreach (ZombieController zombieController in zombieControllers)
        {
            if (zombieController.GetHealth() > 0)   //zombie game objects still exist a few seconds after reaching 0 health, to allow for the death animation to play.
            {
                count++;                            //thus we only count zombies who have more than 0 health as alive
            }
        }
        zombiesLeft = count;
        completedCount = true;
        yield return new WaitForSeconds(.3f);       //will only count again after .3 seconds, as performing the count every frame would be unoptimal.
        completedCount = false;
    }


}
