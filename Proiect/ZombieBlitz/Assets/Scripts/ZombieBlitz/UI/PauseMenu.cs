using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public  static bool isGamePaused = false;
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame() 
    {
        pauseMenu.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void PauseGame() 
    {
        isGamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game");
    }
}
