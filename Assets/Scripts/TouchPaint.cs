using System.Collections;
using UnityEngine;

public class TouchPaint : MonoBehaviour
{
    public Material notReadyMaterial; // Red not ready material
    public Material colliderAvailableMaterial;
    public Material afterCollisionMaterial;
    public GameObject targetObject;
    public AudioClip changeColorSound1;
    public AudioClip promptNextObjectSound;
    public AudioClip secondSound; // New AudioClip variable for the second sound

    public MonoBehaviour colliderHandler;
    public GameObject nextObject;

    private Renderer targetRenderer;
    private Renderer nextObjectRenderer;
    private AudioSource audioSource;

    private bool changeColorSound1Played = false;
    private bool promptNextObjectSoundPlayed = false;
    private bool secondSoundPlayed = false; // New variable to track the second sound

    void Start()
    {
        targetRenderer = targetObject.GetComponent<Renderer>();

        // Ensure notReadyMaterial is assigned before starting
        if (notReadyMaterial != null)
        {
            targetRenderer.material = notReadyMaterial;
        }
        else
        {
            Debug.LogError("notReadyMaterial not assigned!");
        }

        nextObjectRenderer = nextObject.GetComponent<Renderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = changeColorSound1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("paint"))
        {
            Debug.Log("Collision with paint detected!");
            SwitchMaterial();

            if (!changeColorSound1Played)
            {
                PlayChangeColorSound1();
                changeColorSound1Played = true;
            }

            if (!promptNextObjectSoundPlayed)
            {
                StartCoroutine(PlaypromptNextObjectSoundAndEnableCollider());
                promptNextObjectSoundPlayed = true;
            }
        }
    }

    void SwitchMaterial()
    {
        Debug.Log("Material switched to: " + afterCollisionMaterial.name);
        targetRenderer.material = afterCollisionMaterial;
    }

    void PlayChangeColorSound1()
    {
        if (audioSource != null && changeColorSound1 != null)
        {
            audioSource.PlayOneShot(changeColorSound1);
        }
    }

    IEnumerator PlaypromptNextObjectSoundAndEnableCollider()
    {
        audioSource.clip = promptNextObjectSound;

        if (audioSource != null && promptNextObjectSound != null)
        {
            audioSource.PlayOneShot(promptNextObjectSound);
        }

        // Wait until the audio finishes playing
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Play the second sound if it exists and has not been played yet
        if (secondSound != null && !secondSoundPlayed)
        {
            audioSource.clip = secondSound;
            audioSource.PlayOneShot(secondSound);
            secondSoundPlayed = true;
        }

        EnableColliderOnOtherGameObject();
        Debug.Log("Collider ready!");
        SwitchMaterialOnNextObject(); // Update material on the next object
    }

    void EnableColliderOnOtherGameObject()
    {
        if (colliderHandler != null)
        {
            colliderHandler.GetComponent<Collider>().enabled = true;
            Debug.Log("Collider ready!");
        }
    }

    void SwitchMaterialOnNextObject()
    {
        // Assuming nextObjectRenderer is declared and assigned somewhere in your code
        // Switch to the material after the collision for the next object
        Debug.Log("Material switched on next object to: " + colliderAvailableMaterial.name);
        nextObjectRenderer.material = colliderAvailableMaterial;
    }
}
