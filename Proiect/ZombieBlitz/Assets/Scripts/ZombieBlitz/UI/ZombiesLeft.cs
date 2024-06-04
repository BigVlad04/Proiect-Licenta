using TMPro;
using UnityEngine;
/// <summary>
/// Display the number of zombies alive in the level
/// </summary>
public class ZombiesLeft : MonoBehaviour
{
    public GameObject Allzombies;
    public TextMeshProUGUI zombiesLeftText;

    private void Start()
    {
        zombiesLeftText.text = Allzombies.GetComponent<ZombieCounter>().getZombiesLeft().ToString();
    }
    void Update()
    {
        zombiesLeftText.text = Allzombies.GetComponent<ZombieCounter>().getZombiesLeft().ToString();

    }
}