using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material materialInsideStartObject; // Material to use when the user is inside the boundaries of the start object
    public Material materialInsideBoundaries; // Material to use when the user is inside the boundaries defined by the BoundaryChecker script
    public Material materialOutsideAllBoundaries; // Material to use when the user is outside all boundaries

    private Renderer objectRenderer; // Reference to the Renderer component attached to the object
    private BoundaryChecker boundaryChecker; // Reference to the BoundaryChecker script
    public GameObject startObject; // Reference to the start object
    

    private void Start()
    {
        // Get the Renderer component attached to the object
        objectRenderer = GetComponent<Renderer>();

        // Find the BoundaryChecker script in the scene
        boundaryChecker = GameObject.FindObjectOfType<BoundaryChecker>();

        // ...
    }

    private void Update()
    {
        // Check if the user is inside the boundaries defined by the BoundaryChecker script
        bool isInsideBoundaries = boundaryChecker.CheckInsideBoundaries();

        // Check if the user's position is inside the boundaries of the start object
        bool isInsideStartObject = IsInsideStartObject();

        // Update the material of the object's renderer based on the user's position
        if (isInsideStartObject)
        {
            objectRenderer.material = materialInsideStartObject;
        }
        else if (isInsideBoundaries)
        {
            objectRenderer.material = materialInsideBoundaries;
        }
        else
        {
            objectRenderer.material = materialOutsideAllBoundaries;
        }
    }

    // Check if the user's position is inside the boundaries of the start object
    private bool IsInsideStartObject()
    {
        // Get the position and scale of the start object
        Vector3 startObjectPosition = startObject.transform.position;
        Vector3 startObjectScale = startObject.transform.localScale;

        // Calculate the half extents of the start object
        float halfStartObjectX = startObjectScale.x * 0.5f;
        float halfStartObjectZ = startObjectScale.z * 0.5f;

        // Get the user's position (VR headset camera position)
        Vector3 userPosition = Camera.main.transform.position;

        // Check if the user's position is within the boundaries of the start object
        if (userPosition.x >= startObjectPosition.x - halfStartObjectX && userPosition.x <= startObjectPosition.x + halfStartObjectX &&
            userPosition.z >= startObjectPosition.z - halfStartObjectZ && userPosition.z <= startObjectPosition.z + halfStartObjectZ)
        {
            return true; // User's position is inside the boundaries of the start object
        }

        return false; // User's position is outside the boundaries of the start object
    }
}
