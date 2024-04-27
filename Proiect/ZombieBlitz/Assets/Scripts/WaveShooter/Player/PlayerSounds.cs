using System.Collections;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    bool soundPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator footstepSound()
    {
        if (!soundPlaying)
        {
            soundPlaying = true;
            AudioClip footstepSound = footstepSounds[Random.RandomRange(0, footstepSounds.Length)];
            audioSource.volume = .3f;
            audioSource.pitch = Random.Range(0.8f, 1f);
            audioSource.PlayOneShot(footstepSound);
            yield return new WaitForSeconds(footstepSound.length);
            soundPlaying = false;
        }
        else
        {
            yield return null;
        }
    }
}
