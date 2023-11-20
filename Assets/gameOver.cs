using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public InOrOutColor inOrOutColor; // Reference to the script controlling game over status
    public List<GameObject> objectsToUpdateMaterials; // List of objects whose materials you want to update
    public Material newMaterial; // The new material to apply

     public Light sceneLight; // Reference to the light component

    private void Start()
    {
         // Ensure the references are set
        if (inOrOutColor == null || objectsToUpdateMaterials.Count == 0 || newMaterial == null || sceneLight == null)
        {
            Debug.LogError("References not set. Please assign all necessary references in the inspector.");
            enabled = false; // Disable the script if references are not set
        }


        // Subscribe to the game over event
        inOrOutColor.GameOverEvent += OnGameOver;
    }

    private void OnGameOver()
    {
        // Change lighting when the game is over (customize this part based on your requirements)
        ChangeLighting();

        // Update materials for each object
        foreach (GameObject obj in objectsToUpdateMaterials)
        {
            UpdateObjectMaterial(obj);
        }
    }

    private void ChangeLighting()
    {
        // Change lighting when the game is over (customize this part based on your requirements)
        if (sceneLight != null)
        {
            sceneLight.color = Color.red;
            sceneLight.intensity = 0.5f;
        }
        else
        {
            Debug.LogWarning("Light component not found. Please assign a Light component in the inspector.");
        }
    }

    private void UpdateObjectMaterial(GameObject obj)
    {
        // Update the material of the specified object
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on " + obj.name);
        }
    }

    // Ensure to unsubscribe from events when the script is disabled or destroyed
    private void OnDisable()
    {
        if (inOrOutColor != null)
        {
            inOrOutColor.GameOverEvent -= OnGameOver;
        }
    }
}
