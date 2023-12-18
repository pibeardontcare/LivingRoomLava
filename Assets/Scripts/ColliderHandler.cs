using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
    public Material redMaterial; // Reference to the red material
    public GameObject otherObject; // Reference to the other object

   
    // Start is called before the first frame update
    void Start()
    {
        // Disable the collider at the start of the game
        GetComponent<Collider>().enabled = false;
        Debug.Log("Collider NOT ready!");

        // Set another object's material to red
        SetMaterialToRed();
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any update logic here if needed
    }

    void SetMaterialToRed()
    {
        // Check if the other object is assigned
        if (otherObject == null)
        {
            Debug.LogError("Other object not assigned!");
            return;
        }

        // Get the Renderer component of the other object
        Renderer otherObjectRenderer = otherObject.GetComponent<Renderer>();

        // Check if the other object has a Renderer component and the red material is assigned
        if (otherObjectRenderer != null && redMaterial != null)
        {
            // Change the material to red
            otherObjectRenderer.material = redMaterial;
            Debug.Log("Material set to red!");
        }
        else
        {
            Debug.LogError("Renderer or red material not assigned!");
            Debug.Log("Object Name: " + gameObject.name);
            Debug.Log("Parent GameObject: " + transform.parent.name);
        }
    }
}
