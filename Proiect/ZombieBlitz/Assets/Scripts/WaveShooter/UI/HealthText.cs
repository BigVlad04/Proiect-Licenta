using TMPro;
using UnityEngine;
/// <summary>
/// Display player health as a text. Not used in the game anymore
/// </summary>
public class Health : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI healthText;

    void Update()
    {
        healthText.text = "Health: " + player.GetComponent<PlayerData>().getHealth().ToString();
    }
}
