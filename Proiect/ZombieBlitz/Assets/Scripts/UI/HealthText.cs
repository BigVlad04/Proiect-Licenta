using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI healthText;

    void Update()
    {
        healthText.text = "Health: " + player.GetComponent<PlayerData>().getHealth().ToString();
    }
}
