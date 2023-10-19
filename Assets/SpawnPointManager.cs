using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnPointManager : MonoBehaviour
{
    public Transform spawnPoint; // Reference to spawn point Transform
    public Camera oculusCamera; // Reference Oculus main camera

    public GameObject objectToShow;


 private void Awake()


    {
           // Show the object
        objectToShow.SetActive(true);
        // Your initializatio if (oculusCamera == null)
        {
            Debug.LogError("Oculus main camera not assigned. Camera may not spawn correctly.");
            return;
        }

        // Check if the scene is the "MainMenu" scene
        if (SceneManager.GetActiveScene().name == "MainMenu1")
        {
            if (spawnPoint != null)
            {
                // Move the Oculus camera to the spawn point's position and rotation
                oculusCamera.transform.position = spawnPoint.position;
                oculusCamera.transform.rotation = spawnPoint.rotation;
            }
            else
            {
                Debug.LogError("Spawn point not assigned. Camera may not spawn correctly.");
            }
        }
    }

    void Start()

    {

       
    }
}
