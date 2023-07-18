using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrizeReveal : MonoBehaviour

{
    public bool hasPickedUpBox = false;
    public bool isWithinBoundary = true;

    public GameObject prizeObject;
    public GameObject lavaNote;

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
        if (hasPickedUpBox && isWithinBoundary)
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
