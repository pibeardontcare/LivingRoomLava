using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 
public class endSequence : MonoBehaviour
{
   
    
public GameObject objectToShowHide; 
    public InOrOutColor inOrOutColor;
    public ParticleSystem particleEmitter;
    public Camera oculusMainCamera;

    // Define x and z boundaries for the end zone area
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public Animator prizeAnimator; // Reference to the Animator component on the prize object.

    // Trigger parameter name
    // Reference to the GameManager instance.
  
     // Create a public static variable to store the prize unlocked state.
    public static bool prizeUnlocked = false;
    private bool isInsidePerimeter = false;

    private void Start()
    {
        // Hide the objectToShowHide and stop the particle emitter at the start
       
        particleEmitter.Stop();

       // Set PlayerPrefs to "none" at the start of the level
        PlayerPrefs.SetString("LevelCompleted", "none");
        PlayerPrefs.Save();

     
         // Check the value of "Level1Completed" in PlayerPrefs
        int levelCompleted = PlayerPrefs.GetInt("Level1Completed", 0);

        // If "Level1Completed" is set to 1, show the object; otherwise, hide it.
        if (levelCompleted == 1)
        {
            objectToShowHide.SetActive(true);
        }
        else
        {
            objectToShowHide.SetActive(false);
        }
    }

    bool IsCameraWithinObjectPerimeter()
    {
        // Get the camera's position in world coordinates (ignoring y-axis).
        Vector3 cameraPosition = oculusMainCamera.transform.position;
        cameraPosition.y = 0f; // Ignore the y-axis.

        // Check if the camera's x and z positions are within the specified boundaries.
        bool isWithinBounds = (cameraPosition.x >= minX && cameraPosition.x <= maxX &&
                               cameraPosition.z >= minZ && cameraPosition.z <= maxZ);

        return isWithinBounds;
    }


    void Update()
    {
        bool isWithinPerimeter = IsCameraWithinObjectPerimeter();
        bool isGameOver = inOrOutColor.gameOver;
        // Check if the camera has entered or exited the perimeter
        if (isWithinPerimeter != isInsidePerimeter)
        {
            isInsidePerimeter = isWithinPerimeter;

            // Do something based on whether the camera is within the object's perimeter.
            if (isInsidePerimeter && !isGameOver)
            {
                Debug.Log("Camera is within the object's perimeter.");
                // Add your code here for when the camera is within the object.

            
                particleEmitter.Play();

                // Trigger the open animation sequence
                prizeAnimator.SetTrigger("BoxOpen");

               // Set a PlayerPrefs key to indicate that the level is completed
                PlayerPrefs.SetInt("Level1Completed", 1); // You can use a unique key for each level
                PlayerPrefs.Save();
                 StartCoroutine(DelayedSceneChange());


                 // unlock the prize
                prizeUnlocked = true;

            }
            else
            {
                Debug.Log("Camera is NOT within the object's perimeter.");
                // Add your code here for when the camera is NOT within the object.

              
                particleEmitter.Stop();
            }
        }
    }

     IEnumerator DelayedSceneChange()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3.0f);

        // Change the scene here, you can specify the scene name or index
        SceneManager.LoadScene("MainMenu1");
    }
}
