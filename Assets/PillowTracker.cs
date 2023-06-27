using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowTracker : MonoBehaviour
{


    // Start is called before the first frame update
    public List<GameObject> pillows; // List to hold the pillow game objects
    public LineRenderer lineRenderer; // Reference to the LineRenderer component

    // Start is called before the first frame update
    void Start()
    {
        pillows = new List<GameObject>();
        lineRenderer = GetComponent<LineRenderer>(); // Get reference to the LineRenderer component attached to this object
    }

    // Update is called once per frame
    void Update()
    {
        // Clear the line renderer
        lineRenderer.positionCount = 0;

        // Iterate over the pillows list and store the position data
        foreach (GameObject pillow in pillows)
        {
            // Store the position data of each pillow
            Vector3 position = pillow.transform.position;

            // Add the position to the line renderer
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(position.x, 0f, position.z));
        }
    }
}
