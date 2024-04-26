using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    public GameObject waveSpawner;
    public TextMeshProUGUI waveNumberText;

    // Update is called once per frame
    void Update()
    {
        waveNumberText.text = "Wave: " + waveSpawner.GetComponent<WaveSpawner>().getWaveNumber().ToString();
    }
}
