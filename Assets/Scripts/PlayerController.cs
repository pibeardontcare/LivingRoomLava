using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectToPlayer : MonoBehaviour
{
    public GameObject playerMarker; // Reference to the player's marker object
    public GameObject objectToSnap; // The object you want to snap to the player
    public GameObject thirdObject; // The object that will appear when the player marker is close

    private float activationDistance = 0.2f; // The distance at which the third object should appear

    private void Start()
    {
         // Deactivate the third object
                thirdObject.SetActive(false);
        if (playerMarker != null && objectToSnap != null)
        {
            // Get the position and rotation of the playerMarker
            Vector3 playerPosition = playerMarker.transform.position;
            Quaternion playerRotation = playerMarker.transform.rotation;

            // Set the position and rotation of the object to match the player's position and rotation
            objectToSnap.transform.position = playerPosition;
            objectToSnap.transform.rotation = playerRotation;
        }
        else
        {
            Debug.LogError("Missing references. Make sure to assign PlayerMarker and ObjectToSnap in the Inspector.");
        }
    }

    private void Update()
    {
        if (playerMarker != null && objectToSnap != null && thirdObject != null)
        {
            // Calculate the distance between the player marker and the object to snap
            float distance = Vector3.Distance(playerMarker.transform.position, objectToSnap.transform.position);

            // Check if the distance is less than the activationDistance
            if (distance < activationDistance)
            {
                // Activate the third object
                thirdObject.SetActive(true);
            }
            else
            {
                // Deactivate the third object
                thirdObject.SetActive(false);
            }
        }
    }
}
