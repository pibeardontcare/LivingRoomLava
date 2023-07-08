using UnityEngine;

public class PillowSwap : MonoBehaviour
{
    public GameObject safeAreaPrefab; // Reference to SafeArea prefab

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger is tagged as "Floor"
        if (other.CompareTag("Floor"))
        {
            // Get the position of the original object
            Vector3 objectPosition = transform.position;

            // Set the y position to 0.1
            objectPosition.y = 0.1f;

            // Instantiate the safe area object with zero rotation and modified position
            GameObject newSafeArea = Instantiate(safeAreaPrefab, objectPosition, Quaternion.identity);

            // Destroy the original object
            Destroy(gameObject);
        }
    }
}
