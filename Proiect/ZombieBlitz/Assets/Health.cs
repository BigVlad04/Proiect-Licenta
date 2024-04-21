using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.GetComponent<PlayerData>().getHealth().ToString();
    }
}
