using UnityEngine;
using System.Collections.Generic;

public class SafeBoundaries : MonoBehaviour
{
    private HashSet<GameObject> collidedObjects = new HashSet<GameObject>();
    private Vector2 minSafeBoundary = new Vector2(float.MaxValue, float.MaxValue);
    private Vector2 maxSafeBoundary = new Vector2(float.MinValue, float.MinValue);

    public bool IsPlayerWithinBoundaries { get; private set; } = false;

    // Player position variables
    private float playerXPosition;
    private float playerZPosition;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidingObject = collision.gameObject;

        // Check if the colliding object is already in the set
        if (!collidedObjects.Contains(collidingObject))
        {
            collidedObjects.Add(collidingObject);
            UpdateSafeBoundaries(collidingObject);
            Debug.Log("Collision detected with: " + collidingObject.name);
        }
    }

    private void UpdateSafeBoundaries(GameObject obj)
    {

        

        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            Vector3 position = obj.transform.position;
            Vector3 size = collider.bounds.size;

            // Update the min and max safe boundaries
            minSafeBoundary.x = Mathf.Min(minSafeBoundary.x, position.x - size.x * 0.5f);
            minSafeBoundary.y = Mathf.Min(minSafeBoundary.y, position.z - size.z * 0.5f);
            maxSafeBoundary.x = Mathf.Max(maxSafeBoundary.x, position.x + size.x * 0.5f);
            maxSafeBoundary.y = Mathf.Max(maxSafeBoundary.y, position.z + size.z * 0.5f);

            Debug.Log("Safe boundaries updated for: " + obj.name);
        }
   
    }

    private void Update()
    {
        // Calculate player's x and z positions based on main camera
        CalculatePlayerPosition();
        
        // Check if the player is within the safe boundaries
        bool isPlayerWithinBoundaries = CheckIfPositionWithinBoundaries(playerXPosition, playerZPosition);
        Debug.Log("Player within boundaries: " + isPlayerWithinBoundaries);
        IsPlayerWithinBoundaries = CheckIfPositionWithinBoundaries(playerXPosition, playerZPosition);
    }

    private void CalculatePlayerPosition()
    {
        // Get the player's camera (main camera) position
        Vector3 playerPosition = Camera.main.transform.position;
        
        // Store the x and z positions
        playerXPosition = playerPosition.x;
        playerZPosition = playerPosition.z;
    }

    private bool CheckIfPositionWithinBoundaries(float xPosition, float zPosition)
    {
        // Check if the position is within the calculated safe boundaries
        return xPosition >= minSafeBoundary.x && xPosition <= maxSafeBoundary.x &&
               zPosition >= minSafeBoundary.y && zPosition <= maxSafeBoundary.y;
    }
}
