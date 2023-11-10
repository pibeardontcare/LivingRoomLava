using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class endSequence : MonoBehaviour
{
    // Variables to hold references and define boundaries
    public InOrOutColor inOrOutColor; // Reference to another script/component
    public ParticleSystem particleEmitter; // Reference to the particle system
    public Camera oculusMainCamera; // Reference to the main camera
    public string nextSceneName; // Holds the name of the next scene to load

    // Define boundaries for the end zone area
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public Animator prizeAnimator; // Reference to the Animator component on a prize object

    // Static variable to store the unlocked state of a prize
    public static bool prizeUnlocked = false;

    // Internal variable to track if the camera is within the defined area
    private bool isInsidePerimeter = false;

    // Start is called before the first frame update
    private void Start()
    {
        // Stop the particle emitter at the start
        particleEmitter.Stop();

        // Set a player preference at the start of the level
        PlayerPrefs.SetString("LevelProgress", "none");
        PlayerPrefs.Save();

        // Check the value of "Level Progress" in player preferences
        int levelProgress = PlayerPrefs.GetInt("LevelProgress", 0);
    }

    // Checks if the camera is within the defined area
    bool IsCameraWithinObjectPerimeter()
    {
        // Get the camera's position in world coordinates (ignoring y-axis)
        Vector3 cameraPosition = oculusMainCamera.transform.position;
        cameraPosition.y = 0f; // Ignore the y-axis

        // Check if the camera's x and z positions are within the specified boundaries
        bool isWithinBounds = (cameraPosition.x >= minX && cameraPosition.x <= maxX &&
                               cameraPosition.z >= minZ && cameraPosition.z <= maxZ);

        return isWithinBounds;
    }

    // Update is called once per frame
    void Update()
    {
        bool isWithinPerimeter = IsCameraWithinObjectPerimeter();
        bool isGameOver = inOrOutColor.gameOver;

        // Check if the camera has entered or exited the perimeter
        if (isWithinPerimeter != isInsidePerimeter)
        {
            isInsidePerimeter = isWithinPerimeter;

            // Perform actions based on whether the camera is within the object's perimeter
            if (isInsidePerimeter && !isGameOver)
            {
                Debug.Log("Camera is within the object's perimeter.");

                // Start emitting particles
                particleEmitter.Play();

                // Trigger an animation sequence
                prizeAnimator.SetTrigger("BoxOpen");

                // Set a player preference key to indicate that the level is completed
                int levelProgress = PlayerPrefs.GetInt("LevelProgress", 0);
                PlayerPrefs.SetInt("LevelProgress", levelProgress + 1);
                PlayerPrefs.Save();

                // Initiate a delayed scene change
                StartCoroutine(DelayedSceneChange());

                // Unlock the prize
                prizeUnlocked = true;
            }
            else
            {
                Debug.Log("Camera is NOT within the object's perimeter.");

                // Stop emitting particles
                particleEmitter.Stop();
            }
        }
    }

    // Coroutine for a delayed scene change
    IEnumerator DelayedSceneChange()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3.0f);

        // Load the specified scene
        SceneManager.LoadScene(nextSceneName);
    }
}
