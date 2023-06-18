using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerRestart : MonoBehaviour
{
    public Transform targetTransform;
  
    //public Vector3 staticPosition;
    public TMP_Text textDisplay;
    public float xMin = -1f;
    public float xMax = -.4f;
    public float zMin = -.3f;
    public float zMax = .3f; 
    public int sceneIndex;
    public SceneTransitionManager transitionManager;   

    private bool triggered = false; 
    


    

    private void Update()
    {
        // Check if the target object's transform's position is within the specified range
        if (!triggered && targetTransform.transform.position.x > xMin && targetTransform.transform.position.x < xMax &&
            targetTransform.transform.position.z > zMin && targetTransform.transform.position.z < zMax)
        {
            triggered = true; 
            // call new scene
            transitionManager.GoToScene(sceneIndex);





        }
        else
        {
            textDisplay.text = "Restart";
        }
    }
}




