using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSafeGlow : MonoBehaviour
{
   
    public GameObject glowingObjectPrefab;
    private Rigidbody rb;
    private bool isBouncing = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isBouncing && rb.velocity.magnitude < 0.1f)
        {
            // Object has stopped moving significantly
            isBouncing = false;
             // Delay the spawning of the glowing object by 3 seconds
            Invoke("SpawnGlowingObject", 2f);
        }
    }

    void SpawnGlowingObject()
    {
         
        // Instantiate the glowing object above the current object's position
        Vector3 spawnPosition = transform.position + Vector3.up * 0.1f; // Adjust the Y offset as needed
        
        GameObject glowingObject = Instantiate(glowingObjectPrefab, spawnPosition, transform.rotation);
        Instantiate(glowingObjectPrefab, spawnPosition,  transform.rotation);
        Debug.Log("done been instantiated");
        // Get the x and z scale of the current object
        Vector3 objectScale = transform.localScale;
        
        // Set the scale of the glowing object to match the x and z scale of the current object
        Vector3 glowingObjectScale = glowingObject.transform.localScale;
        glowingObjectScale.x = objectScale.x;
        glowingObjectScale.z = objectScale.z;
        glowingObject.transform.localScale = glowingObjectScale;
    
       
    }
}


