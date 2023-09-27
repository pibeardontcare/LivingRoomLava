using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeGlowSelected : MonoBehaviour
{

    public GameObject objectToShowHide;

    private void Start()
    {
        // Make sure the initial state is set correctly
        objectToShowHide.SetActive(false);
    }

    // Called when the OVR hand enters the collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is an OVR hand
        if (other.CompareTag("Hand"))
        {
            // Show the object when the hand enters the collider
            objectToShowHide.SetActive(true);
        }
    }

    // Called when the OVR hand exits the collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object is an OVR hand
        if (other.CompareTag("Hand"))
        {
            // Hide the object when the hand exits the collider
            objectToShowHide.SetActive(false);
        }
    }
}
