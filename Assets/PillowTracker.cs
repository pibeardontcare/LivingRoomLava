using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillowTracker : MonoBehaviour
{
    public List<GameObject> pillows; // List to hold the pillow game objects
    public LineRenderer lineRenderer; // Reference to the LineRenderer component
    public Text infoText; // Reference to the Text component for displaying the information

    void Start()
    {
        pillows = new List<GameObject>();
        lineRenderer = GetComponent<LineRenderer>(); // Get reference to the LineRenderer component attached to this object
    }

    void Update()
    {
        lineRenderer.positionCount = 0; // Clear the line renderer

        foreach (GameObject pillow in pillows)
        {
            Vector3 position = pillow.transform.position;

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(position.x, 0f, position.z));
        }

        PrintPillowInformation(); // Print the pillow information
    }

    void PrintPillowInformation()
    {
        string info = "Pillow Information:\n";

        foreach (GameObject pillow in pillows)
        {
            Vector3 position = pillow.transform.position;
            Vector3 dimensions = pillow.transform.localScale;

            info += "Position: " + position + ", Dimensions: (x: " + dimensions.x + ", z: " + dimensions.z + ")\n";
        }

        infoText.text = info; // Update the Text component with the pillow information
    }
}
