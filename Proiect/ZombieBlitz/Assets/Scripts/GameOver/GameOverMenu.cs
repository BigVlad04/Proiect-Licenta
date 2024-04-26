using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void PlayAgain() 
    {
        SceneManager.LoadScene("WaveShooter");
    }

    public void BackToMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
