using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script runs when the game over screen is displayed.
/// </summary>
public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI wavesSurvived;
    public TextMeshProUGUI zombiesKilled;
    public void Start()
    {
        wavesSurvived.text = "You survived for " + StaticValues.wavesSurvived + " waves";
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
