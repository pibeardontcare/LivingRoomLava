using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeIndicator : MonoBehaviour
{
    public SafeBoundaries safeBoundariesScript; // Reference to the SafeBoundaries script
    public Renderer objectRenderer; // Assign the Renderer component here
    public Color safeColor = Color.green;
    public Color dangerColor = Color.red;
    public Text displayText; // Assign the Text component here

    private void Start()
    {
        UpdateColorAndText();
    }

    private void Update()
    {
        UpdateColorAndText();
    }

    private void UpdateColorAndText()
    {
        if (safeBoundariesScript.IsPlayerWithinBoundaries)
        {
            objectRenderer.material.color = safeColor;
            displayText.text = "Safe Zone";
        }
        else
        {
            objectRenderer.material.color = dangerColor;
            displayText.text = "Danger Zone";
        }
    }
}
