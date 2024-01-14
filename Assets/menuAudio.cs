using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuAudio : MonoBehaviour
{
     private AudioSource audioSource;

    // Set the delay time in seconds
    private float delayTime = 3f;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if AudioSource is present
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject.");
        }
        else
        {
            // Invoke the method to play the delayed audio after the specified delay time
            Invoke("PlayDelayedAudio", delayTime);
        }
    }

    void PlayDelayedAudio()
    {
        // Play the audio with the delay
        audioSource.Play();
    }
}
