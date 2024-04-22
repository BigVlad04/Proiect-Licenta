using TMPro;
using UnityEngine;

public class ZombiesKilled : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI enemiesKilledText;
    void Update()
    {
        enemiesKilledText.text = "x " + player.GetComponent<PlayerData>().getZombiesKilled().ToString();
    }
}
