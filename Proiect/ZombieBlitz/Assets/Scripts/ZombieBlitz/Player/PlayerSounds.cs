using System.Collections;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    public AudioClip playerHit;
    public AudioClip[] hurtSound;
    bool soundPlaying = false;          //this is used to make sure we don't overlap footstep sounds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playerHitSound()
    {
        audioSource.PlayOneShot(playerHit);
        if(Random.value > 0.5f)     //50% of the time a groan sound is played when the player is hit
        {
            audioSource.PlayOneShot(hurtSound[Random.Range(0, hurtSound.Length)]);      //play a random groan sound from the selection
        }
    }

    public IEnumerator footstepSound()
    {
        if (!soundPlaying)  //if we're not already playing a footstep sound
        {
            soundPlaying = true;
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];   //pick a random footstep sound
            audioSource.volume = .7f;
            audioSource.pitch = Random.Range(0.8f, 1f);     //pick a random pitch
            audioSource.PlayOneShot(footstepSound);
            yield return new WaitForSeconds(footstepSound.length);  //set soundPlaying to false after we wait for the sound to finish playing 
            soundPlaying = false;
        }
        else            //if we're already playing a sound do nothing
        {
            yield return null;
        }
    }
}
