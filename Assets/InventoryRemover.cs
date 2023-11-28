using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRemover : MonoBehaviour
{

     public GameObject replacementObject; 

    public float yOffset = 0.12f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Calculate the spawn position with the desired offset
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

            // Spawn a new object at the calculated position
            Instantiate(replacementObject, spawnPosition, transform.rotation);

            // Hide the original object
            gameObject.SetActive(false);
    }
}
