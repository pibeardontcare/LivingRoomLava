using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPaint : MonoBehaviour
{
    public Material beforeCollisionMaterial; // Material before the collision
    public Material afterCollisionMaterial; // Material after the collision
    public GameObject targetObject; //  paint object

    public AudioClip changeColorSound; 

    private Renderer targetRenderer;
    private bool collisionOccurred = false;

     private AudioSource audioSource;

    void Start()
    {
        

        // Get the renderer component of the target object
        targetRenderer = targetObject.GetComponent<Renderer>();

        // Set the initial material of the target object
        targetRenderer.material = beforeCollisionMaterial;

        Debug.Log("Start material set");

        // Add an AudioSource component for sound playback
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = changeColorSound; 
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider's GameObject has the specified tag
        if (other.gameObject.CompareTag("paint"))
        {
            Debug.Log("Collision with paint detected!");
            SwitchMaterial();

        // Play the sound
        PlayChangeColorSound();
            
        }
    }
    void SwitchMaterial()
    {
        // Switch to the material after the collision for the target object
        Debug.Log("Material switched to: " + afterCollisionMaterial.name);
        targetRenderer.material = afterCollisionMaterial;
    }

    void PlayChangeColorSound()
    {
        // Play the assigned sound clip
        if (audioSource != null && changeColorSound != null)
        {
            audioSource.Play();
        }

    }

    
}
