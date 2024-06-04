using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script is run in the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene("ZombieBlitz");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
