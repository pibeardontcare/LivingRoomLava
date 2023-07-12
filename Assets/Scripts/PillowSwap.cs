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

            // Check for overlapping objects
            bool isOverlapping = CheckOverlap(objectPosition);

            if (!isOverlapping)
            {
                // Instantiate the safe area object with zero rotation and modified position
                GameObject newSafeArea = Instantiate(safeAreaPrefab, objectPosition, Quaternion.identity);

                // Destroy the original object
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Object overlap detected. Cannot spawn new object.");
            }
        }
    }

    private bool CheckOverlap(Vector3 position)
    {
        // Get all colliders within the safe area's position and size
        Collider[] colliders = Physics.OverlapBox(position, safeAreaPrefab.transform.localScale / 2f);

        // Check if any colliders were found (excluding self)
        return colliders.Length > 0;
    }
}
