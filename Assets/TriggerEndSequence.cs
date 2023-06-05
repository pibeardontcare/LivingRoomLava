using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerEndSequence : MonoBehaviour
{
    public Transform targetTransform;
    public GameObject spawnPrizeObject;
    public GameObject fireworkSpawnerObject;
    // //show hide next button marker
    // public GameObject hideObject;
    // public GameObject showObject;

    //public Vector3 staticPosition;
    public TMP_Text textDisplay;
    public float xMin = -1.15f;
    public float xMax = - .15f;
    public float zMin = .69f;
    public float zMax = 1.69f;    

    private bool triggered = false; 
       private int level1Progress = 0;
    


    

    private void Update()
    {
        // Check if the target object's transform's position is within the specified range
        if (!triggered && targetTransform.transform.position.x > xMin && targetTransform.transform.position.x < xMax &&
            targetTransform.transform.position.z > zMin && targetTransform.transform.position.z < zMax)
        {
            triggered = true; 
            textDisplay.text = "You Did It!";


         // Store the user's progress for level 1 using PlayerPrefs.SetInt()
            level1Progress = 1; // or any other value that represents the user's progress for level 1
            PlayerPrefs.SetInt("Level1Progress", level1Progress);

           // Get a reference to the LavaSpawner component on the lavaSpawnerObject
            SpawnPrize spawnprize = spawnPrizeObject.GetComponent<SpawnPrize>();

            // Call the TriggerMethod on the LavaSpawner script
            spawnprize.TriggerMethod();

             FireworkSpawner fireworkspawner = fireworkSpawnerObject.GetComponent<FireworkSpawner>();

            // Call the TriggerMethod on the LavaSpawner script
            fireworkspawner.TriggerMethod();

            //hide and show pins
            // showObject.SetActive(true);
            // hideObject.SetActive(false);





        }
        else
        {
            textDisplay.text = "End Here";
        }
    }
}



