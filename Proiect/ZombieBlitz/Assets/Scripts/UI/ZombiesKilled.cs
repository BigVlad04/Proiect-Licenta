using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombiesKilled : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI enemiesKilledText;
    void Update()
    {
        enemiesKilledText.text = ": " + player.GetComponent<PlayerData>().getZombiesKilled().ToString();
    }
}
