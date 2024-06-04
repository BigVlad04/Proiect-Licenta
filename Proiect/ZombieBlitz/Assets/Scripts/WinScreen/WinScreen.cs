using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script runs when the player is presented with the win screen
/// </summary>
public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI zombiesKilled;
    public void Start()
    {
        zombiesKilled.text = "Zombies Killed : " + StaticValues.zombiesKilled;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("ZombieBlitz");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
