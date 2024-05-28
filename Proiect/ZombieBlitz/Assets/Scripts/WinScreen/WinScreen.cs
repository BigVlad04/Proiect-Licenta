using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
