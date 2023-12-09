using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndSceneTrigger : MonoBehaviour
{
    // Reference to the LevelChanger script on another object
    public LevelChanger levelChanger;

    // This method is called when another collider enters the trigger collider.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("paint")) // 
            // Trigger the fade in the LevelChanger script
            levelChanger.FadeToLevel(1); 
    }
}

