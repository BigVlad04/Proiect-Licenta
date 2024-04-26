using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StaticValues.wavesSurvived = FindAnyObjectByType<WaveSpawner>().getWaveNumber()-1;
        StaticValues.zombiesKilled = FindAnyObjectByType<PlayerData>().getZombiesKilled();
        SceneManager.LoadScene("GameOver");
    }
}
