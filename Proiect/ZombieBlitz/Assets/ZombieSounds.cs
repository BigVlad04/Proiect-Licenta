using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] zombieHitSound;
    public AudioClip[] zombieFootstepSound;
    public AudioClip[] growlSound;
    public float timeBetweenGrowls;
    public float growlProbability;
    float timer;
    bool footstepSoundPlaying = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = 0;
    }

    public void playZombieHitSound()
    {
        audioSource.PlayOneShot(zombieHitSound[Random.Range(0, zombieHitSound.Length)]);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeBetweenGrowls)
        {
            timer -= timeBetweenGrowls;
            if(Random.value < growlProbability)
                playGrowlSound();
        }
    }

    void playGrowlSound()
    {
        AudioClip growl = growlSound[Random.Range(0, growlSound.Length)];
        audioSource.pitch = Random.Range(0.7f, 1f);
        audioSource.PlayOneShot(growl);
    }

    public IEnumerator footstepSound()
    {
        if (!footstepSoundPlaying)
        {
            footstepSoundPlaying = true;
            AudioClip footstepSound = zombieFootstepSound[Random.Range(0, zombieFootstepSound.Length)];
            audioSource.volume = .5f;
            audioSource.pitch = Random.Range(0.8f, 1f);
            audioSource.PlayOneShot(footstepSound);
            yield return new WaitForSeconds(footstepSound.length);
            footstepSoundPlaying = false;
        }
        else
        {
            yield return null;
        }
    }
}
