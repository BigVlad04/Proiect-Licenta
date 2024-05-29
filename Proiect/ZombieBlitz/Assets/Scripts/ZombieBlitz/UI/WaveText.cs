using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    public GameObject waveSpawner;
    public TextMeshProUGUI waveNumberText;

    void Update()
    {
        waveNumberText.text = "Wave: " + waveSpawner.GetComponent<WaveSpawner>().getWaveNumber().ToString();
    }
}
