using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// this script implements the functionality of the the settings menu
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
