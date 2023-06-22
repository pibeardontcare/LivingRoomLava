using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowSwap : MonoBehaviour
{
    public GameObject lavaPillowPrefab; // Reference to LavaPillow prefab

    private bool hasSwapped = false; // Flag to track swapping

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !hasSwapped)
        {
            // Get the position of the throw pillow
            Vector3 newPosition = transform.position;

            // Set the Y position to 0
            newPosition.y = 0f;

            // Perform the swap
            GetComponent<Renderer>().enabled = false; // Hide throw pillow

            // Instantiate the LavaPillow prefab
            GameObject newLavaPillow = Instantiate(lavaPillowPrefab, newPosition, Quaternion.identity);
            newLavaPillow.GetComponent<Renderer>().enabled = true; // Show LavaPillow

            hasSwapped = true;
            Destroy(gameObject); // Destroy the throw pillow object after swapping
        }
    }
}
