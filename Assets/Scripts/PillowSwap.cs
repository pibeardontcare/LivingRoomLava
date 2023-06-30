using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowSwap : MonoBehaviour
{
    public GameObject lavaPillowPrefab; // Reference to LavaPillow prefab
    public List<Transform> instantiatedSafeArea; // List to store the transforms of instantiated prefabs

    private bool hasSwapped = false; // Flag to track swapping

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

            // Perform the swap
            GameObject newLavaPillow = Instantiate(lavaPillowPrefab, pillowPosition, Quaternion.identity);
            newLavaPillow.SetActive(true); // Activate LavaPillow

            // Add the transform of the new lava pillow to the list
            instantiatedSafeArea.Add(newLavaPillow.transform);

            hasSwapped = true;
        }
    }
}
