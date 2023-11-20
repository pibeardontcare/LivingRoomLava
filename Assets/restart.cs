using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class restart : MonoBehaviour
{
        public string sceneToRestart; // Name of the scene to restart
    public float minX; // Minimum X-coordinate of the restart area
    public float maxX; // Maximum X-coordinate of the restart area
    public float minZ; // Minimum Z-coordinate of the restart area
    public float maxZ; // Maximum Z-coordinate of the restart area

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player (or any other object with a collider) has entered the restart area
        if (other.CompareTag("Player") && ShouldRestart(other.transform.position))
        {
            // Restart the scene
            SceneManager.LoadScene(sceneToRestart);
        }
    }

    private bool ShouldRestart(Vector3 position)
    {
        // Check if the player is within the defined restart area
        return position.x >= minX && position.x <= maxX && position.z >= minZ && position.z <= maxZ;
    }
}
