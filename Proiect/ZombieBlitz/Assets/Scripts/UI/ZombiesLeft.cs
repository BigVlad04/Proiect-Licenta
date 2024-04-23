using System.Collections;
using TMPro;
using UnityEngine;

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

   /* int CountZombies()      //maybe make this an IEnumerable and call it every .1 or .2 seconds?
    {
        int count = 0;
        ZombieController[] zombieControllers = Allzombies.GetComponentsInChildren<ZombieController>();
        foreach(ZombieController zombieController in zombieControllers)
        {
            if (zombieController.GetHealth() > 0)
            {
                count++;
            }
        }
        return count;
    }*/

    IEnumerator CountZombies()
    {
        int count = 0;
        ZombieController[] zombieControllers = Allzombies.GetComponentsInChildren<ZombieController>();
        foreach (ZombieController zombieController in zombieControllers)
        {
            if (zombieController.GetHealth() > 0)
            {
                count++;
            }
        }
        zombiesLeft = count;
        completedCount = true;
        yield return new WaitForSeconds(.3f);
        completedCount = false;
    }
}
