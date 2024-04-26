using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene("WaveShooter");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame!");
        Application.Quit();
    }
}
