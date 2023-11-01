// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectToPlayer : MonoBehaviour
{
    public OVRCameraRig cameraRig; // Reference to the OVRCameraRig in the scene
    public GameObject objectToSnap; // The object you want to snap to the player

    private void Start()
    {
        if (cameraRig != null && objectToSnap != null)
        {
            // Get the position and rotation of the camera rig's tracking space
            Vector3 playerPosition = cameraRig.trackingSpace.position;
            Quaternion playerRotation = cameraRig.trackingSpace.rotation;

            // Set the position and rotation of the object to match the player's position and rotation
            objectToSnap.transform.position = playerPosition;
            objectToSnap.transform.rotation = playerRotation;
        }
        else
        {
            Debug.LogError("Missing references. Make sure to assign OVRCameraRig and ObjectToSnap in the Inspector.");
        }
    }
}
