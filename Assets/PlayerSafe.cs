using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSafe : MonoBehaviour
{
    private PillowSwap pillowSwapScript; // Reference to the PillowSwap script
    private bool _isOnPath; // Declare the _isOnPath variable
    public GameObject targetGameObject; // Reference to the target game object for color changes
    private Renderer targetRenderer; // Reference to the Renderer component of the target game object

    private void Start()
    
    {
        // Find the instance of the PillowSwap script in the scene
        pillowSwapScript = FindObjectOfType<PillowSwap>();

        // Get the Renderer component attached to the target game object
        targetRenderer = targetGameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        // Access the prefabPositions list from the PillowSwap script
        List<Vector3> prefabPositions = pillowSwapScript.GetPrefabPositions();

        // Check if the player is on the path.
        Vector3 playerPosition = Camera.main.transform.position;
        bool isOnPath = true;
        foreach (Vector3 prefabPosition in prefabPositions)
        {
            if (!SafeXZ.IsInXZRange(playerPosition, prefabPosition, 0.5f))
            {
                isOnPath = false;
                break;
            }
        }

        _isOnPath = isOnPath;

        // Change the color of the target game object based on _isOnPath
        if (!_isOnPath)
        {
            targetRenderer.material.color = Color.red;
        }
        else
        {
            // Set the color back to the original color if needed
            targetRenderer.material.color = Color.white;
        }
    }

    private bool IsOnPath()
    {
        return _isOnPath;
    }
}
