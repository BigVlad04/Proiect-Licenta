using TMPro;
using UnityEngine;
/// <summary>
/// Display how many zombies the player has killed so far
/// </summary>
public class ZombiesKilled : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI enemiesKilledText;
    void Update()
    {
        enemiesKilledText.text = "x " + player.GetComponent<PlayerData>().getZombiesKilled().ToString();
    }
}