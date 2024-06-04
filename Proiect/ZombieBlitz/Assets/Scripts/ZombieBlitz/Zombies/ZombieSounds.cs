using System.Collections;
using UnityEngine;
public class ZombieSounds : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] zombieHitSound;
    public AudioClip[] zombieFootstepSound;
    public AudioClip[] growlSound;
    public float timeBetweenGrowls;
    public float growlProbability;
    float timer;        //used to count the time between growls
    bool footstepSoundPlaying = false;      // to make sure we don't overlap footstep sounds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = 0;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeBetweenGrowls)
        {
            timer -= timeBetweenGrowls;
            if(Random.value < growlProbability)     // to make zombie growls more random
                playGrowlSound();
        }
    }
    public void playZombieHitSound()
    {
        audioSource.PlayOneShot(zombieHitSound[Random.Range(0, zombieHitSound.Length)]);        //play a random zombie hit sound from the selection
    }
    void playGrowlSound()
    {
        AudioClip growl = growlSound[Random.Range(0, growlSound.Length)];   //pick a random growl sound
        audioSource.pitch = Random.Range(0.7f, 1f);     //pick a random pitch
        audioSource.PlayOneShot(growl);
    }
    public IEnumerator footstepSound()
    {
        if (!footstepSoundPlaying)      //if we are not already playing footstep sound
        {
            footstepSoundPlaying = true;        
            AudioClip footstepSound = zombieFootstepSound[Random.Range(0, zombieFootstepSound.Length)];
            audioSource.volume = .5f;
            audioSource.pitch = Random.Range(0.8f, 1f);
            audioSource.PlayOneShot(footstepSound);
            yield return new WaitForSeconds(footstepSound.length);      //wait for the footstep sound to finish playing then set footstepSoundPlaying to false
            footstepSoundPlaying = false;
        }
        else        //if we are already playing a footstep sound do nothing
        {
            yield return null;
        }
    }
}