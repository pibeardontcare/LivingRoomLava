using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowSwap : MonoBehaviour
{
    public GameObject pathPrefab; // Reference to path prefab

    private bool hasSwapped = false; // Flag to track swapping

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !hasSwapped)
        {
            // Get the position of the throw pillow
            Vector3 pillowPosition = transform.position;

            // Set the Y position to 0
            pillowPosition.y = 0f;

            // Perform the swap
            GameObject newPath = Instantiate(pathPrefab, pillowPosition, Quaternion.identity);
            newPath.SetActive(true); // Activate LavaPillow

            hasSwapped = true;
        }
    }
}
