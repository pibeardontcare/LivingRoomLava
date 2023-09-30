using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class InventoryManager : MonoBehaviour

{
    public Transform oculusHand;  // Reference to the Oculus left hand GameObject
    public GameObject objectPrefab;  // Reference to the object prefab to be spawned

    private GameObject spawnedObject;

    private void Update()
    {
        // Check if the Oculus hand GameObject exists
        if (oculusHand != null)
        {
            // Calculate the spawn position based on the hand's position and orientation
            Vector3 spawnPosition = oculusHand.position + oculusHand.forward * 0.03f
            - oculusHand.right * 0.05f   // Move -0.05 units in the x (towards fingers/palm)
            + oculusHand.up * 0.05f;     // Move 0.05 units in the y (around the palm)


            // Spawn the object if it doesn't exist
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                // Update the object's position to follow the hand
                spawnedObject.transform.position = spawnPosition;
            }
        }
        {
            Debug.Log("Oculus hand GameObject not found!");
        }
    }
}
