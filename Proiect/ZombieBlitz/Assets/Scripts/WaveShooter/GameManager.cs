using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject YouDiedText;
    public GameObject YouWinText;


    public IEnumerator GameOver()
    {
        GameObject.Find("Crosshair").SetActive(false);
        FindAnyObjectByType<PlayerMovement>().enabled = false;
        YouDiedText.SetActive(true);
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StaticValues.wavesSurvived = FindAnyObjectByType<WaveSpawner>().getWaveNumber()-1;
        StaticValues.zombiesKilled = FindAnyObjectByType<PlayerData>().getZombiesKilled();
        SceneManager.LoadScene("GameOver");
    }

    public IEnumerator GameWon() 
    {
        GameObject.Find("Crosshair").SetActive(false);
        YouWinText.SetActive(true);
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StaticValues.zombiesKilled = FindAnyObjectByType<PlayerData>().getZombiesKilled();
        SceneManager.LoadScene("WinScreen");
    }
}
