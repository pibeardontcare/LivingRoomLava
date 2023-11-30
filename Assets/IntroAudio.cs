using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAudio : MonoBehaviour
{
    public AudioClip audioClip; // Assign your audio clip in the inspector
    public ColliderHandler colliderHandler; // Assign the ColliderHandler in the inspector
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Delay the start of the audio clip by 5 seconds
        StartCoroutine(PlayAudioDelayed(5f));
    }

    IEnumerator PlayAudioDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Start playing the audio clip
        audioSource.clip = audioClip;
        audioSource.Play();

        // Subscribe to the audio finished event
        StartCoroutine(WaitForAudioFinish());
    }

    IEnumerator WaitForAudioFinish()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Audio has finished playing, enable the collider on the other game object
        EnableColliderOnOtherGameObject();
        Debug.Log("Collider ready!");
    }

    void EnableColliderOnOtherGameObject()
    {
        if (colliderHandler != null)
        {
            // Enable the collider 
            colliderHandler.GetComponent<Collider>().enabled = true;
            Debug.Log("Collider ready!");
        }
    }
}
