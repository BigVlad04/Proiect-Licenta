using TMPro;
using UnityEngine;

public class ZombiesLeft : MonoBehaviour
{
    public GameObject Allzombies;
    public TextMeshProUGUI zombiesText;
    void Update()
    {
        zombiesText.text = CountZombies().ToString();
    }

    int CountZombies()      //maybe make this an IEnumerable and call it every .1 or .2 seconds?
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
    }
}
