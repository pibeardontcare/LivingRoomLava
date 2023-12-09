using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDisableEndScene : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // Disable the collider at the start of the game
        DisableCollider();
    }

    // Update is called once per frame
    void Update()
    {
        // You can add any update logic here if needed
    }

    void DisableCollider()
    {
        // Check if the Collider component is present
        Collider colliderComponent = GetComponent<Collider>();

        if (colliderComponent != null)
        {
            // Disable the collider
            colliderComponent.enabled = false;
            Debug.Log("Collider disabled!");
        }
        else
        {
            Debug.LogError("Collider component not found!");
        }
    }
}
