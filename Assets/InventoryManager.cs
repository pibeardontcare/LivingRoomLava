using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour

{
    public Transform oculusHand;  // Reference to the Oculus left hand GameObject
    public GameObject objectPrefab;  // Reference to the object prefab to be spawned

    public TMP_Text debugText;
    private GameObject spawnedObject;


  private void Start()
    {
        // Get the Text component on the Text object
        debugText = GetComponent<TMP_Text>();
    }

    private void Update()
    {

        // Check if the Oculus hand GameObject exists
        if (oculusHand != null)
        {

            // Calculate the angle between the hand's up direction and the world up direction (palm orientation)
            float angle = Vector3.Angle(oculusHand.up, Vector3.up);

            // Update the Text object with the debug message
            debugText.text = "mep";  // Format to two decimal places
            // Calculate the spawn position based on the hand's position and orientation
            
           
       
            // Calculate the spawn position based on the hand's position and orientation
            Vector3 spawnPosition = oculusHand.position + oculusHand.forward * 0.03f
            - oculusHand.right * 0.05f   // Move -0.05 units in the x (towards fingers/palm)
            + oculusHand.up * 0.05f;     // Move 0.05 units in the y (around the palm)


            // Spawn the object if it doesn't exist
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                // Update the object's position to follow the hand
                spawnedObject.transform.position = spawnPosition;
            }
        }
        {
            Debug.Log("Oculus hand GameObject not found!");
        }
          
            
    }
}
