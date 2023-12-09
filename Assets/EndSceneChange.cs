using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneChange : MonoBehaviour
{

    [SerializeField]
    private string sceneToLoad  = "Menu";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player or any other specific object
        // You can customize this check based on your game's requirements
        if (other.CompareTag("paint"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
  
}
