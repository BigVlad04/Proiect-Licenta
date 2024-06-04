using TMPro;
using UnityEngine;
/// <summary>
/// Display the wave number
/// </summary>
public class WaveText : MonoBehaviour
{
    public GameObject waveSpawner;
    public TextMeshProUGUI waveNumberText;

    void Update()
    {
        waveNumberText.text = "Wave: " + waveSpawner.GetComponent<WaveSpawner>().getWaveNumber().ToString();
    }
}