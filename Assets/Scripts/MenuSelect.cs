using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour
{
    private List<GameObject> balloonObjects = new List<GameObject>();
    public Material hoverMaterial;  // Assign the material for hover state in the Inspector
    public Material originalMaterial; // Static variable to store the original material

    void Start()
    {
        // Find all objects in the scene with the tag "balloon"
        GameObject[] balloons = GameObject.FindGameObjectsWithTag("balloon");

        // Store the objects in the list
        balloonObjects.AddRange(balloons);

        // Store the original material (if not already stored)
        if (originalMaterial == null && balloonObjects.Count > 0)
        {
            MeshRenderer meshRenderer = balloonObjects[0].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                originalMaterial = meshRenderer.material;
            }
        }
    }

    public void HoverOver()
    {
        // Change materials to the hover material for all balloons
        foreach (GameObject balloon in balloonObjects)
        {
            SetMaterial(balloon, hoverMaterial);
        }
    }

    public void HoverExit()
    {
        // Revert to the original material for all balloons when the hover state is exited
        foreach (GameObject balloon in balloonObjects)
        {
            SetMaterial(balloon, originalMaterial);
        }
    }

    // Helper method to set a material for the MeshRenderer of a specific game object
    private void SetMaterial(GameObject obj, Material newMaterial)
    {
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material = newMaterial;
        }
    }
}