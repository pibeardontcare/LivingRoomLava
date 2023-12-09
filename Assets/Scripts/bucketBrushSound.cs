using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketBrushSound : MonoBehaviour
{
    public AudioClip paintCollisionSound; // Sound to play when "paint" object collides
    public float minTimeBetweenSounds = 3f; // Minimum time between playing sounds

    private AudioSource audioSource;
    private float lastSoundTime;

    void Start()
    {
        // Add an AudioSource component to the GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        lastSoundTime = -minTimeBetweenSounds; // Set initial value to allow sound to play immediately
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the tag "paint"
        if (collision.gameObject.CompareTag("paint"))
        {
            // Check if enough time has passed since the last sound was played
            if (Time.time - lastSoundTime >= minTimeBetweenSounds)
            {
                // Play the paint collision sound
                if (audioSource != null && paintCollisionSound != null)
                {
                    audioSource.PlayOneShot(paintCollisionSound);
                    lastSoundTime = Time.time; // Update the last sound time
                }
            }
        }
    }
}
