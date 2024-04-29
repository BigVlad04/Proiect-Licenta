using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] zombieHitSound;
    public AudioClip zombieFootstep;
    bool footstepSoundPlaying = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playZombieHitSound()
    {
        audioSource.PlayOneShot(zombieHitSound[Random.Range(0, zombieHitSound.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator footstepSound()
    {
        if (!footstepSoundPlaying)
        {
            footstepSoundPlaying = true;
            audioSource.volume = .5f;
            audioSource.pitch = Random.Range(0.8f, 1f);
            audioSource.PlayOneShot(zombieFootstep);
            yield return new WaitForSeconds(zombieFootstep.length);
            footstepSoundPlaying = false;
        }
        else
        {
            yield return null;
        }
    }
}
