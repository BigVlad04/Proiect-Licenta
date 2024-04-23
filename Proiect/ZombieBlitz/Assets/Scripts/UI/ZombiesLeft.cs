using System.Collections;
using TMPro;
using UnityEngine;
/// <summary>
/// Display the number of zombies alive in the level
/// </summary>
public class ZombiesLeft : MonoBehaviour
{
    public GameObject Allzombies;
    public TextMeshProUGUI zombiesLeftText;
    int zombiesLeft;
    bool completedCount = false;

    private void Start()
    {
        StartCoroutine(CountZombies());
    }

    void Update()
    {
        zombiesLeftText.text = zombiesLeft.ToString();
        if (completedCount)
            return;
        else
        {
            StartCoroutine(CountZombies());
            return;
        }

    }

    IEnumerator CountZombies()     //count the zombies every .3 seconds
    {
        int count = 0;
        ZombieController[] zombieControllers = Allzombies.GetComponentsInChildren<ZombieController>();  //get only objects which have a zombie controller script
        foreach (ZombieController zombieController in zombieControllers)
        {
            if (zombieController.GetHealth() > 0)   //zombie game objects still exist a few seconds after reaching 0 health, to allow for the death animation to play.
            {                       
                count++;                            //thus we only count zombies who have more than 0 health as alive
            }
        }
        zombiesLeft = count;
        completedCount = true;
        yield return new WaitForSeconds(.3f);       //will only count again after .3 seconds, as performing the count every frame would be redundant.
        completedCount = false;
    }
}
