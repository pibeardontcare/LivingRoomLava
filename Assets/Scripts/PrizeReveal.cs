using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeReveal : MonoBehaviour
{
    public bool hasPickedUpBox = false;

    public GameObject prizeObject;
    public GameObject lavaNote;

    // Reference to the ColorChanger script
    public ColorChanger colorChanger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            hasPickedUpBox = true;
            Destroy(other.gameObject); // Remove the box from the scene
        }
    }

    private void Update()
    {
        // Check if both conditions are met: box picked up and game over
        if (hasPickedUpBox && colorChanger.IsGameOver)
        {
            // Show the prize object
            prizeObject.SetActive(true);
        }
        else
        {
            // Show the lava note
            lavaNote.SetActive(true);
        }
    }
}
