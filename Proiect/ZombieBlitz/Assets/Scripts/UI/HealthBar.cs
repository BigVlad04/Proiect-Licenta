using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public Slider healthbar;
    float health;

    void Update()
    {
        health = player.GetComponent<PlayerData>().getHealth();
        healthbar.value = health;
    }
}
