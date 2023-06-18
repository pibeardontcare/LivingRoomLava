using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CenterEyePosition : MonoBehaviour
{
    public Transform targetTransform;
    public GameObject lavaSpawnerObject;
    public TMP_Text textDisplay;
    public float xMin = -1.15f;
    public float xMax = -0.15f;
    public float zMin = 0.69f;
    public float zMax = 1.69f;

    private bool hasEnteredRange = false;

    private void Update()
    {
        // Check if the target object's transform's position is within the specified range
        if (targetTransform.transform.position.x > xMin && targetTransform.transform.position.x < xMax &&
            targetTransform.transform.position.z > zMin && targetTransform.transform.position.z < zMax)
        {
            if (!hasEnteredRange)
            {

// Get a    reference to the LavaSpawner component on the lavaSpawnerObject
                LavaSpawner lavaSpawner = lavaSpawnerObject.GetComponent<LavaSpawner>();
                // Call the TriggerMethod on the LavaSpawner script
                lavaSpawner.TriggerMethod();
                // Set the flag to true so that this block of code only runs once
                hasEnteredRange = true;
                
                // Display the "Don't Step in the Lava!" text
                textDisplay.text = "Don't Step in the Lava!";

                

            }
        }
        else
        {
            // Reset the flag so that the "Don't Step in the Lava!" text can be displayed again
            hasEnteredRange = false;

            // // Display the "Start Here" text
            // textDisplay.text = "Start Here";
        }
    }
}
