using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This script displays the health bar
/// </summary>
public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public Slider healthbar;

    void Update()
    {
        healthbar.value = player.GetComponent<PlayerData>().getHealth();
    }
}