using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
    
{
    // Reference to the Renderer component of ObjectB
    private Renderer bubbleRenderer;


    // Reference to the Renderer component of ZuriLegs
    public Renderer zuriLegsRenderer;

    // Variable to store the initial color ofZuri Legs
    private Color initialColor;

    private void Start()
    {
        // Get the Renderer component attached to this GameObject
        bubbleRenderer = GetComponent<Renderer>();

        // Store the initial color of ZuriLegs
        initialColor = zuriLegsRenderer.material.color;

        // Make ObjectB initially invisible
        bubbleRenderer.enabled = false;
    }

    private void Update()
    {
        // Check if the color of Zuri legs has changed
        if (zuriLegsRenderer.material.color != initialColor)
        {
            // Color has changed, make Bubble visible
            bubbleRenderer.enabled = true;
        }
        else
        {
            // Color is still the same, make Bubble invisible
            bubbleRenderer.enabled = false;
        }
    }
}


