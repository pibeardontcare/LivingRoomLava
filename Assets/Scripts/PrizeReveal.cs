using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeReveal : MonoBehaviour
{
  
    public ColorChanger colorChanger; // Drag your ColorChanger script here
    public GameObject prizeDescriptionUI;
    public GameObject nextLevelButton;

    private bool hasBeenPickedUp = false;


        private void Start()
    {
        // Deactivate the UI elements initially
        prizeDescriptionUI.SetActive(false);
        nextLevelButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenPickedUp && other.CompareTag("Player"))
        {
            if (!colorChanger.IsGameOver)
            {
                // Trigger the box opening animation here

                // Enable prize description UI and next level button
                prizeDescriptionUI.SetActive(true);
                nextLevelButton.SetActive(true);

                hasBeenPickedUp = true;
            }
            else
            {
                return;
                // Display a message or effect indicating the game is over
            }
        }
    }
}
