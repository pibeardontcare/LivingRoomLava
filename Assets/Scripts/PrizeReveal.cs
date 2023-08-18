using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeReveal : MonoBehaviour
{
    public ColorChanger colorChanger; // Drag your ColorChanger script here
    public GameObject prizeDescriptionUI;
   
    public ParticleSystem particleEmitter; // Drag your particle emitter here
    public Animator endSequenceAnimator; // Drag your end sequence animator here

    private bool triggerEntered = false; // Flag to track if the trigger has been entered

    private void Start()
    {
    
       
        
        // Deactivate the particle emitter at the start
        if (particleEmitter != null)
        {
            particleEmitter.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!colorChanger.IsGameOver)
            {
                // Trigger the box opening animation here

             // Enable prize description UI and next level button if it's not already active
                if (!prizeDescriptionUI.activeSelf)
                {
                    prizeDescriptionUI.SetActive(true);
                }

                // Start particle emitter if the trigger hasn't been entered before
                if (!triggerEntered && particleEmitter != null)
                {
                    particleEmitter.Play();
                    triggerEntered = true; // Set the flag to true to avoid playing the emitter again
                }

                // Start end sequence animation
                if (endSequenceAnimator != null)
                {
                    endSequenceAnimator.SetTrigger("StartEndSequence");
                }
                
            }
            else
            {
                // Display a message or effect indicating the game is over
            }
        }
    }
}
