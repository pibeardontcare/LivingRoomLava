using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowSwap : MonoBehaviour
{
 
    public GameObject lavaPillow; // Reference to Pillow
    private bool hasSwapped = false; // Flag to track swapping

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !hasSwapped)
        {
            // Get the position and rotation of throw pillow
            Vector3 newPosition = transform.position;
            Quaternion newRotation = transform.rotation;

            // Perform the swap
            GetComponent<Renderer>().enabled = false; // Hide throw pillow

            // Instantiate Object B at the same position and rotation as Throw Pillow
            GameObject newlavaPillow = Instantiate(lavaPillow, newPosition, newRotation);
            newlavaPillow.GetComponent<Renderer>().enabled = true; // Show LavaPillow

            hasSwapped = true;
        }
    }


}
