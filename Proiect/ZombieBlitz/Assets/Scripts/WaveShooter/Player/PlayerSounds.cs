using System.Collections;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    public AudioClip playerHit;
    public AudioClip[] hurtSound;
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

    public void playerHitSound()
    {
        audioSource.PlayOneShot(playerHit);
        if(Random.value > 0.5f)
        {
            audioSource.PlayOneShot(hurtSound[Random.Range(0, hurtSound.Length)]);
        }
    }

    public IEnumerator footstepSound()
    {
        if (!soundPlaying)
        {
            soundPlaying = true;
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.volume = .7f;
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
