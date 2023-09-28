using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class InventoryManager : MonoBehaviour
{
       // Reference to the left and right OVRHand components
    public OVRHand leftHand;
    public OVRHand rightHand;

    public GameObject spawnedObject; // Reference to the spawned object

    private void Update()
    {
        if (leftHand != null && leftHand.IsPointerPoseValid)
        {
            CheckAndSpawnObject(leftHand);
        }
        else if (rightHand != null && rightHand.IsPointerPoseValid)
        {
            CheckAndSpawnObject(rightHand);
        }
        else if (spawnedObject != null)
        {
            // If neither hand has a valid pointer pose, destroy the spawned object
            Destroy(spawnedObject);
            spawnedObject = null;
        }
    }

    private void CheckAndSpawnObject(OVRHand hand)
    {
        // Get the orientation of the pointer pose (usually index finger)
        Quaternion pointerPoseRotation = hand.PointerPose.rotation;

        // Define a threshold for what constitutes "facing the user"
        float facingThreshold = 0.9f;

        // Check if the palm is facing the user
        if (Vector3.Dot(pointerPoseRotation * Vector3.forward, Vector3.forward) > facingThreshold)
        {
            // Palm is facing the user, spawn the object if it hasn't been spawned yet
            if (spawnedObject == null)
            {
                // Instantiate the object prefab at the hand's position and orientation
                spawnedObject = Instantiate(spawnedObject, hand.PointerPose.position, pointerPoseRotation);

                // Set the hand as the parent of the spawned object
                spawnedObject.transform.parent = hand.transform; // The hand GameObject becomes the parent

                // You can also adjust the object's local position and rotation relative to the hand if needed
                // spawnedObject.transform.localPosition = ...
                // spawnedObject.transform.localRotation = ...
            }
        }
        else
        {
            // Palm is not facing the user, destroy the spawned object if it exists
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
                spawnedObject = null;
            }
        }
    }
}