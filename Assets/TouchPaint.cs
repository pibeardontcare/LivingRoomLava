using System.Collections;
using UnityEngine;

public class TouchPaint : MonoBehaviour
{
    public Material notReadyMaterial; // Red not ready material
    public Material colliderAvailableMaterial;
    public Material afterCollisionMaterial;
    public GameObject targetObject;
    public AudioClip changeColorSound1;
    public AudioClip nextPaintObject;

    public  MonoBehaviour colliderHandler;
    public GameObject nextObject;

    private Renderer targetRenderer;
    private Renderer nextObjectRenderer;
    private AudioSource audioSource;

    private bool changeColorSound1Played = false;
    private bool nextPaintObjectPlayed = false;

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

            if (!nextPaintObjectPlayed)
            {
                StartCoroutine(PlayNextPaintObjectAndEnableCollider());
                nextPaintObjectPlayed = true;
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

    IEnumerator PlayNextPaintObjectAndEnableCollider()
    {
        audioSource.clip = nextPaintObject;

        if (audioSource != null && nextPaintObject != null)
        {
            audioSource.PlayOneShot(nextPaintObject);
        }

// Wait until the audio finishes playing
    while (audioSource.isPlaying)
    {
        yield return null;
    }
        //yield return new WaitForSeconds(5f);

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
