using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject YouDiedText;
    public GameObject YouWinText;

    public IEnumerator GameOver()
    {
        GameObject.Find("Crosshair").SetActive(false);
        FindAnyObjectByType<PlayerMovement>().enabled = false;      
        YouDiedText.SetActive(true);
        yield return new WaitForSeconds(5);             //display "You Died!" text for 5 seconds, then change to the game over screen
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StaticValues.wavesSurvived = FindAnyObjectByType<WaveSpawner>().getWaveNumber()-1;      //these values are needed in the game over screen so they are stored in static values
        StaticValues.zombiesKilled = FindAnyObjectByType<PlayerData>().getZombiesKilled();      //
        SceneManager.LoadScene("GameOver");
    }

    public IEnumerator GameWon() 
    {
        GameObject.Find("Crosshair").SetActive(false);
        YouWinText.SetActive(true);         
        yield return new WaitForSeconds(5);         //display "You Win!" for 5 seconds, then change to win screen
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StaticValues.zombiesKilled = FindAnyObjectByType<PlayerData>().getZombiesKilled();
        SceneManager.LoadScene("WinScreen");
    }
}
