using System.Collections;
using UnityEngine;

public class PaintbrushCatcher : MonoBehaviour
{
    public AudioClip grassEffect; // The sound to play when the paintbrush enters the trigger
    public Transform paintbrushReturnTransform; // The destination transform where you want to move the paintbrush
    public string paintTag = "paint"; // Tag to identify the paint object
    public GameObject paintbrush; // Reference to the paintbrush to be moved

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(paintTag))
        {
            // Play sound effect
            if (grassEffect != null)
            {
                AudioSource.PlayClipAtPoint(grassEffect, transform.position);
            }
            Debug.Log("the paintbrush fell");

            // Schedule the movement after a 1-second delay
            Invoke("MovePaintbrushToReturnTransform", 1f);
        }
    }

    private void MovePaintbrushToReturnTransform()
    {
        // Move the paintbrush to the destination
        if (paintbrushReturnTransform != null && paintbrush != null)
        {
            paintbrush.transform.position = paintbrushReturnTransform.position;
            paintbrush.transform.rotation = paintbrushReturnTransform.rotation;
        }
    }
}
