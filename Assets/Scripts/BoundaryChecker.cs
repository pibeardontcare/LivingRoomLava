using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour
{
    public delegate void BoundaryObjectCollidedEventHandler(GameObject boundaryObject);
    public event BoundaryObjectCollidedEventHandler OnBoundaryObjectCollided;
    public Transform vrCameraTransform; // Reference to the VR camera transform
    public List<GameObject> boundaryObjects; // List of game objects representing the boundaries

    private bool isInsideBoundaries = false; // Flag to track if the user is inside the boundaries

    public delegate void BoundaryEnterEventHandler();
    public event BoundaryEnterEventHandler OnEnterBoundaries;

    public delegate void BoundaryExitEventHandler();
    public event BoundaryExitEventHandler OnExitBoundaries;

    // Update is called once per frame
    private void Update()
    {
        // Check if the user is inside the boundaries
        bool isInside = CheckInsideBoundaries();

        // Check for boundary enter/exit events and trigger them accordingly
        if (isInside && !isInsideBoundaries)
        {
            isInsideBoundaries = true;
            if (OnEnterBoundaries != null)
            {
                OnEnterBoundaries();
            }
        }
        else if (!isInside && isInsideBoundaries)
        {
            isInsideBoundaries = false;
            if (OnExitBoundaries != null)
            {
                OnExitBoundaries();
            }
        }
    }

    public bool CheckInsideBoundaries()
    {
        // Get the user's position from the VR camera transform
        Vector3 userPosition = vrCameraTransform.position;

        // Loop through all the boundary objects and check if the user's position is within their X and Z boundaries
        foreach (GameObject boundaryObject in boundaryObjects)
        {
            // Get the position and scale of the boundary object
            Vector3 boundaryPosition = boundaryObject.transform.position;
            Vector3 boundaryScale = boundaryObject.transform.localScale;

            // Calculate the boundaries
            float minX = boundaryPosition.x - boundaryScale.x * 0.5f;
            float maxX = boundaryPosition.x + boundaryScale.x * 0.5f;
            float minZ = boundaryPosition.z - boundaryScale.z * 0.5f;
            float maxZ = boundaryPosition.z + boundaryScale.z * 0.5f;

            // Check if the user's position is inside the boundaries
            if (userPosition.x >= minX && userPosition.x <= maxX && userPosition.z >= minZ && userPosition.z <= maxZ)
            {
                return true;
            }
        }

        return false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Notify listeners that a boundary object has collided with the floor
            if (OnBoundaryObjectCollided != null)
            {
                OnBoundaryObjectCollided(gameObject);
            }
        }
    }

}
