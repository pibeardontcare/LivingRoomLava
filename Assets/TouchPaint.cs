using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPaint : MonoBehaviour
{
    public Material beforeCollisionMaterial; // Material before the collision
    public Material afterCollisionMaterial; // Material after the collision
    public GameObject targetObject; // Paint object
    public GameObject otherObject; // Another object with a collider
    public AudioClip changeColorSound1; // First sound clip
    public AudioClip changeColorSound2; // Second sound clip

    private Renderer targetRenderer;
    private AudioSource audioSource;
    private bool sound1Played = false;
    private bool sound2Played = false;

    void Start()
    {
        // Get the renderer component of the target object
        targetRenderer = targetObject.GetComponent<Renderer>();

        // Set the initial material of the target object
        targetRenderer.material = beforeCollisionMaterial;

        // Add an AudioSource component for sound playback
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = changeColorSound1;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider's GameObject has the specified tag
        if (other.gameObject.CompareTag("paint"))
        {
            Debug.Log("Collision with paint detected!");
            SwitchMaterial();

            // Play the first sound if not already played
            if (!sound1Played)
            {
                PlayChangeColorSound1();
                sound1Played = true;
            }

            // Invoke a method to play the second sound and enable collider after a delay
            Invoke("PlayChangeColorSound2AndEnableCollider", 2.0f);
        }
    }

    void SwitchMaterial()
    {
        // Switch to the material after the collision for the target object
        Debug.Log("Material switched to: " + afterCollisionMaterial.name);
        targetRenderer.material = afterCollisionMaterial;
    }

    void PlayChangeColorSound1()
    {
        // Play the first assigned sound clip
        if (audioSource != null && changeColorSound1 != null)
        {
            audioSource.Play();
        }
    }

    void PlayChangeColorSound2AndEnableCollider()
    {
        // Change the sound clip to the second one
        audioSource.clip = changeColorSound2;

        // Play the second sound clip if not already played
        if (!sound2Played && audioSource != null && changeColorSound2 != null)
        {
            audioSource.Play();
            sound2Played = true;
        }

        // Enable the collider on the other object
        EnableColliderOnOtherObject();
    }

    void EnableColliderOnOtherObject()
    {
        // Enable the collider on the specified other object
        Collider otherObjectCollider = otherObject.GetComponent<Collider>();
        if (otherObjectCollider != null)
        {
            otherObjectCollider.enabled = true;
            Debug.Log("Collider on other object enabled!");
        }
    }
}
