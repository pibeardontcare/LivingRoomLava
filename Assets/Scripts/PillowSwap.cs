using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowSwap : MonoBehaviour
{
    public GameObject lavaPillowPrefab; // Reference to LavaPillow prefab
    public List<Transform> instantiatedSafeArea; // List to store the transforms of instantiated prefabs

    private bool hasSwapped = false; // Flag to track swapping
    private bool isBouncing = false; // Flag to track bouncing

    private void Start()
    {
        instantiatedSafeArea = new List<Transform>(); // Initialize the list
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !hasSwapped)
        {
            // Get the position of the throw pillow
            Vector3 pillowPosition = transform.position;

            // Set the Y position to 0
            pillowPosition.y = 0f;

            // Disable the collider temporarily to prevent further collisions
            GetComponent<Collider>().enabled = false;

            // Enable bouncing flag
            isBouncing = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && isBouncing)
        {
            // Start the coroutine to check when the pillow has stopped bouncing
            StartCoroutine(CheckBounceEnd());
        }
    }

    private IEnumerator CheckBounceEnd()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        while (rb.velocity.magnitude > 0.1f) // Adjust the threshold as needed
        {
            yield return null;
        }

        // Get the position of the throw pillow
        Vector3 pillowPosition = transform.position;

        // Set the Y position to 0
        pillowPosition.y = 0f;

        // Perform the swap
        GameObject newLavaPillow = Instantiate(lavaPillowPrefab, pillowPosition, Quaternion.identity);
        newLavaPillow.SetActive(true); // Activate LavaPillow

        // Add the transform of the new lava pillow to the list
        instantiatedSafeArea.Add(newLavaPillow.transform);

        // Enable the collider again
        GetComponent<Collider>().enabled = true;

        hasSwapped = true;
        isBouncing = false;
    }
}
