using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] zombieHitSound;
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
}
